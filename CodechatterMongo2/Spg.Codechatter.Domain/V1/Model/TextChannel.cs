using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Spg.Codechatter.Domain.V1.Model
{
    public class TextChannel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Guid")]
        public Guid Guid { get; set; } = Guid.NewGuid();

        [BsonElement("Name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("ChatroomId")]
        public Guid ChatroomId { get; set; }

        // Embed Messages directly within the TextChannel
        [BsonElement("Messages")]
        public List<Message> Messages { get; set; } = new();

        public TextChannel(string name, Guid chatroomId)
        {
            Name = name;
            ChatroomId = chatroomId;
        }

        public void AddMessage(Message message)
        {
            if (message is not null)
            {
                Messages ??= new List<Message>();
                Messages.Add(message);
            }
            else
            {
                throw new ArgumentNullException(nameof(message));
            }
        }

        public void RemoveMessage(Message message)
        {
            Messages?.Remove(message);
        }
    }
}