using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Spg.Codechatter.Domain.V1.Model
{
    public class Chatroom
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Guid")]
        public Guid Guid { get; set; } = Guid.NewGuid();

        [BsonElement("Name")]
        public string Name { get; set; } = default!;

        // Embed TextChannels directly within the Chatroom
        [BsonElement("TextChannels")]
        public List<TextChannel> TextChannels { get; set; } = new();

        public Chatroom(string name)
        {
            Name = name;
        }

        public void AddTextChannel(TextChannel textChannel)
        {
            if (textChannel is not null)
            {
                if (TextChannels == null)
                {
                    TextChannels = new List<TextChannel>();
                }

                if (!TextChannels.Exists(c => c.Name.Equals(textChannel.Name)))
                {
                    TextChannels.Add(textChannel);
                }
                else
                {
                    throw new ArgumentException($"TextChannel with name {textChannel.Name} already exists");
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(textChannel));
            }
        }

        public void RemoveTextChannel(TextChannel textChannel)
        {
            if (textChannel is not null)
            {
                TextChannels?.Remove(textChannel);
            }
            else
            {
                throw new ArgumentNullException("TextChannel cannot be null");
            }
        }
    }
}