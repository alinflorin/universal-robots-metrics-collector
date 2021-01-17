using IMS.URM.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMS.URM.BusinessServices.Abstractions
{
    public interface IRobotEventsService
    {
        Task SaveEvent(RobotEventDto dto);
        Task<IEnumerable<RobotEventDto>> GetEvents(GetEventsQueryDto queryDto);
        Task<IEnumerable<MetricDto>> GetMetrics(GetMetricsQueryDto queryDto);
        Task<IEnumerable<RobotDto>> GetAllRobots();
        Task<MetricDetailsDto> GetMetricDetails(GetMetricDetailsQueryDto queryDto);
    }
}
