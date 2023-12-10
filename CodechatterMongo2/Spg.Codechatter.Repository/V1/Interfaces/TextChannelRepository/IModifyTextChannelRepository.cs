using Spg.Codechatter.Domain.V1.Dtos.TextChannel;
using Spg.Codechatter.Domain.V1.Model;

namespace Spg.Codechatter.Repository.V1.Interfaces.TextChannelRepository;

public interface IModifyTextChannelRepository
{
    void AddTextChannel(TextChannel textChannel);
    
    void UpdateTextChannel(UpdateTextChannelDto textChannel);
    
    void DeleteTextChannel(Guid textChannelId);
}