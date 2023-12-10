using Spg.Codechatter.Domain.V1.Dtos.TextChannel;
using Spg.Codechatter.Domain.V1.Model;
using Spg.Codechatter.Infrastructure;
using Spg.Codechatter.Repository.V1.Interfaces.TextChannelRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace Spg.Codechatter.Repository.V1.Repositories
{
    public class TextChannelRepository : IReadTextChannelRepository, IModifyTextChannelRepository
    {
        private readonly CodechatterMongoContext _db;

        public TextChannelRepository(CodechatterMongoContext db)
        {
            _db = db;
        }

        public TextChannel GetTextChannelById(Guid id)
        {
            var textChannel = _db.TextChannels.Find(t => t.Guid == id)?.FirstOrDefault();

            if (textChannel == null)
            {
                throw new KeyNotFoundException("Text Channel was not found. ID: " + id);
            }

            return textChannel;
        }

        public IEnumerable<TextChannel> GetAllTextChannels()
        {
            return _db.TextChannels.AsQueryable();
        }

        public IEnumerable<TextChannel> GetTextChannelsByChatroomId(Guid chatroomId)
        {
            return _db.TextChannels.Find(t => t.ChatroomId == chatroomId).ToEnumerable();
        }

        public void AddTextChannel(TextChannel textChannel)
        {
            _db.TextChannels.InsertOne(textChannel);
        }

        public void UpdateTextChannel(UpdateTextChannelDto textChannel)
        {
            var result = _db.TextChannels.FindOneAndDelete(t => t.Guid == textChannel.Id);
            if (result != null)
            {
                _db.TextChannels.InsertOne(new TextChannel(textChannel.Name, textChannel.ChatroomId) { Guid = textChannel.Id });
            }
            else
            {
                throw new KeyNotFoundException("Text Channel was not found. UserId: " + textChannel.Id);
            }
        }

        public void DeleteTextChannel(Guid textChannelId)
        {
            var result = _db.TextChannels.FindOneAndDelete(t => t.Guid == textChannelId);
            if (result == null)
            {
                throw new KeyNotFoundException("Text Channel was not found. UserId: " + textChannelId);
            }
        }
    }
}
