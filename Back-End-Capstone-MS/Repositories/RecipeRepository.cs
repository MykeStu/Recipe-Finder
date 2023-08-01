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
                        INSERT INTO Recipe (UserId, Difficulty, [Name], Instructions, Hidden, DateCreated, ImageUrl)
                        OUTPUT INSERTED.Id
                        VALUES (@userId, @difficulty, @name, @instructions, @hidden, @dateCreated, @imageUrl)";
                    DbUtils.AddParameter(cmd, "@userId", recipe.UserId);
                    DbUtils.AddParameter(cmd, "@Difficulty", recipe.Difficulty);
                    DbUtils.AddParameter(cmd, "@name", recipe.Name);
                    DbUtils.AddParameter(cmd, "@instructions", recipe.Instructions);
                    DbUtils.AddParameter(cmd, "@hidden", recipe.Hidden);
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
        public List<Recipe> GetAllPublic()
        {
            using ( var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT r.Id as rId, r.Difficulty, r.[Name], r.Instructions, r.DateCreated, r.ImageUrl as RecipeImage,
                        u.DisplayName, u.Id as uId, t.Name as TagName, i.Name as IngredientName                   
                        JOIN [User] u on r.UserId = u.Id
                        WHERE r.Hidden = 0";
                    using (var reader = cmd.ExecuteReader())
                    {
                        var recipe = new Recipe();
                        var recipes = new List<Recipe>();
                        while (reader.Read())
                        {
                            if(recipe.Id != DbUtils.GetInt(reader, "rId"))
                            {
                                recipe.Id = DbUtils.GetInt(reader, "rId");
                                recipe.Difficulty = DbUtils.GetInt(reader, "Difficulty");
                                recipe.Name = DbUtils.GetString(reader, "Name");
                                recipe.Instructions = DbUtils.GetString(reader, "Instructions");
                                recipe.DateCreated = DbUtils.GetDateTime(reader, "DateCreated");
                                recipe.ImageUrl = DbUtils.GetString(reader, "RecipeImage");
                                recipe.User = new User()
                                {
                                    Id = DbUtils.GetInt(reader, "uId"),
                                    DisplayName = DbUtils.GetString(reader, "DisplayName")
                                };
                            };
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
                        SELECT Id, Difficulty, [Name], Instructions, Hidden, DateCreated, ImageUrl FROM Recipe
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
                                Hidden = reader.GetBoolean(reader.GetOrdinal("Hidden")),
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

        public List<Recipe> GetByDifficulty(int difficulty)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT r.Id as rId, r.Difficulty, r.[Name], r.Instructions, r.DateCreated, r.ImageUrl as RecipeImage,
                        u.DisplayName, u.Id as uId FROM Recipe r
                        JOIN [User] u on r.UserId = u.Id
                        WHERE r.Hidden = 0 AND r.Difficulty = @difficulty";
                    DbUtils.AddParameter(cmd, "@difficulty", difficulty);
                    using (var reader = cmd.ExecuteReader())
                    {
                        var recipes = new List<Recipe>();
                        while (reader.Read())
                        {
                            var recipe = new Recipe()
                            {
                                Id = DbUtils.GetInt(reader, "rId"),
                                Difficulty = DbUtils.GetInt(reader, "Difficulty"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Instructions = DbUtils.GetString(reader, "Instructions"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                                ImageUrl = DbUtils.GetString(reader, "RecipeImage"),
                                User = new User()
                                {
                                    Id = DbUtils.GetInt(reader, "uId"),
                                    DisplayName = DbUtils.GetString(reader, "DisplayName")
                                }
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
                        SELECT r.Id as rId, r.Difficulty, r.[Name], r.Instructions, r.DateCreated, r.ImageUrl as RecipeImage,
                        u.DisplayName, u.Id as uId FROM Recipe r
                        JOIN [User] u on r.UserId = u.Id
                        WHERE r.Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    using (var reader = cmd.ExecuteReader()) 
                    {
                        var recipe = new Recipe();
                        while (reader.Read())
                        {
                            if(recipe.Id != DbUtils.GetInt(reader, "rId"))
                                {
                                recipe.Id = DbUtils.GetInt(reader, "rId");
                                recipe.Difficulty = DbUtils.GetInt(reader, "Difficulty");
                                recipe.Name = DbUtils.GetString(reader, "Name");
                                recipe.Instructions = DbUtils.GetString(reader, "Instructions");
                                recipe.DateCreated = DbUtils.GetDateTime(reader, "DateCreated");
                                recipe.ImageUrl = DbUtils.GetString(reader, "RecipeImage");
                                recipe.User = new User()
                                {
                                    Id = DbUtils.GetInt(reader, "uId"),
                                    DisplayName = DbUtils.GetString(reader, "DisplayName")                              
                                };
                                recipe.Ingredients.Add(new Ingredient()
                                {

                                });
                                recipe.Tags.Add(new Tag()
                                {

                                });
                                recipe.Comments.Add(new Comment()
                                {

                                });
                            }
                            else
                            {

                            }
                        }
                        return recipe;
                    }
                }
            }
        }

        public List<Recipe> GetByTag(int tagId)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Recipe recipe)
        {
            throw new System.NotImplementedException();
        }
        public List<Recipe> GetByIngredient(string ingredientName)
        {
            throw new System.NotImplementedException();
        }
    }
}
