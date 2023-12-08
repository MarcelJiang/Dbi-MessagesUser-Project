
namespace Spg.Codechatter.Domain.V1.Model;


public class Message
{
    public int Id { get; init; }
    public Guid Guid { get; init; } = Guid.NewGuid();
    
    public string Content { get; set; } = string.Empty;
    
    public DateTime DateAndTime { get; } = DateTime.Now;
    
    public virtual Guid TextChannelId { get; init; }

    public virtual Guid UserId { get; init; }
    public virtual Guid ChatroomId { get; init; }
    
    protected Message() { }
    
    public Message(string content, Guid userId, Guid chatroomId, Guid textChannelId)
    {
        Content = content;
        UserId = userId;
        ChatroomId = chatroomId;
        TextChannelId = textChannelId;
    }
    
    
}