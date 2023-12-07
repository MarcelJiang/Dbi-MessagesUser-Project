
using Spg.Codechatter.Domain.V1.Dtos.Chatroom;

namespace Spg.Codechatter.Application.V1.Interfaces.ChatroomService;

public interface IReadChatroomService
{
    IEnumerable<ReadChatroomDto> GetAllChatrooms();

    ReadChatroomDto GetChatroomById(Guid id);
}