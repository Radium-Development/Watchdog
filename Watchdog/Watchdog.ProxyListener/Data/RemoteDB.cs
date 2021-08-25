using MongoDB.Driver;
using System.Collections.Generic;
using Watchdog.ProxyListener.Models;

namespace Watchdog.ProxyListener.Data
{
    public class RemoteDB: IRemoteDB
    {
        private readonly IMongoCollection<AttackString> _attackStrings;
        private Config _config { get; }

        public RemoteDB(Config config)
        {
            this._config = config;
            // connect remote mongodb
            var client = new MongoClient(_config.RemoteDBConnectionString);
            var database = client.GetDatabase(_config.RemoteDBDatabaseName);

            _attackStrings = database.GetCollection<AttackString>(_config.RemoteDBCollectionName);
        }

        public IEnumerable<AttackString> GetAllAttackStrings()
        {
            return _attackStrings.Find(attackString => true).ToList();
        }
    }
}
