using Spg.Codechatter.Domain.V1.Dtos.User;

namespace Spg.Codechatter.Application.V1.Interfaces.UserService;

public interface IReadUserService
{
    IEnumerable<ReadUserDto> GetAllUsers();
    
    ReadUserDto GetUserById(Guid id);
}