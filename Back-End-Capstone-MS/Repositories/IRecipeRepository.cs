using Back_End_Capstone_MS.Models;
using System.Collections.Generic;

namespace Back_End_Capstone_MS.Repositories
{
    public interface IRecipeRepository
    {
        List<Recipe> GetAll();
        List<Recipe> GetByUserId(int userId);
        Recipe GetById(int id);
        void Update(Recipe recipe);
        void Delete(int id);
        void Create(Recipe recipe);
    }
}
