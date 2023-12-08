
namespace Spg.Codechatter.Domain.V1.Model;

public class TextChannel
{
    public int Id { get; init; }
    public Guid Guid { get; init; } = Guid.NewGuid();
    public string Name { get; set; } = String.Empty;
    
    public virtual Guid ChatroomId { get; set; }
    //public Chatroom NavChatroom { get; set; } 
    // possible future implementation: List of banned words
    
    private List<Message> _messages = new();
    public virtual IReadOnlyList<Message> Messages => _messages;
    
    protected TextChannel() { }
    
    public TextChannel(string name, Guid chatroomId)
    {
        Name = name;
        ChatroomId = chatroomId;
    }
    
    public void AddMessage(Message message)
    {
        // Possible future implementation: Add filter for banned words
        
        if (message is not null)
        {
            _messages.Add(message);
        }
        else
        {
            throw new ArgumentNullException(nameof(message));
        }
    }
    public void RemoveMessage(Message message)
    {
        if (message is not null)
        {
            _messages.Remove(message);
        }
        else
        {
            throw new ArgumentNullException(nameof(message));
        }
    }
    
    
}