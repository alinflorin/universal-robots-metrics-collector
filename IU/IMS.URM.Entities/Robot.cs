using IMS.URM.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.URM.Entities
{
    public class Robot : BaseEntity
    {
        public string Ip { get; set; }
        public string Hostname { get; set; }
    }
}
