using IMS.URM.BusinessServices.Abstractions;
using IMS.URM.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.URM.BusinessServices.Metrics
{
    public class StandByMetric : IMetric
    {
        public string DisplayName { get; } = "Timp fara miscare";
        public string Name { get; set; } = "standby";
        public IEnumerable<string> EventNames { get; set; } = new List<string> { "startmovement", "stopmovement" };
        public async Task<MetricDto> Execute(IEnumerable<RobotEventDto> events)
        {
            var filteredEvents = events
                .AsParallel()
                .Where(x => x.EventName == "startmovement" || x.EventName == "stopmovement")
                .OrderBy(x => x.EventDateTime)
                .ToList();
            double totalMs = 0;
            for (var i = 0; i < filteredEvents.Count; i++)
            {
                if (i > 0 && filteredEvents[i].EventName == "startmovement" && filteredEvents[i-1].EventName == "stopmovement")
                {
                    totalMs += (filteredEvents[i].EventDateTime - filteredEvents[i-1].EventDateTime).TotalMilliseconds;
                }
            }
            return await Task.FromResult(
                new MetricDto
                {
                    Description = "Timpul in care robotul nu a efectuat nicio miscare",
                    Title = DisplayName,
                    Value = TimeSpan.FromMilliseconds(totalMs).ToString(),
                    MetricName = Name
                });
        }

        public async Task<MetricDetailsDto> GetDetails(IEnumerable<RobotEventDto> events)
        {
            return await Task.FromResult(new MetricDetailsDto
            {
                DisplayName = DisplayName,
                Data = events
                .AsParallel()
                .OrderByDescending(x => x.EventDateTime)
                .Select(x => new MetricDetailDto
                {
                    Date = x.EventDateTime,
                    Value = x.EventName == "startmovement" ? "start miscare" : "stop miscare"
                })
                .ToList()
            });
        }
    }
}
