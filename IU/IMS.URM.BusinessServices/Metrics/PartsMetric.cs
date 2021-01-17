using IMS.URM.BusinessServices.Abstractions;
using IMS.URM.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.URM.BusinessServices.Metrics
{
    public class PartsMetric : IMetric
    {
        public string DisplayName { get; } = "Piese manipulate";
        public string Name { get; set; } = "parts";
        public IEnumerable<string> EventNames { get; set; } = new List<string> {"partmanipulated" };
        public async Task<MetricDto> Execute(IEnumerable<RobotEventDto> events)
        {
            return await Task.FromResult(
                new MetricDto
                {
                    Description = "Nr. de piese manipulate",
                    Title = DisplayName,
                    Value = events.AsParallel().LongCount(x => x.EventName == "partmanipulated") + " piese manipulate",
                    MetricName = Name
                });
        }

        public async Task<MetricDetailsDto> GetDetails(IEnumerable<RobotEventDto> events)
        {
            return await Task.FromResult(new MetricDetailsDto
            {  
                DisplayName = DisplayName,
                Data = events.AsParallel().Select(x => new MetricDetailDto {
                    Date = x.EventDateTime,
                    Value = "piesa manipulata"
                }).OrderByDescending(x => x.Date).ToList()
            });
        }
    }
}
