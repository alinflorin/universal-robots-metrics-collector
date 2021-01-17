using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IMS.URM.Entities;

namespace IMS.URM.BusinessServices.Abstractions
{
    public interface IAuthenticationService
    {
        Task<User> Login(string username, string plainPassword);
    }
}
