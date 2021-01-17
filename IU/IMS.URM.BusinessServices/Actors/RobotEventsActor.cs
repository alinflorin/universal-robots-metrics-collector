using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using IMS.URM.BusinessServices.Abstractions;
using Microsoft.Extensions.Logging;

namespace IMS.URM.BusinessServices.Actors
{
    public class RobotEventsActor : Actor<TcpClient>
    {
        private readonly ITcpDecoderService _tcpDecoderService;
        private readonly IRobotEventsService _robotEventsService;

        public RobotEventsActor(ILoggerFactory loggerFactory, ITcpDecoderService tcpDecoderService, IRobotEventsService robotEventsService) : base(loggerFactory)
        {
            _tcpDecoderService = tcpDecoderService;
            _robotEventsService = robotEventsService;
        }

        public override async Task OnReceive(TcpClient tcpClient)
        {
            try
            {
                var ip = TryGetIp(tcpClient);
                var hostname = TryGetHostname(tcpClient);
                var stream = tcpClient.GetStream();
                int i;
                var bytes = new byte[256];
                while (tcpClient.Connected && (i = (await stream.ReadAsync(bytes, 0, bytes.Length))) != 0)
                {
                    var hex = BitConverter.ToString(bytes);
                    var data = Encoding.ASCII.GetString(bytes, 0, i);
                    if (!tcpClient.Connected) throw new Exception("Client disconnected");
                    Logger.LogInformation($"Received from {ip}: {data}");
                    var dto = _tcpDecoderService.Decode(data);
                    dto.ReporterIp = ip;
                    dto.ReporterHostname = hostname;
                    await _robotEventsService.SaveEvent(dto);
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Client socket connection failed");
            }
        }

        private static string TryGetHostname(TcpClient client)
        {
            try
            {
                return Dns.GetHostEntry(((IPEndPoint)client.Client.RemoteEndPoint).Address).HostName;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static string TryGetIp(TcpClient client)
        {
            try
            {
                return ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
