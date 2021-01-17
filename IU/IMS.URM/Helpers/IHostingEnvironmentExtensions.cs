using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace IMS.URM.Helpers
{
    public static class IHostingEnvironmentExtensions
    {
        private const string RunInContainerKey = "RUN_IN_CONTAINER";

        public static bool IsRunningInDocker(this IHostingEnvironment env)
        {
            var envVar = Environment.GetEnvironmentVariable(RunInContainerKey);
            if (string.IsNullOrEmpty(envVar))
            {
                return false;
            }

            return envVar == "true";
        }
    }
}
