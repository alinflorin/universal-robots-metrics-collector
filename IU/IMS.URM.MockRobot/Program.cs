using System;
using System.Net.Sockets;
using System.Text;

namespace IMS.URM.MockRobot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var tcpClient = new TcpClient();
            tcpClient.Connect("localhost", 5002);
            var stream = tcpClient.GetStream();
            while (true)
            {
                Console.Write("Data = ");
                var data = Console.ReadLine();
                if (string.IsNullOrEmpty(data))
                {
                    continue;
                }
                var bytes = Encoding.ASCII.GetBytes(data);
                stream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
