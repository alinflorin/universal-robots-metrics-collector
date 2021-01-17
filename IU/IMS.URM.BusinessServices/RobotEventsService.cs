using IMS.URM.BusinessServices.Abstractions;
using IMS.URM.Dto;
using IMS.URM.Entities;
using IMS.URM.Mappers.Abstractions;
using IMS.URM.Persistence.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IMS.URM.BusinessServices
{
    public class RobotEventsService : IRobotEventsService
    {
        private readonly IMappingService _mappingService;
        private readonly IPersistenceService _persistenceService;
        private readonly IList<IMetric> _metrics;

        public RobotEventsService(IMappingService mappingService, IPersistenceService persistenceService, IList<IMetric> metrics)
        {
            _metrics = metrics;
            _mappingService = mappingService;
            _persistenceService = persistenceService;
        }

        public async Task<IEnumerable<RobotEventDto>> GetEvents(GetEventsQueryDto queryDto)
        {
            var db = _persistenceService.Collection<RobotEvent>("robotevents");
            var wheres = new List<Expression<Func<RobotEvent, bool>>>();
            if (!string.IsNullOrEmpty(queryDto.Ip))
            {
                wheres.Add(x => queryDto.Ip == x.ReporterIp);
            }
            if (queryDto.StartDate.HasValue)
            {
                wheres.Add(x => x.EventDateTime >= queryDto.StartDate.Value);
            }
            if (queryDto.EndDate.HasValue)
            {
                wheres.Add(x => x.EventDateTime <= queryDto.EndDate.Value);
            }
            if (queryDto.EventNames != null && queryDto.EventNames.Any())
            {
                wheres.Add(x => queryDto.EventNames.Contains(x.EventName));
            }
            var orders = new List<(Expression<Func<RobotEvent, object>>, bool)>
            {
                {(x => x.EventDateTime, true)}
            };
            return (await db.Get(wheres, orders, queryDto.Limit))
                .Select(x => _mappingService.Map<RobotEvent, RobotEventDto>(x).Result)
                .ToList();
        }

        public async Task<IEnumerable<MetricDto>> GetMetrics(GetMetricsQueryDto queryDto)
        {
            var result = new List<MetricDto>();
            foreach (var metric in _metrics)
            {
                var allEventsInRange = await GetEvents(new GetEventsQueryDto
                {
                    EndDate = queryDto.EndDate,
                    StartDate = queryDto.StartDate,
                    Ip = queryDto.Ip,
                    EventNames = metric.EventNames
                });
                result.Add(await metric.Execute(allEventsInRange));
            }
            return result;
        }

        public async Task SaveEvent(RobotEventDto dto)
        {
            var entity = await _mappingService.Map<RobotEventDto, RobotEvent>(dto);
            var db = _persistenceService.Collection<RobotEvent>("robotevents");
            await db.Save(entity);

            var robotsDb = _persistenceService.Collection<Robot>("robots");
            if (!(await robotsDb.Get(r => r.Ip == entity.ReporterIp)).Any())
            {
                await robotsDb.Save(new Robot {
                    Ip = entity.ReporterIp,
                    Hostname = entity.ReporterHostname
                });
            }
        }

        public async Task<IEnumerable<RobotDto>> GetAllRobots()
        {
            var db = _persistenceService.Collection<Robot>("robots");
            return (await db.Get(x => true))
                .Select(x => _mappingService.Map<Robot, RobotDto>(x).Result)
                .ToList();
        }

        public async Task<MetricDetailsDto> GetMetricDetails(GetMetricDetailsQueryDto queryDto)
        {
            var metric = _metrics.Single(x => x.Name == queryDto.MetricName);
            var allEventsInRange = await GetEvents(new GetEventsQueryDto
            {
                EndDate = queryDto.EndDate,
                StartDate = queryDto.StartDate,
                Ip = queryDto.Ip,
                EventNames = metric.EventNames,
                Limit = 1000
            });
            return await metric.GetDetails(allEventsInRange);
        }
    }
}
