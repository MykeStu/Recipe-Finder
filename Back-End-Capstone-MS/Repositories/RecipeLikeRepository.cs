using Microsoft.Extensions.Configuration;
using Back_End_Capstone_MS.Models;
using System.Collections.Generic;
using Back_End_Capstone_MS.Utils;

namespace Back_End_Capstone_MS.Repositories
{
    public class RecipeLikeRepository : BaseRepository, IRecipeLikeRepository
    {
        public RecipeLikeRepository(IConfiguration config) : base(config) { }

        public void Add(RecipeLike rl)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO RecipeLike (RecipeId, UserId)
                        OUTPUT INSERTED.Id
                        VALUES (@recipeID, @userId)";
                    DbUtils.AddParameter(cmd, "@recipeId", rl.RecipeId);
                    DbUtils.AddParameter(cmd, "@userId", rl.UserId);
                    rl.Id = (int)cmd.ExecuteScalar();
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
                    cmd.CommandText = "DELETE FROM RecipeLike WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<RecipeLike> GetByRecipeId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, RecipeId, UserId FROM RecipeLike WHERE RecipeId = @recipeId";
                    DbUtils.AddParameter(cmd, "@recipeId", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        var likes = new List<RecipeLike>();
                        while (reader.Read())
                        {
                            likes.Add(new RecipeLike()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                RecipeId = DbUtils.GetInt(reader, "RecipeId"),
                                UserId = DbUtils.GetInt(reader, "UserId")
                            });
                        }
                        return likes;
                    }
                }
            }
        }
    }
}
