using Spg.Codechatter.Domain.V1.Dtos.Message;
using Spg.Codechatter.Domain.V1.Model;
using Spg.Codechatter.Infrastructure;
using Spg.Codechatter.Repository.V1.Interfaces.MessageRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace Spg.Codechatter.Repository.V1.Repositories
{
    public class MessageRepository : IReadMessageRepository, IModifyMessageRepository
    {
        private readonly CodechatterMongoContext _db;

        public MessageRepository(CodechatterMongoContext db)
        {
            _db = db;
        }

        public Message GetMessageById(Guid id)
        {
            var message = _db.Messages.Find(m => m.Guid == id)?.FirstOrDefault();

            if (message == null)
            {
                throw new KeyNotFoundException("Message was not found. ID: " + id);
            }

            return message;
        }

        public IEnumerable<Message> GetAllMessages()
        {
            return _db.Messages.AsQueryable();
        }

        public IEnumerable<Message> GetMessagesByMemberId(Guid userId, Guid chatroomId)
        {
            return _db.Messages.Find(m => m.UserId == userId && m.ChatroomId == chatroomId).ToEnumerable();
        }

        public IEnumerable<Message> GetMessagesByTextChannelId(Guid textChannelId)
        {
            return _db.Messages.Find(m => m.TextChannelId == textChannelId).ToEnumerable();
        }

        public void AddMessage(Message message)
        {
            _db.Messages.InsertOne(message);
        }

        public void UpdateMessage(UpdateMessageDto message)
        {
            var result = _db.Messages.FindOneAndDelete(m => m.Guid == message.Id);
            if (result != null)
            {
                _db.Messages.InsertOne(new Message(message.Content, message.UserId, message.ChatroomId, message.TextChannelId) { Guid = message.Id });
            }
            else
            {
                throw new KeyNotFoundException("Message was not found. ID: " + message.Id);
            }
        }

        public void DeleteMessage(Guid id)
        {
            var result = _db.Messages.FindOneAndDelete(m => m.Guid == id);
            if (result == null)
            {
                throw new KeyNotFoundException("Message was not found. ID: " + id);
            }
        }
    }
}
