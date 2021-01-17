using IMS.URM.Dto.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.URM.Dto
{
    public class MetricDetailsDto : BaseDto
    {
        public IEnumerable<MetricDetailDto> Data { get; set; }
        public string DisplayName { get; set; }
    }
}
