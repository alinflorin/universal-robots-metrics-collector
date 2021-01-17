using IMS.URM.BusinessServices.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using IMS.URM.BusinessServices.Actors;

namespace IMS.URM.BusinessServices
{
    public class TcpServerService : ITcpServerService
    {
        private const string TcpServerPortKey = "TcpServer:Port";
        private readonly int _port;
        private readonly TcpListener _tcpListener;
        private bool _canRun;
        private readonly ILogger<TcpServerService> _logger;
        private readonly IActorSystem _actorSystem;

        public TcpServerService(IConfiguration config, ILoggerFactory loggerFactory, IActorSystem actorSystem)
        {
            _port = int.Parse(config[TcpServerPortKey]);
            _tcpListener = new TcpListener(IPAddress.Any, _port);
            _logger = loggerFactory.CreateLogger<TcpServerService>();
            _actorSystem = actorSystem;
        }

        public void Start()
        {
            _canRun = true;
            _tcpListener.Start();
            Task.Run(() => {
                while (_canRun)
                {
                    try
                    {
                        var client = _tcpListener.AcceptTcpClient();
                        var ip = TryGetIp(client);
                        var actor = _actorSystem.GetOrCreate<RobotEventsActor, TcpClient>(ip).Result;
                        actor.Send(client);
                    } catch (Exception e)
                    {
                        _logger.LogError(e, "Client connect failed");
                    }
                }
            });
        }

        public void Stop()
        {
            _canRun = false;
            _tcpListener.Stop();
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
