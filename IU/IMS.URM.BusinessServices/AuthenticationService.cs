using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.URM.BusinessServices.Abstractions;
using IMS.URM.Entities;
using IMS.URM.Persistence.Abstractions;

namespace IMS.URM.BusinessServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPersistenceService _persistenceService;
        private readonly IPasswordEncryptor _passwordEncryptor;
        private IEnumerable<User> _validUsers { get; set; } = new List<User>();

        public AuthenticationService(IPersistenceService persistenceService, IPasswordEncryptor passwordEncryptor)
        {
            _passwordEncryptor = passwordEncryptor;
            _persistenceService = persistenceService;
            var usersCol = _persistenceService.Collection<User>("users");
            _validUsers = usersCol.Get(x => !x.IsArchived).Result;
        }

        public async Task<User> Login(string username, string plainPassword)
        {
            return await Task.FromResult(_validUsers.FirstOrDefault(x =>
                x.Username == username && x.Password == _passwordEncryptor.Encrypt(plainPassword)));
        }
    }
}
