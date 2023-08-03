using Back_End_Capstone_MS.Models;
using System.Collections.Generic;

namespace Back_End_Capstone_MS.Repositories
{
    public interface IRecipeLikeRepository
    {
        List<RecipeLike> GetByRecipeId(int recipeId);
        void Add(RecipeLike recipeLike);
        void Delete(int id);
    }
}
