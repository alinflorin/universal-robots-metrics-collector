using IMS.URM.Dto.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.URM.Dto
{
    public class GetMetricsQueryDto : BaseDto
    {
        public string Ip { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
