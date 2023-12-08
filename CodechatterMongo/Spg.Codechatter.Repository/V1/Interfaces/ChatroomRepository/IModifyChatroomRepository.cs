

using Spg.Codechatter.Domain.V1.Dtos.Chatroom;
using Spg.Codechatter.Domain.V1.Model;

namespace Spg.Codechatter.Repository.V1.Interfaces.ChatroomRepository;

public interface IModifyChatroomRepository
{
    void AddChatroom(Chatroom chatroom);
    
    void UpdateChatroom(UpdateChatroomDto chatroom);

    void DeleteChatroom(Guid id);
}