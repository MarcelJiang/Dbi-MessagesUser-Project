using Spg.Codechatter.Application.V1.Interfaces.TextChannelService;
using Spg.Codechatter.Domain.V1.Dtos.TextChannel;
using Spg.Codechatter.Domain.V1.Model;
using Spg.Codechatter.Repository.V1.Interfaces.TextChannelRepository;

namespace Spg.Codechatter.Application.V1.Services;

public class TextChannelService : IReadTextChannelService, IModifyTextChannelService
{
    private readonly IReadTextChannelRepository _readTextChannelRepository;
    private readonly IModifyTextChannelRepository _modifyTextChannelRepository;

    public TextChannelService(IReadTextChannelRepository readTextChannelRepository, IModifyTextChannelRepository modifyTextChannelRepository)
    {
        _readTextChannelRepository = readTextChannelRepository;
        _modifyTextChannelRepository = modifyTextChannelRepository;
    }


    public IEnumerable<ReadTextChannelDto> GetAllTextChannels()
    {
        return _readTextChannelRepository.GetAllTextChannels().Select(t => new ReadTextChannelDto(t.Guid, t.Name, t.ChatroomId));
    }

    public ReadTextChannelDto GetTextChannelById(Guid id)
    {
        TextChannel t = _readTextChannelRepository.GetTextChannelById(id);

        return new ReadTextChannelDto(t.Guid, t.Name, t.ChatroomId);

    }

    public IEnumerable<ReadTextChannelDto> GetTextChannelsByChatroomId(Guid id)
    {
        return _readTextChannelRepository.GetTextChannelsByChatroomId(id).Select(t => new ReadTextChannelDto(t.Guid, t.Name, t.ChatroomId));
    }

    public ReadTextChannelDto AddTextChannel(CreateTextChannelDto textChannel)
    {
        TextChannel t = new TextChannel(textChannel.Name, textChannel.ChatroomId);

        _modifyTextChannelRepository.AddTextChannel(t);

        return new ReadTextChannelDto(t.Guid, t.Name, t.ChatroomId);
    }

    public void UpdateTextChannel(UpdateTextChannelDto textChannel)
    {
        TextChannel t = new TextChannel(textChannel.Name, textChannel.ChatroomId);
        _modifyTextChannelRepository.UpdateTextChannel(textChannel);
    }

    public void DeleteTextChannel(Guid id)
    {
        _modifyTextChannelRepository.DeleteTextChannel(id);
    }
}