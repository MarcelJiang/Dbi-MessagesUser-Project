using System;
using System.Collections.Generic;
using Spg.Codechatter.Domain.V1.Model;

namespace Spg.Codechatter.Repository.V1.Interfaces.UserRepository
{
    public interface IReadUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(Guid id);
    }
}