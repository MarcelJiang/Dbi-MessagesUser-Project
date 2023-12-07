using Spg.Codechatter.Domain.V1.Dtos.TextChannel;

namespace Spg.Codechatter.Application.V1.Interfaces.TextChannelService;

public interface IReadTextChannelService
{
    IEnumerable<ReadTextChannelDto> GetAllTextChannels();

    ReadTextChannelDto GetTextChannelById(Guid id);
    
    IEnumerable<ReadTextChannelDto> GetTextChannelsByChatroomId(Guid chatroomId);
}