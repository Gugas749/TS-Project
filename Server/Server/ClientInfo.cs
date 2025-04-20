using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class ClientInfo
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string PublicKeyXml { get; set; }
        public NetworkStream Stream { get; set; }
    }
}
