using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Back_End_Capstone_MS.Models;
using Microsoft.AspNetCore.Identity;
using Back_End_Capstone_MS.Utils;

namespace Back_End_Capstone_MS.Repositories
{
    public class RecipeIngredientRepository : BaseRepository, IRecipeIngredientRepository
    {
        public RecipeIngredientRepository(IConfiguration configuration) : base(configuration) { }
        public void Add(RecipeIngredient ri)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO RecipeIngredient (RecipeId, IngredientId, Amount)
                        OUTPUT INSERTED.Id
                        VALUES (@recipeId, @ingredientId, @amount)";
                    DbUtils.AddParameter(cmd, "@recipeId", ri.RecipeId);
                    DbUtils.AddParameter(cmd, "@IngredientId", ri.IngredientId);
                    DbUtils.AddParameter(cmd, "@amount", ri.Amount);
                    ri.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM RecipeIngredient WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<RecipeIngredient> GetByRecipeId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, RecipeId, IngredientId, Amount
                        FROM RecipeIngredient
                        WHERE RecipeId = @recipeId";
                    DbUtils.AddParameter(cmd, "@recipeId", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        var recipeIngredients = new List<RecipeIngredient>();
                        while (reader.Read())
                        {
                            recipeIngredients.Add(new RecipeIngredient()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                RecipeId = DbUtils.GetInt(reader, "RecipeId"),
                                IngredientId = DbUtils.GetInt(reader, "IngredientId"),
                                Amount = DbUtils.GetString(reader, "Amount")
                            });
                        }
                        return recipeIngredients;
                    }
                }
            }
        }
    }
}
