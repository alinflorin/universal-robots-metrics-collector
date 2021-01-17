using IMS.URM.Entities.Abstractions;
using System;

namespace IMS.URM.Entities
{
    public class RobotEvent : BaseEntity
    {
        public string ReporterIp { get; set; }
        public string ReporterHostname { get; set; }
        public DateTime EventDateTime { get; set; }
        public string EventName { get; set; }
        public string EventDetails { get; set; }
    }
}
