using Spg.Codechatter.Domain.V1.Dtos.Message;
using Spg.Codechatter.Domain.V1.Model;
using Spg.Codechatter.Infrastructure;
using Spg.Codechatter.Repository.V1.Interfaces.MessageRepository;

namespace Spg.Codechatter.Repository.V1.Repositories;

public class MessageRepository: IReadMessageRepository, IModifyMessageRepository
{
    private readonly CodechatterContext _db;

    public MessageRepository(CodechatterContext db)
    {
        _db = db;
    }


    public Message GetMessageById(Guid id)
    {
        try
        {
            return _db.Messages.First(m => m.Guid == id);
        }
        catch (InvalidOperationException ex)
        {
            throw new KeyNotFoundException("Message was not found. ID: " + id);
        }
               
    }

    public IEnumerable<Message> GetAllMessages()
    {
        return _db.Messages;
    }

    public IEnumerable<Message> GetMessagesByMemberId(Guid userId, Guid chatroomId)
    {
        return _db.Messages.Where(e => e.UserId == userId && e.ChatroomId == chatroomId);
    }

    public IEnumerable<Message> GetMessagesByTextChannelId(Guid textChannelId)
    {
        return _db.Messages.Where(e => e.TextChannelId == textChannelId);
    }

    public void AddMessage(Message message)
    {
        _db.Messages.Add(message);
        _db.SaveChanges();
    }

    public void UpdateMessage(UpdateMessageDto message)
    {
        try
        {
            Message result = _db.Messages.First(m => m.Guid == message.Guid);
            
            _db.Messages.Remove(result);
            _db.Messages.Add(new Message(message.Content, message.UserId, message.ChatroomId, message.TextChannelId){Guid = message.Guid});
            _db.SaveChanges();
            
        }
        catch (InvalidOperationException ex)
        {
            throw new KeyNotFoundException("Message was not found. ID: "  + message.Guid);
        }
    }

    public void DeleteMessage(Guid id)
    {
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

    }
}