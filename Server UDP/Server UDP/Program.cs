using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_UDP
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerSocket server = new ServerSocket();

            Console.Write("IP: ");
            string ip = Console.ReadLine();

            Console.Write("Port: ");
            int port = Convert.ToInt32(Console.ReadLine());

            server.Server(ip, port);

            Console.ReadKey();
        }
    }
}
