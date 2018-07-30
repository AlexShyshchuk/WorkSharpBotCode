using System;
using System.Net.Sockets;
using System.Net;

namespace NewBot.Commands.Weather
{
    public class GetIpAdress
    {

        public string GetIp(){

            string ip = string.Empty;

            using(Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP)) {

                socket.Connect("10.0.5.4", 65530);
                ip = (socket.LocalEndPoint as IPEndPoint).Address.ToString();

            }

            return ip;

        }

    }
}
