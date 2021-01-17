using IMS.URM.Dto.Abstractions;
using System;

namespace IMS.URM.Dto
{
    public class RobotEventDto : BaseDto
    {
        public string ReporterIp { get; set; }
        public string ReporterHostname { get; set; }
        public DateTime EventDateTime { get; set; } = DateTime.UtcNow;
        public string EventName { get; set; }
        public string EventDetails { get; set; }
    }
}
