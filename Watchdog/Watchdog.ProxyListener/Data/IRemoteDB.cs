using System.Collections.Generic;
using Watchdog.ProxyListener.Models;

namespace Watchdog.ProxyListener.Data
{
    public interface IRemoteDB
    {
        public IEnumerable<AttackString> GetAllAttackStrings();
    }
}
