﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatUDP
{
    class State
    {
        public byte[] buffer;

        public State(int bufSize)
        {
            buffer = new byte[bufSize];
        }
    }
}
