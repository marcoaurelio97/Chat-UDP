using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatUDP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Port Server: ");
            int portServer = Convert.ToInt32(Console.ReadLine());

            SocketUDP server = new SocketUDP();
            server.Server(portServer);

            Console.Write("IP Client: ");
            string ipClient = Console.ReadLine();

            Console.Write("Port Client: ");
            int portClient = Convert.ToInt32(Console.ReadLine());

            SocketUDP client = new SocketUDP();
            client.Client(ipClient, portClient);

            while (true)
            {
                string message = Console.ReadLine();
                client.Send(message);
            }
        }
    }
}
