using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Heartbeat
{
    class Program
    {
        private static List<User> Users = new List<User>();
        private static SocketUDP2 Socket;

        static void Main(string[] args)
        {
            //Console.Write("Server Port: ");
            //int ServerPort = Convert.ToInt32(Console.ReadLine());

            Socket = new SocketUDP2();

            while (true)
            {
                Console.Write("0 -> Exit | 1 -> Add User: ");
                int op = Convert.ToInt32(Console.ReadLine());

                if (op == 0)
                {
                    break;
                }

                Console.Write("Client IP: ");
                string ClientIP = Console.ReadLine();

                Console.Write("Client Port: ");
                int ClientPort = Convert.ToInt32(Console.ReadLine());

                Users.Add(new User(ClientIP, ClientPort));
            }

            Users.Add(new User("172.18.0.21", 60000));
            //Users.Add(new User("172.18.0.29", 60000));
            //Users.Add(new User("172.18.0.30", 60000));
            //Users.Add(new User("172.18.0.31", 60000));

            StartThreads();
        }

        static void StartThreads()
        {
            var SendMsg = new Thread(new ThreadStart(Send));
            //SendMsg.IsBackground = true;
            SendMsg.Start();

            var ReceiveMsg = new Thread(new ThreadStart(Receive));
            //ReceiveMsg.IsBackground = true;
            ReceiveMsg.Start();

            var ReplyMsg = new Thread(new ThreadStart(Reply));
            //ReplyMsg.IsBackground = true;
            ReplyMsg.Start();
        }

        static void Send()
        {
            while (true)
            {
                foreach (User user in Users)
                {
                    Socket.Send(user.Ip, user.Port, "Heartbeat request");
                }

                Thread.Sleep(1000);
            }
        }

        static void Receive()
        {
            while (true)
            {
                Socket.Receive();
            }
        }

        static void Reply()
        {
            while (true)
            {
                if (Socket.Replies.Count > 0)
                {
                    User user = Socket.Replies.Dequeue();
                    Socket.Send(user.Ip, user.Port, "Heartbeat reply");
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
