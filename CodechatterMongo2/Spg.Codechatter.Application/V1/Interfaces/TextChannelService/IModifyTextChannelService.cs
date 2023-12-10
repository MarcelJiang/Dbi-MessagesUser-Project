using Spg.Codechatter.Domain.V1.Dtos.TextChannel;

namespace Spg.Codechatter.Application.V1.Interfaces.TextChannelService;

public interface IModifyTextChannelService
{
    ReadTextChannelDto AddTextChannel(CreateTextChannelDto textChannel);

    void UpdateTextChannel(UpdateTextChannelDto textChannel);

    void DeleteTextChannel(Guid textChannelId);
}