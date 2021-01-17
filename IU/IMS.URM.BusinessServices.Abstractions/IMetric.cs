using IMS.URM.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IMS.URM.BusinessServices.Abstractions
{
    public interface IMetric
    {
        string DisplayName { get; }
        string Name { get; }
        IEnumerable<string> EventNames { get; }
        Task<MetricDto> Execute(IEnumerable<RobotEventDto> events);
        Task<MetricDetailsDto> GetDetails(IEnumerable<RobotEventDto> events);
    }
}
