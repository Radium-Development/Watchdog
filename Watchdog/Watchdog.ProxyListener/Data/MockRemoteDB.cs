using System.Collections.Generic;
using Watchdog.ProxyListener.Models;

namespace Watchdog.ProxyListener.Data
{
    /* Mock class for unit testing */
    public class MockRemoteDB : IRemoteDB
    {
        public MockRemoteDB()
        {
        }

        public IEnumerable<AttackString> GetAllAttackStrings()
        {
            List<AttackString> mockList = new List<AttackString>();

            // mock data
            AttackString mockData1 = new AttackString();
            mockData1.String = "<iframe onbeforeload";
            mockData1.Type = "Cross Site Scripting";

            AttackString mockData2 = new AttackString();
            mockData2.String = "<body onkeydown body";
            mockData2.Type = "Cross Site Scripting";

            AttackString mockData3 = new AttackString();
            mockData3.String = "<script>alert";
            mockData3.Type = "Cross Site Scripting";

            mockList.Add(mockData1);
            mockList.Add(mockData2);
            mockList.Add(mockData3);
            return mockList;
        }
    }
}
