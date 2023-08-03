using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Back_End_Capstone_MS.Models;
using Back_End_Capstone_MS.Utils;

namespace Back_End_Capstone_MS.Repositories
{
    public class RecipeTagRepository : BaseRepository, IRecipeTagRepository
    {
        public RecipeTagRepository(IConfiguration config) : base(config) { }
        
        public List<RecipeTag> GetByRecipeId(int recipeId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, RecipeId, TagId
                        FROM RecipeTag
                        WHERE RecipeId = @recipeId";
                    DbUtils.AddParameter(cmd, "@recipeId", recipeId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        var recipeTags = new List<RecipeTag>();
                        while (reader.Read())
                        {
                            var recipeTag = new RecipeTag()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                RecipeId = DbUtils.GetInt(reader, "RecipeId"),
                                TagId = DbUtils.GetInt(reader, "TagId")
                            };
                            recipeTags.Add(recipeTag);
                        }
                        return recipeTags;
                    }
                }
            }
        }
        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM RecipeTag where Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Add(RecipeTag recipeTag)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO RecipeTag (RecipeId, TagId)
                        OUTPUT INSERTED.Id
                        VALUES (@recipeId, @tagId)";
                    DbUtils.AddParameter(cmd, "@recipeId", recipeTag.RecipeId);
                    DbUtils.AddParameter(cmd, "@tagId", recipeTag.TagId);
                    recipeTag.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
