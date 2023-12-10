using Spg.Codechatter.Domain.V1.Dtos.Chatroom;
using Spg.Codechatter.Domain.V1.Model;
using Spg.Codechatter.Infrastructure;
using Spg.Codechatter.Repository.V1.Interfaces.ChatroomRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace Spg.Codechatter.Repository.V1.Repositories
{
    public class ChatroomRepository : IReadChatroomRepository, IModifyChatroomRepository
    {
        private readonly CodechatterMongoContext _db;

        public ChatroomRepository(CodechatterMongoContext db)
        {
            _db = db;
        }

        public Chatroom GetChatroomById(Guid guid)
        {
            var chatroom = _db.Chatrooms.Find(c => c.Guid == guid)?.FirstOrDefault();

            if (chatroom == null)
            {
                throw new KeyNotFoundException("Chatroom was not found. ID: " + guid);
            }

            return chatroom;
        }

        public IEnumerable<Chatroom> GetAllChatrooms()
        {
            return _db.Chatrooms.AsQueryable();
        }

        public void AddChatroom(Chatroom chatroom)
        {
            _db.Chatrooms.InsertOne(chatroom);
        }

        public void UpdateChatroom(UpdateChatroomDto chatroom)
        {
            var result = _db.Chatrooms.FindOneAndDelete(c => c.Guid == chatroom.Id);
            if (result != null)
            {
                _db.Chatrooms.InsertOne(new Chatroom(chatroom.Name) { Guid = chatroom.Id });
            }
            else
            {
                throw new KeyNotFoundException("Chatroom was not found. ID: " + chatroom.Id);
            }
        }

        public void DeleteChatroom(Guid guid)
        {
            var result = _db.Chatrooms.FindOneAndDelete(c => c.Guid == guid);
            if (result == null)
            {
                throw new KeyNotFoundException("Chatroom was not found. ID: " + guid);
            }
        }
    }
}