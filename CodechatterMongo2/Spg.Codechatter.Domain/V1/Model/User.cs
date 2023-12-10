using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Spg.Codechatter.Domain.V1.Model
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Guid")]
        public Guid Guid { get; set; } = Guid.NewGuid();

        [BsonElement("EmailAddress")]
        public string EmailAddress { get; set; } = string.Empty;

        [BsonElement("Username")]
        public string Username { get; set; } = string.Empty;

        [BsonElement("Password")]
        public string Password { get; set; } = string.Empty;

        [BsonElement("ChatroomId")]
        public Guid ChatroomId { get; set; }

        // Embed Messages directly within the User
        [BsonElement("Messages")]
        public List<Message> Messages { get; set; } = new();

        public User(Guid chatroomId, string emailAddress, string username, string password)
        {
            ChatroomId = chatroomId;
            EmailAddress = emailAddress;
            Username = username;
            Password = password;
        }
    }
}