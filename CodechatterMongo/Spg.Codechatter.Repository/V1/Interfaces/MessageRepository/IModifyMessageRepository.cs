

using Spg.Codechatter.Domain.V1.Dtos.Message;
using Spg.Codechatter.Domain.V1.Model;

namespace Spg.Codechatter.Repository.V1.Interfaces.MessageRepository;

public interface IModifyMessageRepository
{
    void AddMessage(Message message);
    
    void UpdateMessage(UpdateMessageDto message);
    
    void DeleteMessage(Guid id);
}