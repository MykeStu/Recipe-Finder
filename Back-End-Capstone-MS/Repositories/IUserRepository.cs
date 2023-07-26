using Back_End_Capstone_MS.Models;
using System.Collections.Generic;

namespace Back_End_Capstone_MS.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetByFireBaseUserId(string fireBaseUserId);
        User GetById(int id);
        void Update(User user);
        void Add(User user);
    }
}
