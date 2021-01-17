using IMS.URM.BusinessServices.Abstractions;
using IMS.URM.Dto;

namespace IMS.URM.BusinessServices
{
    public class TcpDecoderService : ITcpDecoderService
    {
        public RobotEventDto Decode(string data)
        {
            var splitData = data.Split('#');
            return new RobotEventDto
            {
                EventName = splitData[0],
                EventDetails = splitData.Length > 1 ? splitData[1] : null
            };
        }
    }
}
