using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_UDP
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientSocket client = new ClientSocket();

            Console.Write("IP: ");
            string ip = Console.ReadLine();

            Console.Write("Port: ");
            int port = Convert.ToInt32(Console.ReadLine());

            Console.Write("User: ");
            string user = Console.ReadLine();

            client.Client(ip, port, user);

            while (true)
            {
                Console.Write("Message: ");
                string message = Console.ReadLine();

                client.Send(message);
            }
        }
    }
}
