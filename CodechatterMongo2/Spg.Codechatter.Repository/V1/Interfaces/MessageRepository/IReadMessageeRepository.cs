using Spg.Codechatter.Domain.V1.Model;
using Spg.Codechatter.Domain.V1.Dtos.Message;

namespace Spg.Codechatter.Repository.V1.Interfaces.MessageRepository;

public interface IReadMessageRepository
{
    IEnumerable<Message> GetAllMessages();
    
    Message GetMessageById(Guid id);
    
    IEnumerable<Message> GetMessagesByMemberId(Guid userId, Guid chatroomId);
    
    IEnumerable<Message> GetMessagesByTextChannelId(Guid textChannelId);

    IEnumerable<UserWithMessagesDto> GetAllUsersWithMessages();
    
    IEnumerable<UserWithMessagesDto> UserMessagesFilterByDate();
    
    

}