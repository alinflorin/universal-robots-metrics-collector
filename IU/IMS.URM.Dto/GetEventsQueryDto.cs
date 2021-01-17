using IMS.URM.Dto.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.URM.Dto
{
    public class GetEventsQueryDto : GetMetricsQueryDto
    {
        public IEnumerable<string> EventNames { get; set; }
        public int? Limit { get; set; }
    }
}
