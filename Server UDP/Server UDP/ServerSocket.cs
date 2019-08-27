using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server_UDP
{
    class ServerSocket
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

        public void Server(string ip, int port)
        {
            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            socket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            Receive();
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
