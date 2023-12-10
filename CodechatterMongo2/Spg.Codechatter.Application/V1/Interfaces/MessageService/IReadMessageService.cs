using Spg.Codechatter.Domain.V1.Dtos.Message;

namespace Spg.Codechatter.Application.V1.Interfaces.MessageService;

public interface IReadMessageService
{
    IEnumerable<ReadMessageDto> GetAllMessages();

    ReadMessageDto GetMessageById(Guid id);
    IEnumerable<ReadMessageDto> GetMessagesByMemberId(Guid userId, Guid chatroomId);

    IEnumerable<ReadMessageDto> GetMessagesByTextChannelId(Guid textChannelId);
}