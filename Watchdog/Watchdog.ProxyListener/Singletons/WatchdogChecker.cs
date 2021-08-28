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
        private IRemoteDB _database { get; }
        // (key, value) pair dictionary of (attack string text, type of attack) 
        private Dictionary<Regex, string> _attackStrDict { get; }

        public WatchdogChecker(Config config, ILogger logger, IRemoteDB database)
        {
            this._config = config;
            this._logger = logger;
            this._database = database;
            // fetch documents from remote database and save locally to save bandwidth
            this._attackStrDict = new Dictionary<Regex, string>();
            foreach (AttackString attackStr in _database.GetAllAttackStrings())
            {
                Regex rx = new Regex(@attackStr.String, RegexOptions.IgnoreCase);
                this._attackStrDict.Add(rx, attackStr.Type);
            }
        }

        public bool ContainsAttackString(string data)
        {
            bool contains = false;

            // see if data contains for every attack string
            foreach (Regex rx in _attackStrDict.Keys)
            {
                if (rx.IsMatch(data))
                {
                    contains = true;
                    string type = _attackStrDict[rx];
                    _logger.Log($"[{type}] string found: {rx.ToString()}", LogSeverity.INFO);
                    break;  // only need to find once
                }
            }
            return contains;
        }
    }

}
