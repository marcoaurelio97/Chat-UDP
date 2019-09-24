using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Heartbeat
{
    class SocketUDP2
    {
        private UdpClient Client, Server;
        public Queue<User> Replies;
        private int ServerPort;

        public SocketUDP2()
        {
            this.ServerPort = 60000;
            Server = new UdpClient(ServerPort);
            Client = new UdpClient();
            Replies = new Queue<User>();
        }

        public void Send(string ip, int port, string text)
        {
            Client.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
            byte[] data = Encoding.ASCII.GetBytes(text);
            Client.Send(data, data.Length);

            if (text.Contains("reply"))
            {
                Console.WriteLine("Enviando reply para " + ip);
            }
            else
            {
                Console.WriteLine("Enviando request para " + ip);
            }
        }

        public void Receive()
        {
            var ReceiveEndPoint = new IPEndPoint(IPAddress.Any, ServerPort);
            var ReceiveBuffer = Server.Receive(ref ReceiveEndPoint);
            var ip = ReceiveEndPoint.ToString();
            var mensagemRecebida = Encoding.ASCII.GetString(ReceiveBuffer);

            if (mensagemRecebida.Contains("request"))
            {
                var info = ip.Split(':');
                Console.WriteLine("Recebendo request de " + info[0]);
                Replies.Enqueue(new User(info[0], Convert.ToInt32(info[1])));
            }
            else
            {
                Console.WriteLine("Recebendo reply de " + ip);
            }
        }
    }
}
