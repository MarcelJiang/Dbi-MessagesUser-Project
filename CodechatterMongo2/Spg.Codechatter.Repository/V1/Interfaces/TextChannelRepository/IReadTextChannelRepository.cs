using Spg.Codechatter.Domain.V1.Model;

namespace Spg.Codechatter.Repository.V1.Interfaces.TextChannelRepository;

public interface IReadTextChannelRepository
{
    IEnumerable<TextChannel> GetAllTextChannels();

    TextChannel GetTextChannelById(Guid id);
    
    IEnumerable<TextChannel> GetTextChannelsByChatroomId(Guid chatroomId);
}