using Back_End_Capstone_MS.Models;
using System.Collections.Generic;
namespace Back_End_Capstone_MS.Repositories
{
    public interface IRecipeTagRepository
    {
        List<RecipeTag> GetByRecipeId(int recipeId);
        void Add(RecipeTag recipeTag);
        void Delete(int id);
    }
}
