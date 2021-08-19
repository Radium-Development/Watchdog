using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watchdog.ProxyListener.Models
{
    public class Config
    {
        public ushort WatchdogListenerPort { get; set; } = 3000; // Port to listen for traffic on
        public ushort WatchdogOutgoingPort { get; set; } = 3001; // Port to send data too once processed
<<<<<<< HEAD
        public string[] ListnerIPs { get; set; } = { "127.0.0.1", "localhost" };
=======
        public string[] ListnerIPs { get; set; } = { "127.0.0.1", "0.0.0.0" };
>>>>>>> 26fab3b47051fbf35cbd1b08a182d64809be1165
    }
}
