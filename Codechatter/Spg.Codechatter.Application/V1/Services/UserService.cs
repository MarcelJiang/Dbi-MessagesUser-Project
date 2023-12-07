using Spg.Codechatter.Application.V1.Interfaces.UserService;
using Spg.Codechatter.Domain.V1.Dtos.User;
using Spg.Codechatter.Domain.V1.Model;
using Spg.Codechatter.Repository.V1.Interfaces.UserRepository;

namespace Spg.Codechatter.Application.V1.Services;

public class UserService: IReadUserService, IModifyUserService
{

    private readonly IReadUserRepository _readUserRepository;
    private readonly IModifyUserRepository _modifyUserRepository;

    public UserService(IReadUserRepository readUserRepository, IModifyUserRepository modifyUserRepository)
    {
        _readUserRepository = readUserRepository;
        _modifyUserRepository = modifyUserRepository;
    }

    public IEnumerable<ReadUserDto> GetAllUsers()
    {
        return _readUserRepository.GetAllUsers().Select(u => new ReadUserDto(u.Guid, u.EmailAddress, u.Username));
    }

    public ReadUserDto GetUserById(Guid id)
    {
        User u = _readUserRepository.GetUserById(id);
        
        return new ReadUserDto(u.Guid, u.EmailAddress, u.Username);
    }

    public ReadUserDto AddUser(CreateUserDto user)
    {
        User u = new User(user.ChatroomId, user.EmailAddress, user.Username, user.Password);
                
        _modifyUserRepository.AddUser(u);
        
        return new ReadUserDto(u.Guid, u.EmailAddress, u.Username);
    }

    public void UpdateUser(UpdateUserDto user)
    {
        _modifyUserRepository.UpdateUser(user);
    }

    public void DeleteUser(Guid id)
    {
        _modifyUserRepository.DeleteUser(id);
    }
}