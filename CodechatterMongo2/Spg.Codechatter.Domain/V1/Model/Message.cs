using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Spg.Codechatter.Domain.V1.Model
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Guid")]
        public Guid Guid { get; set; } = Guid.NewGuid();

        [BsonElement("Content")]
        public string Content { get; set; } = string.Empty;

        [BsonElement("DateAndTime")]
        public DateTime DateAndTime { get; set; } = DateTime.Now;

        [BsonElement("TextChannelId")]
        public Guid TextChannelId { get; set; }

        [BsonElement("UserId")]
        public Guid UserId { get; set; }

        [BsonElement("ChatroomId")]
        public Guid ChatroomId { get; set; }

        public Message(string content, Guid userId, Guid chatroomId, Guid textChannelId)
        {
            Content = content;
            UserId = userId;
            ChatroomId = chatroomId;
            TextChannelId = textChannelId;
        }
    }
}