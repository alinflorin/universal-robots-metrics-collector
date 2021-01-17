using System.Collections.Generic;
using IMS.URM.BusinessServices.Abstractions;
using IMS.URM.Helpers;
using IMS.URM.Persistence.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using ZNetCS.AspNetCore.Authentication.Basic;
using ZNetCS.AspNetCore.Authentication.Basic.Events;
using IAuthenticationService = IMS.URM.BusinessServices.Abstractions.IAuthenticationService;

namespace IMS.URM
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var env = services.BuildServiceProvider().GetRequiredService<IHostingEnvironment>();
            services.AddUrmCommon();
            services.AddUrmMappersAutoMapper();
            services.AddUrmMongoDbPersistence();
            services.AddUrmBusinessServices();

            AuthorizationPolicy policy = null;

            if (!env.IsDevelopment())
            {
                services
                  .AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
                  .AddBasicAuthentication(
                      options =>
                      {
                          options.Realm = "IMS.URM";
                          options.Events = new BasicAuthenticationEvents
                          {
                              OnValidatePrincipal = context =>
                                  {
                                      var authService = context.HttpContext.RequestServices.GetRequiredService<IAuthenticationService>();
                                      var foundUser = authService.Login(context.UserName, context.Password).Result;
                                      if (foundUser == null)
                                      {
                                          context.AuthenticationFailMessage = "Authentication failed.";
                                          return Task.CompletedTask;
                                      }

                                      var claims = new List<Claim>
                                      {
                                      new Claim(ClaimTypes.Name, foundUser.Username, ClaimValueTypes.String,
                                          context.Options.ClaimsIssuer),
                                      new Claim(ClaimTypes.Role, foundUser.IsAdmin ? "admin" : "user", ClaimValueTypes.String,
                                          context.Options.ClaimsIssuer)
                                      };
                                      var principal = new ClaimsPrincipal(new ClaimsIdentity(claims,
                                          BasicAuthenticationDefaults.AuthenticationScheme));
                                      context.Principal = principal;
                                      return Task.CompletedTask;
                                  }
                          };
                      });

                policy = new AuthorizationPolicyBuilder(BasicAuthenticationDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();

                services.AddAuthorization(ao =>
                {
                    ao.AddPolicy("policy", policy);
                    ao.DefaultPolicy = ao.GetPolicy("policy");
                });
            }

            services.AddMvc(o =>
            {
                if (!env.IsDevelopment())
                {
                    o.Filters.Add(new AuthorizeFilter(policy));
                }
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IPersistenceService persistenceService, IPasswordEncryptor encryptor, ITcpServerService tcpServer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            if (!env.IsDevelopment())
            {
                app.UseAuthentication();

                app.Use(async (context, next) =>
                {
                    if (!context.User.Identity.IsAuthenticated)
                    {
                        await context.ChallengeAsync();
                    }
                    else
                    {
                        await next();
                    }
                });
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment() && !env.IsRunningInDocker())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            Seeder.Seed(persistenceService, encryptor).Wait();

            tcpServer.Start();
        }
    }
}
