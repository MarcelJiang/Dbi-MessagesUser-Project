using Spg.Codechatter.Domain.V1.Dtos.Message;

namespace Spg.Codechatter.Application.V1.Interfaces.MessageService;

public interface IModifyMessageService
{
    ReadMessageDto AddMessage(CreateMessageDto message);

    void UpdateMessage(UpdateMessageDto message);

    void DeleteMessage(Guid id);
}