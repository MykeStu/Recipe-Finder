using Back_End_Capstone_MS.Models;
using Back_End_Capstone_MS.Utils;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Back_End_Capstone_MS.Repositories
{
    public class TagRepository : BaseRepository, ITagRepository
    {
        public TagRepository(IConfiguration configuration) : base(configuration) { }
        public void Add(Tag tag)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Tag ([Name])
                        OUTPUT INSERTED.Id
                        VALUES (@name)";
                    DbUtils.AddParameter(cmd, "@name", tag.Name);
                    tag.Id = (int)cmd.ExecuteScalar();
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
                    cmd.CommandText = "DELETE FROM Tag WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Tag> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, [Name] FROM Tag";
                    using (var reader = cmd.ExecuteReader())
                    {
                        var tags = new List<Tag>();
                        while (reader.Read())
                        {
                            var tag = new Tag()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name")
                            };
                            tags.Add(tag);
                        }
                        return tags;
                    }
                }
            }
        }

        public Tag GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, [Name] FROM Tag
                        WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        Tag tag = null;
                        if (reader.Read())
                        {
                            tag.Id = id;
                            tag.Name = DbUtils.GetString(reader, "Name");
                        }
                        return tag;
                    }
                }
            }
        }

        public void Update(Tag tag)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Tag 
                        SET([Name] = @name)
                        WHERE Id = @id
                        ";
                    DbUtils.AddParameter(cmd, "@name", tag.Name);
                    DbUtils.AddParameter(cmd, "@id", tag.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
