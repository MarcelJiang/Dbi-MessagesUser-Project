using Spg.Codechatter.Domain.V1.Dtos.User;
using Spg.Codechatter.Domain.V1.Model;
using Spg.Codechatter.Infrastructure;
using Spg.Codechatter.Repository.V1.Interfaces.UserRepository;


namespace Spg.Codechatter.Repository.V1.Repositories;

public class UserRepository: IReadUserRepository, IModifyUserRepository
{
    private readonly CodechatterContext _db;

    public UserRepository(CodechatterContext db)
    {
        _db = db;
    }

    public User GetUserById(Guid guid)
    {
        try
        {
            return _db.Users.First(u => u.Guid == guid);
        }
        catch (InvalidOperationException ex)
        {
            throw new KeyNotFoundException("User was not found. ID: " + guid);
        }

        
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _db.Users;
    }

    public void AddUser(User user)
    {
        _db.Users.Add(user);
        _db.SaveChanges();
    }

    public void UpdateUser(UpdateUserDto user)
    {
        try
        {
            User result = _db.Users.First(u => u.Guid == user.Guid);

            // Update the properties of the existing user
            result.EmailAddress = user.EmailAddress;
            result.Username = user.Username;
            result.Password = user.Password;

            // If ChatroomId is supposed to be updated as well, add the following line:
            // result.ChatroomId = updatedChatroomId;

            _db.SaveChanges();
        }
        catch (InvalidOperationException ex)
        {
            throw new KeyNotFoundException("User was not found. ID: " + user.Guid);
        }
    }

    public void DeleteUser(Guid id)
    {
        try
        {
            User user = _db.Users.First(u => u.Guid == id);
            
            _db.Users.Remove(user);
            _db.SaveChanges();
        }
        catch (InvalidOperationException e)
        {
            throw new KeyNotFoundException("User was not found. ID: " + id);;
        }

         
    }
}