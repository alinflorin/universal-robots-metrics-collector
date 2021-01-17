using IMS.URM.Dto.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.URM.Dto
{
    public class RobotDto : BaseDto
    {
        public string Ip { get; set; }
        public string Hostname { get; set; }
    }
}
