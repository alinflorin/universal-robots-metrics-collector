using IMS.URM.Dto.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.URM.Dto
{
    public class MetricDetailDto : BaseDto
    {
        public DateTime Date { get; set; }
        public dynamic Value { get; set; }
    }
}
