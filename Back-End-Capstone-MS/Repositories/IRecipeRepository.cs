using Back_End_Capstone_MS.Models;
using System.Collections.Generic;

namespace Back_End_Capstone_MS.Repositories
{
    public interface IRecipeRepository
    {
        List<Recipe> GetAllPublic();
        List<Recipe> GetByDifficulty(int difficulty);
        List<Recipe> GetByTag(int tagId);

        List<Recipe> GetByIngredient(string ingredientName);
        List<Recipe> GetByUserId(int userId);
        Recipe GetById(int id);
        void Update(Recipe recipe);
        void Delete(int id);
        void Create(Recipe recipe);
    }
}
