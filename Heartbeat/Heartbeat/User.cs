using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heartbeat
{
    public class User
    {
        public string Ip { get; set; }
        public int Port { get; set; }

        public User(string ip, int port)
        {
            this.Ip = ip;
            this.Port = port;
        }
    }
}
