using Spg.Codechatter.Domain.V1.Dtos.Message;
using Spg.Codechatter.Domain.V1.Model;
using Spg.Codechatter.Infrastructure;
using Spg.Codechatter.Repository.V1.Interfaces.MessageRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Diagnostics;
using Castle.Components.DictionaryAdapter.Xml;
using Spg.Codechatter.Domain.V1.Dtos.User;

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
                Stopwatch stopwatch = new Stopwatch();

                // Start the Stopwatch
                stopwatch.Start();
                try
                {
                    var message = _db.Messages.Find(m => m.Guid == id)?.FirstOrDefault();

                    if (message == null)
                    {
                        throw new KeyNotFoundException("Message was not found. ID: " + id);
                    }

                    return message;
            
                }
                finally
                {
                    stopwatch.Stop();

                    // Log or use the elapsed time as needed
                    Console.WriteLine($"Get Message by ID: Elapsed Time for Data Operation: {stopwatch.ElapsedMilliseconds} ms");
              
                }
            
            
        }

        public IEnumerable<Message> GetAllMessages()
        {   
            Stopwatch stopwatch = new Stopwatch();

            // Start the Stopwatch
            stopwatch.Start();
            try
            {
                return _db.Messages.AsQueryable();
            
            }
            finally
            {
                stopwatch.Stop();

                // Log or use the elapsed time as needed
                Console.WriteLine($"Get all Messages: Elapsed Time for Data Operation: {stopwatch.ElapsedMilliseconds} ms");
              
            }
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
            Stopwatch stopwatch = new Stopwatch();

            // Start the Stopwatch
            stopwatch.Start();
            try
            {
                _db.Messages.InsertOne(message);
            }
            finally
            {
                stopwatch.Stop();

                // Log or use the elapsed time as needed
                Console.WriteLine($"Adding Message: Elapsed Time for Data Operation: {stopwatch.ElapsedMilliseconds} ms");
              
            }
        }

        public void UpdateMessage(UpdateMessageDto message)
        {
            Stopwatch stopwatch = new Stopwatch();

            // Start the Stopwatch
            stopwatch.Start();

            try
            {
                var result = _db.Messages.FindOneAndDelete(m => m.Guid == message.Id);
                if (result != null)
                {
                    _db.Messages.InsertOne(
                        new Message(message.Content, message.UserId, message.ChatroomId, message.TextChannelId)
                            { Guid = message.Id });
                }
                else
                {
                    throw new KeyNotFoundException("Message was not found. ID: " + message.Id);
                }
            }
            finally
            {
                stopwatch.Stop();

                // Log or use the elapsed time as needed
                Console.WriteLine($"Update Message: Elapsed Time for Data Operation: {stopwatch.ElapsedMilliseconds} ms");
              
            }
        }

        public void DeleteMessage(Guid id)
        {      
            Stopwatch stopwatch = new Stopwatch();

            // Start the Stopwatch
            stopwatch.Start();
            try
            {
                var result = _db.Messages.FindOneAndDelete(m => m.Guid == id);
                if (result == null)
                {
                    throw new KeyNotFoundException("Message was not found. ID: " + id);
                }
            }
            finally
            {
                stopwatch.Stop();

                // Log or use the elapsed time as needed
                Console.WriteLine($"Delete Message: Elapsed Time for Data Operation: {stopwatch.ElapsedMilliseconds} ms");
            }    
            
            
        }

        public IEnumerable<UserWithMessagesDto> GetAllUsersWithMessages()
        
        {
            Stopwatch stopwatch = new Stopwatch();

            // Start the Stopwatch
            stopwatch.Start();
            
            // Fetch all users
            List<UserWithMessagesDto> usersWithMessages = new List<UserWithMessagesDto>();
            List<User> allUsers = _db.Users.Find(_ => true).ToList();
            try
            {
                foreach (User user in allUsers)
                {
                    // Fetch all messages for each user
                    List<ReadMessageDto> userMessages = _db.Messages
                        .Find(m => m.UserId == user.Guid)
                        .ToList()
                        .Select(message => new ReadMessageDto(
                            message.Guid,
                            message.Content,
                            message.DateAndTime,
                            message.TextChannelId,
                            message.UserId,
                            message.ChatroomId))
                        .ToList();

                    // Create a DTO representing the user with their messages
                    UserWithMessagesDto userWithMessages = new UserWithMessagesDto
                    {
                        UserId = user.Guid,
                        UserName = user.Username, // Assuming there's a UserName property in your User class
                        Messages = userMessages
                    };

                    // Add the user DTO to the list
                    usersWithMessages.Add(userWithMessages);
                }
            }
            finally
            {
                // Stop the Stopwatch
                stopwatch.Stop();

                // Log or use the elapsed time as needed
                Console.WriteLine($"Get All Users with their Messages: Elapsed Time for Data Operation: {stopwatch.ElapsedMilliseconds} ms");
            }
            

            return usersWithMessages;
        }
        
        public IEnumerable<UserWithMessagesDto>UserMessagesFilterByDate()
        {
            Stopwatch stopwatch = new Stopwatch();

            // Start the Stopwatch
            stopwatch.Start();
            
            // Fetch all users
            List<UserWithMessagesDto> usersWithMessagesSortByDate = new List<UserWithMessagesDto>();
            List<User> allUsers = _db.Users.Find(_ => true).ToList();
            try
            {
                foreach (User user in allUsers)
                {
                    // Fetch all messages for each user
                    List<ReadMessageDto> userMessages = _db.Messages
                        .Find(m => m.UserId == user.Guid)
                        .ToList()
                        .Select(message => new ReadMessageDto(
                            message.Guid,
                            message.Content,
                            message.DateAndTime,
                            message.TextChannelId,
                            message.UserId,
                            message.ChatroomId))
                        .OrderBy(message => message.DateAndTime)
                        .ToList();

                    // Create a DTO representing the user with their messages
                    UserWithMessagesDto userWithMessages = new UserWithMessagesDto
                    {
                        UserId = user.Guid,
                        UserName = user.Username, // Assuming there's a UserName property in your User class
                        Messages = userMessages
                    };

                    // Add the user DTO to the list
                    usersWithMessagesSortByDate.Add(userWithMessages);
                }
            }
            finally
            {
                // Stop the Stopwatch
                stopwatch.Stop();

                // Log or use the elapsed time as needed
                Console.WriteLine($"Get All Users with their Messages Sort by Date: Elapsed Time for Data Operation: {stopwatch.ElapsedMilliseconds} ms");
            }
            

            return usersWithMessagesSortByDate;
        }
        
        public IEnumerable<UserMessageCountDto> MessagesCountPerUser()
        {
            Stopwatch stopwatch = new Stopwatch();

            // Start the Stopwatch
            stopwatch.Start();

            // Fetch all users
            List<UserMessageCountDto> countMessages = new List<UserMessageCountDto>();
            List<User> allUsers = _db.Users.Find(_ => true).ToList();

            try
            {
                foreach (User user in allUsers)
                {
                    // Count messages within the specified date range for each user
                    long messageCount = _db.Messages
                        .CountDocuments(m => m.UserId == user.Guid);

                    // Create a DTO representing the user with their message count
                    UserMessageCountDto userCountMessages = new UserMessageCountDto()
                    {
                        UserId = user.Guid,
                        UserName = user.Username, // Assuming there's a UserName property in your User class
                        MessageCount = messageCount
                    };

                    // Add the user DTO to the list
                    countMessages.Add(userCountMessages);
                }
            }
            finally
            {
                // Stop the Stopwatch
                stopwatch.Stop();

                // Log or use the elapsed time as needed
                Console.WriteLine($"Get All Users with their Messages Count Sort by Date: Elapsed Time for Data Operation: {stopwatch.ElapsedMilliseconds} ms");
            }

            return countMessages;

        }
    }
    
    
        
        
    
}
