using System;
using System.Collections.Generic;
using System.Text;
using IMS.URM.Entities.Abstractions;

namespace IMS.URM.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
