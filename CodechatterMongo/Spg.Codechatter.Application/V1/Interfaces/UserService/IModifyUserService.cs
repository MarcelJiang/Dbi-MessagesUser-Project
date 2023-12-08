using Spg.Codechatter.Domain.V1.Dtos.User;

namespace Spg.Codechatter.Application.V1.Interfaces.UserService;

public interface IModifyUserService
{
    ReadUserDto AddUser(CreateUserDto user);

    void UpdateUser(UpdateUserDto user);

    void DeleteUser(Guid id);
}