using System;
using System.Collections.Generic;
using Spg.Codechatter.Domain.V1.Dtos.Chatroom;

namespace Spg.Codechatter.Application.V1.Interfaces.ChatroomService;

public interface IModifyChatroomService
{
    ReadChatroomDto AddChatroom(CreateChatroomDto chatroom);

    void UpdateChatroom(UpdateChatroomDto chatroom);

    void DeleteChatroom(Guid id);
}