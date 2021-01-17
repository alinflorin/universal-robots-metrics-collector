using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.URM.BusinessServices.Abstractions
{
    public interface IPasswordEncryptor
    {
        string Encrypt(string password);
    }
}
