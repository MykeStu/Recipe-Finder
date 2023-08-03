using Back_End_Capstone_MS.Models;
using Back_End_Capstone_MS.Utils;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.ComponentModel;

namespace Back_End_Capstone_MS.Repositories
{
    public class RecipeRepository : BaseRepository, IRecipeRepository
    {
        public RecipeRepository(IConfiguration configuration) : base(configuration) { }
        public void Create(Recipe recipe)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Recipe (UserId, Difficulty, [Name], Instructions, Ingredients, DateCreated, ImageUrl)
                        OUTPUT INSERTED.Id
                        VALUES (@userId, @difficulty, @name, @instructions, @ingredients, @dateCreated, @imageUrl)";
                    DbUtils.AddParameter(cmd, "@userId", recipe.UserId);
                    DbUtils.AddParameter(cmd, "@Difficulty", recipe.Difficulty);
                    DbUtils.AddParameter(cmd, "@name", recipe.Name);
                    DbUtils.AddParameter(cmd, "@instructions", recipe.Instructions);
                    DbUtils.AddParameter(cmd, "@ingredients", recipe.Ingredients);
                    DbUtils.AddParameter(cmd, "@dateCreated", recipe.DateCreated);
                    DbUtils.AddParameter(cmd, "@imageUrl", recipe.ImageUrl);
                    recipe.Id = (int)cmd.ExecuteScalar();
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
                    cmd.CommandText = "DELETE FROM Recipe WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        //Need to create a COUNT(RecipeLike) for this
        public List<Recipe> GetAll()
        {
            using ( var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, UserId, Difficulty, [Name], Instructions, Ingredients, DateCreated, ImageUrl
                        FROM Recipe";
                    using (var reader = cmd.ExecuteReader())
                    {
                        var recipes = new List<Recipe>();
                        var recipe = new Recipe();
                        while (reader.Read())
                        {
                            recipe.Id = DbUtils.GetInt(reader, "Id");
                            recipe.UserId = DbUtils.GetInt(reader, "UserId");
                            recipe.Difficulty = DbUtils.GetInt(reader, "Difficulty");
                            recipe.Name = DbUtils.GetString(reader, "Name");
                            recipe.Instructions = DbUtils.GetString(reader, "Instructions");
                            recipe.Ingredients = DbUtils.GetString(reader, "Ingredients");
                            recipe.DateCreated = DbUtils.GetDateTime(reader, "DateCreated");
                            recipe.ImageUrl = DbUtils.GetString(reader, "ImageUrl");
                            
                        recipes.Add(recipe);
                        }
                        return recipes;
                    }
                }
            }
        }
        public List<Recipe> GetByUserId(int userId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, Difficulty, [Name], Instructions, Ingredients, DateCreated, ImageUrl FROM Recipe
                        WHERE UserId = @userId";
                    DbUtils.AddParameter(cmd, "@userId", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        var recipes = new List<Recipe>();
                        while (reader.Read())
                        {
                            var recipe = new Recipe()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Difficulty = DbUtils.GetInt(reader, "Difficulty"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Instructions = DbUtils.GetString(reader, "Instructions"),
                                Ingredients = DbUtils.GetString(reader, "Ingredients"),
                                DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl")
                            };
                            recipes.Add(recipe);
                        }
                        return recipes;
                    }
                }
            }
        }
        //comments will be a separate query from the comment repository
        public Recipe GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT r.Id as rId, r.UserId, r.Difficulty, r.[Name], r.Instructions, r.Ingredients, r.DateCreated, r.ImageUrl as RecipeImage,
                        u.DisplayName, u.Id as uId
                        FROM Recipe r            
                        WHERE r.Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    using (var reader = cmd.ExecuteReader()) 
                    {
                        var recipe = new Recipe();
                        if (reader.Read())
                        {
                            
                            recipe.Id = DbUtils.GetInt(reader, "rId");
                            recipe.UserId = DbUtils.GetInt(reader, "UserId");
                            recipe.Difficulty = DbUtils.GetInt(reader, "Difficulty");
                            recipe.Name = DbUtils.GetString(reader, "Name");
                            recipe.Instructions = DbUtils.GetString(reader, "Instructions");
                            recipe.Ingredients = DbUtils.GetString(reader, "Ingredients");
                            recipe.DateCreated = DbUtils.GetDateTime(reader, "DateCreated");
                            recipe.ImageUrl = DbUtils.GetString(reader, "RecipeImage");
                        }
                        return recipe;
                    }
                }
            }
        }

        public void Update(Recipe recipe)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Recipe
                        SET (Difficulty = @difficulty,
                            [Name] = @name,
                            Ingredients = @ingredients,
                            Instructions = @instructions,
                            ImageUrl = @imageUrl)
                        WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@name", recipe.Name);
                    DbUtils.AddParameter(cmd, "@ingredients", recipe.Ingredients);
                    DbUtils.AddParameter(cmd, "@instructions", recipe.Instructions);
                    DbUtils.AddParameter(cmd, "@imageUrl", recipe.ImageUrl);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
