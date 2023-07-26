using Back_End_Capstone_MS.Models;
using System.Collections.Generic;

namespace Back_End_Capstone_MS.Repositories
{
    public interface IIngredientRepository
    {
        void Add(Ingredient ingredient);
        void Delete(int id);
        List<Ingredient> GetAll();
        List<Ingredient> Search(string searchParameters);
    }
}
