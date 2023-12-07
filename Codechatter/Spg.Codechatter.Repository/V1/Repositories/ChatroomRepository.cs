using Spg.Codechatter.Domain.V1.Dtos.Chatroom;
using Spg.Codechatter.Domain.V1.Model;
using Spg.Codechatter.Infrastructure;
using Spg.Codechatter.Repository.V1.Interfaces.ChatroomRepository;

namespace Spg.Codechatter.Repository.V1.Repositories;

public class ChatroomRepository: IReadChatroomRepository, IModifyChatroomRepository
{
    private readonly CodechatterContext _db;

    public ChatroomRepository(CodechatterContext db)
    {
        _db = db;
    }

    
    public Chatroom GetChatroomById(Guid guid)
    {

        try
        {
            return _db.Chatrooms.First(c => c.Guid == guid);
        }
        catch (InvalidOperationException ex)
        {
            throw new KeyNotFoundException("Chatroom was not found. ID: " + guid);
        }
        
    }

    public IEnumerable<Chatroom> GetAllChatrooms()
    {
        return _db.Chatrooms;
    }

    public void AddChatroom(Chatroom chatroom)
    {
        _db.Add(chatroom);
        _db.SaveChanges();
    }

    public void UpdateChatroom(UpdateChatroomDto chatroom)
    {
        try
        {
            Chatroom result = _db.Chatrooms.First(c => c.Guid == chatroom.Guid);
            
            _db.Chatrooms.Remove(result);
            _db.Chatrooms.Add(new Chatroom(chatroom.Name){Guid = chatroom.Guid});
            _db.SaveChanges();
        }
        catch (InvalidOperationException ex)
        {
            throw new KeyNotFoundException("Chatroom was not found. ID: " + chatroom.Guid);
        }
      
    }

    public void DeleteChatroom(Guid guid)
    {
        try
        {
            Chatroom chatroom = _db.Chatrooms.First(c => c.Guid == guid);
            
            _db.Chatrooms.Remove(chatroom);
            _db.SaveChanges();
        }
        catch (InvalidOperationException ex)
        {
            throw new KeyNotFoundException("Chatroom was not found. ID: " + guid);
        }

       
    }
}