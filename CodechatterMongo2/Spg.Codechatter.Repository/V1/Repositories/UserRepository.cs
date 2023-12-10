using Spg.Codechatter.Domain.V1.Dtos.User;
using Spg.Codechatter.Domain.V1.Model;
using Spg.Codechatter.Infrastructure;
using Spg.Codechatter.Repository.V1.Interfaces.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace Spg.Codechatter.Repository.V1.Repositories
{
    public class UserRepository : IReadUserRepository, IModifyUserRepository
    {
        private readonly CodechatterMongoContext _db;

        public UserRepository(CodechatterMongoContext db)
        {
            _db = db;
        }

        public User GetUserById(Guid guid)
        {
            var user = _db.Users.Find(u => u.Guid == guid)?.FirstOrDefault();

            if (user == null)
            {
                throw new KeyNotFoundException("User was not found. ID: " + guid);
            }

            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _db.Users.AsQueryable();
        }

        public void AddUser(User user)
        {
            _db.Users.InsertOne(user);
        }

        public void UpdateUser(UpdateUserDto user)
        {
            var result = _db.Users.FindOneAndDelete(u => u.Guid == user.Id);
            if (result != null)
            {
                // Update the properties of the existing user
                result.EmailAddress = user.EmailAddress;
                result.Username = user.Username;
                result.Password = user.Password;

                // If ChatroomId is supposed to be updated as well, add the following line:
                // result.ChatroomId = updatedChatroomId;

                _db.Users.InsertOne(result);
            }
            else
            {
                throw new KeyNotFoundException("User was not found. ID: " + user.Id);
            }
        }

        public void DeleteUser(Guid id)
        {
            var result = _db.Users.FindOneAndDelete(u => u.Guid == id);
            if (result == null)
            {
                throw new KeyNotFoundException("User was not found. ID: " + id);
            }
        }
    }
}