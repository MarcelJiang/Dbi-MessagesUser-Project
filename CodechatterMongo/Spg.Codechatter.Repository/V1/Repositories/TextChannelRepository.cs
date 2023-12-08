using Spg.Codechatter.Domain.V1.Dtos.TextChannel;
using Spg.Codechatter.Domain.V1.Model;
using Spg.Codechatter.Infrastructure;
using Spg.Codechatter.Repository.V1.Interfaces.TextChannelRepository;

namespace Spg.Codechatter.Repository.V1.Repositories;

public class TextChannelRepository: IReadTextChannelRepository, IModifyTextChannelRepository
{
    private readonly CodechatterContext _db;

    public TextChannelRepository(CodechatterContext db)
    {
        _db = db;
    }

    public TextChannel GetTextChannelById(Guid id)
    {

        try
        {
            return _db.TextChannels.First(t => t.Guid == id);
        }
        catch (InvalidOperationException ex)
        {
            throw new KeyNotFoundException("Text Channel was not found. ID: " + id);
        }
    }
    
    public IEnumerable<TextChannel> GetAllTextChannels()
    {
        return _db.TextChannels;
    }

    public IEnumerable<TextChannel> GetTextChannelsByChatroomId(Guid chatroomId)
    {
        return _db.TextChannels.Where(e => e.ChatroomId == chatroomId);
    }

    public void AddTextChannel(TextChannel textChannel)
    {
        _db.TextChannels.Add(textChannel);
        _db.SaveChanges();
    }

    public void UpdateTextChannel(UpdateTextChannelDto textChannel)
    {
        try
        {
            TextChannel result = _db.TextChannels.First(t => t.Guid == textChannel.Guid);
        
            _db.TextChannels.Remove(result);
            _db.TextChannels.Add(new TextChannel(textChannel.Name, textChannel.ChatroomId){Guid = textChannel.Guid});
            _db.SaveChanges();
        }
        catch (InvalidOperationException ex)
        {
            throw new KeyNotFoundException("Text Channel was not found. UserId: " + textChannel.Guid);
        }
    }

    public void DeleteTextChannel(Guid textChannelId)
    {
        try
        {
            TextChannel textChannel = _db.TextChannels.First(t => t.Guid == textChannelId);
            
            _db.Remove(textChannel);
            _db.SaveChanges();
        }
        catch (InvalidOperationException ex)
        {
            throw new KeyNotFoundException("Text Channel was not found. UserId: " + textChannelId);
        }
       
    }
}