using Spg.Codechatter.Domain.V1.Dtos.Message;
using Spg.Codechatter.Domain.V1.Model;

namespace Spg.Codechatter.Repository.V1.Interfaces.MessageRepository;

public interface IReadMessageRepository
{
    IEnumerable<Message> GetAllMessages();
    
    Message GetMessageById(Guid id);
    
    IEnumerable<Message> GetMessagesByMemberId(Guid userId, Guid chatroomId);
    
    IEnumerable<Message> GetMessagesByTextChannelId(Guid textChannelId);
    
    IEnumerable<UserWithMessagesDto> GetAllUsersWithMessages();
    
    IEnumerable<UserWithMessagesDto> UserMessagesFilterByDate();

    IEnumerable<UserMessageCountDto> MessagesCountPerUser();
    
}