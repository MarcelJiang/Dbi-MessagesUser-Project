using Spg.Codechatter.Application.V1.Interfaces.ChatroomService;
using Spg.Codechatter.Domain.V1.Dtos.Chatroom;
using Spg.Codechatter.Domain.V1.Model;
using Spg.Codechatter.Repository.V1.Interfaces.ChatroomRepository;

namespace Spg.Codechatter.Application.V1.Services;

public class ChatroomService: IReadChatroomService, IModifyChatroomService
{
    private readonly IReadChatroomRepository _readChatroomRepository;
    private readonly IModifyChatroomRepository _modifyChatroomRepository;

    public ChatroomService(IReadChatroomRepository readChatroomRepository , IModifyChatroomRepository modifyChatroomRepository)
    {
        _readChatroomRepository = readChatroomRepository;
        _modifyChatroomRepository = modifyChatroomRepository;
    }
    
    
    public IEnumerable<ReadChatroomDto> GetAllChatrooms()
    {
        return _readChatroomRepository.GetAllChatrooms().Select(c => new ReadChatroomDto(c.Guid, c.Name)) ;
    }

    public ReadChatroomDto GetChatroomById(Guid id)
    {
        Chatroom chatroom = _readChatroomRepository.GetChatroomById(id);
        
        return new ReadChatroomDto(chatroom.Guid, chatroom.Name);
    }

    public ReadChatroomDto AddChatroom(CreateChatroomDto chatroom)
    {
        Chatroom c = new Chatroom(chatroom.Name);
        
        _modifyChatroomRepository.AddChatroom(c);

        return new ReadChatroomDto(c.Guid, c.Name);
    }

    public void UpdateChatroom(UpdateChatroomDto chatroom)
    {
        _modifyChatroomRepository.UpdateChatroom(chatroom);
    }

    public void DeleteChatroom(Guid guid)
    {
        _modifyChatroomRepository.DeleteChatroom(guid);
    }
}