using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Watchdog.ProxyListener.Models
{
    public class AttackString
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("string")]
        public string String { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }
    }
}
