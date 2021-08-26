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

        private List<Regex> _attackStrRegexList { get; }

        public WatchdogChecker(Config config, ILogger logger, IRemoteDB database)
        {
            this._config = config;
            this._logger = logger;
            this._database = database;
            // fetch documents from remote database and save locally to save bandwidth
            this._attackStrRegexList = new List<Regex>();
            foreach (AttackString attackStr in _database.GetAllAttackStrings())
            {
                Regex rx = new Regex(@attackStr.String, RegexOptions.IgnoreCase);
                this._attackStrRegexList.Add(rx);
            }
        }

        public bool ContainsAttackString(string data)
        {
            bool contains = false;

            // see if data contains for every attack string
            foreach (Regex rx in _attackStrRegexList)
            {
                if (rx.IsMatch(data))
                {
                    contains = true;
                    _logger.Log($"Attack string REGEX match found: {rx.ToString()}", LogSeverity.INFO);
                    break;  // only need to find once
                }
            }
            return contains;
        }
    }

}
