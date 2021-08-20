using MongoDB.Bson.Serialization.Attributes;

namespace Watchdog.ProxyListener.Models
{
    public class AttackString
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        public string String { get; set; }

        public string Type { get; set; }
    }
}
