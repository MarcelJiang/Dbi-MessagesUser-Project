using Spg.Codechatter.Domain.V1.Dtos.User;
using Spg.Codechatter.Domain.V1.Model;

namespace Spg.Codechatter.Repository.V1.Interfaces.UserRepository;

public interface IModifyUserRepository
{
    void AddUser(User user);
    
    void UpdateUser(UpdateUserDto user);
    
    void DeleteUser(Guid id);
}