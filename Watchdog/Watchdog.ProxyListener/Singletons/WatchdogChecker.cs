using System.Collections.Generic;
using Watchdog.ProxyListener.Data;
using Watchdog.ProxyListener.Models;
using Watchdog.ProxyListener.Singletons.Logging;
using System.Text.RegularExpressions;

namespace Watchdog.ProxyListener.Singletons
{
    public class WatchdogChecker
    {
        private Config _config { get; }

        private ILogger _logger { get; }

        private IRemoteDB _database { get;  }

        private IEnumerable<AttackString> _attackStrings { get; }

        public WatchdogChecker(Config config, ILogger logger, IRemoteDB database)
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
            Regex rx = null;

            // see if data contains for every attack string
            foreach (AttackString attackStr in _attackStrings)
            {
                rx =  new Regex(@attackStr.String, RegexOptions.IgnoreCase);
                if (rx.IsMatch(data))
                {
                    contains = true;
                    _logger.Log($"Attack string of type [{attackStr.Type}] found (REGEX): {attackStr.String}", LogSeverity.INFO);
                    break;  // only need to find once
                }
            }
            return contains;
        }
    }

}
