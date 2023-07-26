using Back_End_Capstone_MS.Models;
using Back_End_Capstone_MS.Utils;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Back_End_Capstone_MS.Repositories
{
    public class IngredientRepository : BaseRepository, IIngredientRepository
    {
        public IngredientRepository(IConfiguration configuration) : base(configuration) { }
        public void Add(Ingredient ingredient)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Ingredient ([Name])
                        OUTPUT INSERTED.Id
                        VALUES (@name)";
                    DbUtils.AddParameter(cmd, "@name", ingredient.Name);
                    ingredient.Id = (int)cmd.ExecuteScalar();
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
                    cmd.CommandText = "DELETE FROM Ingredient WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Ingredient> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, [Name] FROM Ingredient";
                    using (var reader = cmd.ExecuteReader())
                    {
                        List<Ingredient> ingredients = new List<Ingredient>();
                        while(reader.Read())
                        {
                            var ingredient = new Ingredient()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            };
                            ingredients.Add(ingredient);
                        }
                        return ingredients;
                    }
                }
            }
        }

        public List<Ingredient> Search(string searchParameters)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, [Name] FROM Ingredient
                        WHERE [Name] LIKE @searchParameters";
                    DbUtils.AddParameter(cmd, "@searchParameters", $"%{searchParameters}%");
                    using (var reader = cmd.ExecuteReader())
                    {
                        var ingredients = new List<Ingredient>();
                        while (reader.Read())
                        {
                            var ingredient = new Ingredient()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name")
                            };
                            ingredients.Add(ingredient);
                        }
                        return ingredients;
                    }
                }
            }
        }
    }
}
