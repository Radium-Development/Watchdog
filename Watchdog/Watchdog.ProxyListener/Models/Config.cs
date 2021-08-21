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
        public string WatchdogListenerIP { get; set; } = "127.0.0.1";
        public string RemoteDBConnectionString { get; set; } = "mongodb://Watchdog:Watchdog@10.0.0.171:27017/WatchdogSuspects?authSource=WatchdogSuspects";
        public string RemoteDBDatabaseName { get; set; } = "WatchdogSuspects";
        public string RemoteDBCollectionName { get; set; } = "strings";
    }
}
