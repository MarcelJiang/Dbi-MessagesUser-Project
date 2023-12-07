using Spg.Codechatter.Domain.V1.Model;

namespace Spg.Codechatter.Repository.V1.Interfaces.ChatroomRepository;

public interface IReadChatroomRepository
{
    IEnumerable<Chatroom> GetAllChatrooms();
    
    Chatroom GetChatroomById(Guid guid);
}