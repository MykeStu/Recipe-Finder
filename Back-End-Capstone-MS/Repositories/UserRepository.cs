using Back_End_Capstone_MS.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Back_End_Capstone_MS.Repositories;
using Back_End_Capstone_MS.Utils;
public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(IConfiguration configuration) : base(configuration) { }
    public List<User> GetAll()
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT Id, DisplayName, Email, DateCreated, [Admin], Active FROM [User]";
                using (var reader = cmd.ExecuteReader())
                {
                    var users = new List<User>();
                    while (reader.Read())
                    {
                        users.Add(new User()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            DisplayName = DbUtils.GetString(reader, "DisplayName"),
                            Email = DbUtils.GetString(reader, "Email"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                            Admin = reader.GetBoolean(reader.GetOrdinal("Admin")),
                            Active = reader.GetBoolean(reader.GetOrdinal("Active"))
                        });
                    }
                    return users;
                }
            }
        }
    }

    public User GetByFireBaseUserId(string fireBaseUserId)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT Id, DisplayName, Email, DateCreated, Admin, Active, FireBaseUserId FROM User
                                        WHERE FireBaseUserId = @fireBaseUserId";
                DbUtils.AddParameter(cmd, "@fireBaseUserId", fireBaseUserId);
                using (var reader = cmd.ExecuteReader())
                {
                    User user = null;
                    if (reader.Read())
                    {
                        user.Id = DbUtils.GetInt(reader, "Id");
                        user.DisplayName = DbUtils.GetString(reader, "DisplayName");
                        user.Email = DbUtils.GetString(reader, "Email");
                        user.DateCreated = DbUtils.GetDateTime(reader, "DateCreated");
                        user.Admin = reader.GetBoolean(reader.GetOrdinal("Admin"));
                        user.Active = reader.GetBoolean(reader.GetOrdinal("Active"));
                        user.FireBaseUserId = DbUtils.GetString(reader, "FireBaseUserId");
                    }
                    return user;
                }
            }
        }
    }
    public User GetById(int id)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT Id, DisplayName, Email, DateCreated, [Admin], Active FROM [User]
                                        WHERE [User].Id = @id";
                DbUtils.AddParameter(cmd, "@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    User user = null;
                    if (reader.Read())
                    {
                        user.Id = DbUtils.GetInt(reader, "Id");
                        user.DisplayName = DbUtils.GetString(reader, "DisplayName");
                        user.Email = DbUtils.GetString(reader, "Email");
                        user.DateCreated = DbUtils.GetDateTime(reader, "DateCreated");
                        user.Admin = reader.GetBoolean(reader.GetOrdinal("Admin"));
                        user.Active = reader.GetBoolean(reader.GetOrdinal("Active"));
                    }
                    return user;
                }
            }
        }
    }
    public void Update(User user)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    UPDATE [User]
                    SET (
                    DisplayName = @displayName,
                    Email = @email,
                    [Admin] = @admin
                    Active = @active
                    )
                    WHERE Id = @id
                    ";
                DbUtils.AddParameter(cmd, "@id", user.Id);
                DbUtils.AddParameter(cmd, "@displayName", user.DisplayName);
                DbUtils.AddParameter(cmd, "@email", user.Email);
                DbUtils.AddParameter(cmd, "@admin", user.Admin);
                DbUtils.AddParameter(cmd, "@active", user.Active);
                cmd.ExecuteNonQuery();
            }
        }
    }
    public void Add(User user)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO [User] (DisplayName, Email, [Admin], Active, FireBaseUserId)
                    OUTPUT INSERTED.Id
                    VALUES (@displayName, @email, @admin, @active, @fireBaseUserId)";
                DbUtils.AddParameter(cmd, "@displayName", user.DisplayName);
                DbUtils.AddParameter(cmd, "@email", user.Email);
                DbUtils.AddParameter(cmd, "@admin", user.Admin);
                DbUtils.AddParameter(cmd, "@active", user.Active);
                DbUtils.AddParameter(cmd, "@fireBaseUserId", user.FireBaseUserId);
                user.Id = (int)cmd.ExecuteScalar();
            }
        }
    }
}

