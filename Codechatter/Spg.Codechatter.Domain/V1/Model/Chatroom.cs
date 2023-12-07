namespace Spg.Codechatter.Domain.V1.Model;

public class Chatroom
{
    public int Id { get; init; }
    public Guid Guid { get; init; } = Guid.NewGuid();

    public string Name { get; set; } = default!;

    private List<TextChannel> _textChannels = new();
    public virtual IReadOnlyList<TextChannel> TextChannels => _textChannels;
    
    protected Chatroom() { }

    public Chatroom(string name)
    {
        Name = name;
    }

    public void AddTextChannel(TextChannel textChannel)
    {
        if (textChannel is not null)
        {
            if (!_textChannels.Any(c => c.Name.Equals(textChannel.Name)))
            {
                _textChannels.Add(textChannel);
            }
            else
            {
                throw new ArgumentException("textChannel with name " + textChannel.Name + " already exists");
            }

        }
        else
        {
            throw new ArgumentNullException(nameof(textChannel));
        }
    }

    public void RemoveTextChannel(TextChannel textChannel)
    {
        if (textChannel is not null)
        {
            if (_textChannels.Contains(textChannel))
            {
                _textChannels.Remove(textChannel);
            }
            else
            {
                throw new ArgumentException("The given TextChannel is not within the Chatrooms List");
            }
        }
        else
        {
            throw new ArgumentNullException("TextChannel cannot be null");
        }
    }

}