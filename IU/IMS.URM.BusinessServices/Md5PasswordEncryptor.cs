using System;
using System.Collections.Generic;
using System.Text;
using IMS.URM.BusinessServices.Abstractions;
using Microsoft.Extensions.Configuration;

namespace IMS.URM.BusinessServices
{
    public class Md5PasswordEncryptor : IPasswordEncryptor
    {
        private const string SaltKey = "Encryptor:Salt";
        private readonly string _salt;

        public Md5PasswordEncryptor(IConfiguration config)
        {
            _salt = config[SaltKey];
        }

        public string Encrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return string.Empty;
            }
            password = $"{_salt}{password}{_salt}";
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var inputBytes = Encoding.ASCII.GetBytes(password);
                var hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                var sb = new StringBuilder();
                foreach (var t in hashBytes)
                {
                    sb.Append(t.ToString("X2"));
                }
                return sb.ToString().ToLower();
            }
        }
    }
}
