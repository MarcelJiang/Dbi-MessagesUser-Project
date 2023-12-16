using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Spg.Codechatter.Domain.V1.Dtos.Message;
using Spg.Codechatter.Domain.V1.Model;
using Spg.Codechatter.Infrastructure;
using Spg.Codechatter.Repository.V1.Interfaces.MessageRepository;

namespace Spg.Codechatter.Repository.V1.Repositories
{
    public class MessageRepository : IReadMessageRepository, IModifyMessageRepository
    {
        private readonly CodechatterContext _db;

        public MessageRepository(CodechatterContext db)
        {
            _db = db;
        }

        public Message GetMessageById(Guid id)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                return _db.Messages.First(m => m.Guid == id);
            }
            catch (InvalidOperationException ex)
            {
                throw new KeyNotFoundException("Message was not found. ID: " + id);
            }
            finally
            {
                stopwatch.Stop();
                Console.WriteLine($"Get Message by ID: Elapsed Time for Data Operation: {stopwatch.ElapsedMilliseconds} ms");
            }
        }

        public IEnumerable<Message> GetAllMessages()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var messages = _db.Messages.ToList();

            stopwatch.Stop();
            Console.WriteLine($"Get all Messages: Elapsed Time for Data Operation: {stopwatch.ElapsedMilliseconds} ms");

            return messages;
        }

        public IEnumerable<Message> GetMessagesByMemberId(Guid userId, Guid chatroomId)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var messages = _db.Messages
                .Where(e => e.UserId == userId && e.ChatroomId == chatroomId)
                .Include(m => m.UserId)
                .ToList();

            stopwatch.Stop();
            Console.WriteLine($"Get Messages by Member ID: Elapsed Time for Data Operation: {stopwatch.ElapsedMilliseconds} ms");

            return messages;
        }

        public IEnumerable<Message> GetMessagesByTextChannelId(Guid textChannelId)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var messages = _db.Messages
                .Where(e => e.TextChannelId == textChannelId)
                .Include(m => m.UserId)
                .ToList();

            stopwatch.Stop();
            Console.WriteLine($"Get Messages by Text Channel ID: Elapsed Time for Data Operation: {stopwatch.ElapsedMilliseconds} ms");

            return messages;
        }

        public void AddMessage(Message message)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                _db.Messages.Add(message);
                _db.SaveChanges();
            }
            finally
            {
                stopwatch.Stop();
                Console.WriteLine($"Adding Message: Elapsed Time for Data Operation: {stopwatch.ElapsedMilliseconds} ms");
            }
        }

        public void UpdateMessage(UpdateMessageDto message)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                Message result = _db.Messages.First(m => m.Guid == message.Guid);
                _db.Messages.Remove(result);
                _db.Messages.Add(new Message(message.Content, message.UserId, message.ChatroomId, message.TextChannelId) { Guid = message.Guid });
                _db.SaveChanges();
            }
            catch (InvalidOperationException ex)
            {
                throw new KeyNotFoundException("Message was not found. ID: " + message.Guid);
            }
            finally
            {
                stopwatch.Stop();
                Console.WriteLine($"Update Message: Elapsed Time for Data Operation: {stopwatch.ElapsedMilliseconds} ms");
            }
        }

        public void DeleteMessage(Guid id)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                Message? message = _db.Messages.First(m => m.Guid == id);
                _db.Messages.Remove(message);
                _db.SaveChanges();
            }
            catch (InvalidOperationException ex)
            {
                throw new KeyNotFoundException("Message was not found. ID: " + id);
            }
            finally
            {
                stopwatch.Stop();
                Console.WriteLine($"Delete Message: Elapsed Time for Data Operation: {stopwatch.ElapsedMilliseconds} ms");
            }
        }

        public IEnumerable<UserWithMessagesDto> GetAllUsersWithMessages()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<UserWithMessagesDto> usersWithMessages = new List<UserWithMessagesDto>();
            try
            {
                List<User> allUsers = _db.Users.ToList();
                foreach (User user in allUsers)
                {
                    List<ReadMessageDto> userMessages = _db.Messages
                        .Where(m => m.UserId == user.Guid)
                        .ToList()
                        .Select(message => new ReadMessageDto(
                            message.Guid,
                            message.Content,
                            message.DateAndTime,
                            message.TextChannelId,
                            message.UserId,
                            message.ChatroomId))
                        .ToList();

                    UserWithMessagesDto userWithMessagesDto = new UserWithMessagesDto
                    {
                        UserId = user.Guid,
                        UserName = user.Username,
                        Messages = userMessages
                    };

                    usersWithMessages.Add(userWithMessagesDto);
                }
            }
            finally
            {
                stopwatch.Stop();
                Console.WriteLine($"Get All Users with their Messages: Elapsed Time for Data Operation: {stopwatch.ElapsedMilliseconds} ms");
            }

            return usersWithMessages;
        }

        public IEnumerable<UserWithMessagesDto> UserMessagesFilterByDate()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<UserWithMessagesDto> usersWithMessagesSortByDate = new List<UserWithMessagesDto>();
            try
            {
                List<User> allUsers = _db.Users.ToList();
                foreach (User user in allUsers)
                {
                    List<ReadMessageDto> userMessages = _db.Messages
                        .Where(m => m.UserId == user.Guid)
                        .AsEnumerable() // or .ToList()
                        .OrderBy(m => m.DateAndTime)
                        .Select(message => new ReadMessageDto(
                            message.Guid,
                            message.Content,
                            message.DateAndTime,
                            message.TextChannelId,
                            message.UserId,
                            message.ChatroomId))
                        .ToList();

                    UserWithMessagesDto userWithMessagesDto = new UserWithMessagesDto
                    {
                        UserId = user.Guid,
                        UserName = user.Username,
                        Messages = userMessages
                    };

                    usersWithMessagesSortByDate.Add(userWithMessagesDto);
                }
            }
            finally
            {
                stopwatch.Stop();
                Console.WriteLine($"Get All Users with their Messages Sort by Date: Elapsed Time for Data Operation: {stopwatch.ElapsedMilliseconds} ms");
            }

            return usersWithMessagesSortByDate;
        }

        public IEnumerable<UserMessageCountDto> MessagesCountPerUser()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<UserMessageCountDto> countMessages = new List<UserMessageCountDto>();
            try
            {
                List<User> allUsers = _db.Users.ToList();
                foreach (User user in allUsers)
                {
                    long messageCount = _db.Messages.Count(m => m.UserId == user.Guid);

                    UserMessageCountDto userCountMessages = new UserMessageCountDto()
                    {
                        UserId = user.Guid,
                        UserName = user.Username,
                        MessageCount = messageCount
                    };

                    countMessages.Add(userCountMessages);
                }
            }
            finally
            {
                stopwatch.Stop();
                Console.WriteLine($"Get All Users with their Messages Count Sort by Date: Elapsed Time for Data Operation: {stopwatch.ElapsedMilliseconds} ms");
            }

            return countMessages;
        }
    }
}
