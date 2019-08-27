using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client_UDP
{
    class ClientSocket
    {
        private Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private const int bufSize = 8 * 1024;
        private State state = new State();
        private EndPoint endpointFrom = new IPEndPoint(IPAddress.Any, 0);
        private AsyncCallback asc = null;

        public class State
        {
            public byte[] buffer = new byte[bufSize];
            public string user;
        }

        public void Client(string ip, int port, string user)
        {
            state.user = user;
            socket.Connect(IPAddress.Parse(ip), port);
            Receive();
        }

        public void Send(string message)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            //state.buffer = Encoding.ASCII.GetBytes(message);
            socket.BeginSend(data, 0, data.Length, SocketFlags.None, (aResult) =>
            {
                State so = (State)aResult.AsyncState;
                int bytes = socket.EndSend(aResult);
                Console.WriteLine("{0}:, {1}", so.user, message);
            }, state);
        }

        private void Receive()
        {
            socket.BeginReceiveFrom(state.buffer, 0, bufSize, SocketFlags.None, ref endpointFrom, asc = (aResult) =>
            {
                State so = (State)aResult.AsyncState;
                int bytes = socket.EndReceiveFrom(aResult, ref endpointFrom);
                socket.BeginReceiveFrom(so.buffer, 0, bufSize, SocketFlags.None, ref endpointFrom, asc, so);
                Console.WriteLine("Received: {0}: {1}, {2}", endpointFrom.ToString(), bytes, Encoding.ASCII.GetString(so.buffer, 0, bytes));
            }, state);
        }
    }
}
