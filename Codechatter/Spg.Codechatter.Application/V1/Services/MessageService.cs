using Spg.Codechatter.Application.V1.Interfaces.MessageService;
using Spg.Codechatter.Domain.V1.Dtos.Message;
using Spg.Codechatter.Domain.V1.Model;
using Spg.Codechatter.Repository.V1.Interfaces.MessageRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spg.Codechatter.Application.V1.Services
{
    public class MessageService : IReadMessageService, IModifyMessageService
    {
        private readonly IReadMessageRepository _readMessageRepository;
        private readonly IModifyMessageRepository _modifyMessageRepository;

        public MessageService(IReadMessageRepository readMessageRepository, IModifyMessageRepository modifyMessageRepository)
        {
            _readMessageRepository = readMessageRepository;
            _modifyMessageRepository = modifyMessageRepository;
        }

        public IEnumerable<ReadMessageDto> GetAllMessages()
        {
            return _readMessageRepository.GetAllMessages().Select(m =>
                new ReadMessageDto(m.Guid, m.Content, m.DateAndTime, m.TextChannelId, m.UserId, m.ChatroomId));
        }

        public ReadMessageDto GetMessageById(Guid id)
        {
            Message message = _readMessageRepository.GetMessageById(id);

            return new ReadMessageDto(message.Guid, message.Content, message.DateAndTime, message.TextChannelId,
                message.UserId, message.ChatroomId);
        }

        public IEnumerable<ReadMessageDto> GetMessagesByMemberId(Guid userId, Guid chatroomId)
        {
            return _readMessageRepository.GetMessagesByMemberId(userId, chatroomId).Select(m =>
                new ReadMessageDto(m.Guid, m.Content, m.DateAndTime, m.TextChannelId, m.UserId, m.ChatroomId));
        }

        public IEnumerable<ReadMessageDto> GetMessagesByTextChannelId(Guid textChannelId)
        {
            return _readMessageRepository.GetMessagesByTextChannelId(textChannelId).Select(m =>
                new ReadMessageDto(m.Guid, m.Content, m.DateAndTime, m.TextChannelId, m.UserId, m.ChatroomId));
        }

        public ReadMessageDto AddMessage(CreateMessageDto message)
        {
            Message m = new Message(message.Content, message.UserId, message.ChatroomId, message.TextChannelId);

            _modifyMessageRepository.AddMessage(m);

            return new ReadMessageDto(m.Guid, m.Content, m.DateAndTime, m.TextChannelId, m.UserId, m.ChatroomId);
        }

        public void UpdateMessage(UpdateMessageDto message)
        {
            _modifyMessageRepository.UpdateMessage(message);
        }

        public void DeleteMessage(Guid id)
        {
            _modifyMessageRepository.DeleteMessage(id);
        }

        public IEnumerable<UserWithMessagesDto> GetAllUsersWithMessages()
        {
            return _readMessageRepository.GetAllUsersWithMessages().Select(user => new UserWithMessagesDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Messages = user.Messages
            })
            .ToList();
        }

        public IEnumerable<UserWithMessagesDto> UserMessagesFilterByDate()
        {
            return _readMessageRepository.UserMessagesFilterByDate().Select(user => new UserWithMessagesDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Messages = user.Messages
            })
            .ToList();
        }

        public IEnumerable<UserMessageCountDto> MessagesCountPerUser()
        {
            return _readMessageRepository.MessagesCountPerUser().Select(user => new UserMessageCountDto()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                MessageCount = user.MessageCount
            })
            .ToList();

        }
    }
}
