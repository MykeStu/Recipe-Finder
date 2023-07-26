using Back_End_Capstone_MS.Models;
using System.Collections.Generic;

namespace Back_End_Capstone_MS.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAll();
        Tag GetById(int id);
        void Add(Tag tag);
        void Update(Tag tag);
        void Delete(int id);
    }
}
