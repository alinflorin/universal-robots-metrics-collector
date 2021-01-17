using IMS.URM.Dto.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.URM.Dto
{
    public class MetricDto : BaseDto
    {
        public string MetricName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public dynamic Value { get; set; }
        public string EventName { get; set; }
    }
}
