using Back_End_Capstone_MS.Models;
using System.Collections.Generic;
namespace Back_End_Capstone_MS.Repositories
{
    public interface IRecipeIngredientRepository
    {
        void Add(RecipeIngredient ri);
        void Delete(int id);
        List<RecipeIngredient> GetByRecipeId(int id);
    }
}
