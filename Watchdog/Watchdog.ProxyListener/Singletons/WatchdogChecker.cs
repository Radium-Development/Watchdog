using System.Collections.Generic;
using Watchdog.ProxyListener.Data;
using Watchdog.ProxyListener.Models;
using Watchdog.ProxyListener.Singletons.Logging;

namespace Watchdog.ProxyListener.Singletons
{
    public class WatchdogChecker
    {
        private Config _config { get; }

        private ILogger _logger { get; }

        private RemoteDB _database { get;  }

        private IEnumerable<AttackString> _attackStrings { get; }

        public WatchdogChecker(Config config, ILogger logger, RemoteDB database)
        {
            this._config = config;
            this._logger = logger;
            this._database = database;
            // fetch documents from remote database and save locally to save bandwidth
            this._attackStrings = _database.GetAllAttackStrings();
        }

        public bool ContainsAttackString(string data)
        {
            bool contains = false;

            // see if data contains for every attack string
            foreach (AttackString str in _attackStrings)
            {
                if (data.Contains(str.String))
                {
                    contains = true;
                    _logger.Log($"Attack string of type [{str.Type}] found: {str.String}", LogSeverity.INFO);
                    break;  // only need to find once
                }
            }
            return contains;
        }
    }

}
