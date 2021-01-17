using IMS.URM.Dto.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.URM.Dto
{
    public class GetMetricDetailsQueryDto : GetMetricsQueryDto
    {
        public string MetricName { get; set; }
    }
}
