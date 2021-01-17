using System.Collections.Generic;
using System.Threading.Tasks;
using IMS.URM.BusinessServices.Abstractions;
using IMS.URM.Dto;
using Microsoft.AspNetCore.Mvc;

namespace IMS.URM.Controllers
{
    [Route("api/[controller]")]
    public class RobotEventsController : Controller
    {
        private readonly IRobotEventsService _robotEventsService;

        public RobotEventsController(IRobotEventsService robotEventsService)
        {
            _robotEventsService = robotEventsService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IEnumerable<RobotEventDto>> GetEvents([FromBody]GetEventsQueryDto dto)
        {
            return await _robotEventsService.GetEvents(dto);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IEnumerable<MetricDto>> GetMetrics([FromBody]GetMetricsQueryDto dto)
        {
            return await _robotEventsService.GetMetrics(dto);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IEnumerable<RobotDto>> GetAllRobots()
        {
            return await _robotEventsService.GetAllRobots();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<MetricDetailsDto> GetMetricDetails([FromBody]GetMetricDetailsQueryDto dto)
        {
            return await _robotEventsService.GetMetricDetails(dto);
        }
    }
}
