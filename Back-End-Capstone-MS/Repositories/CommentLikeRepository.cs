using System.Collections.Generic;
using Back_End_Capstone_MS.Models;
using Back_End_Capstone_MS.Utils;
using Microsoft.Extensions.Configuration;

namespace Back_End_Capstone_MS.Repositories
{
    public class CommentLikeRepository : BaseRepository, ICommentLikeRepository
    {
        public CommentLikeRepository(IConfiguration config) : base(config) { }
        public void Add(CommentLike cl)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO CommentLike (CommentId, UserId)
                        OUTPUT INSERTED.Id
                        VALUES (@commentId, @userId)";
                    DbUtils.AddParameter(cmd, "@commentId", cl.CommentId);
                    DbUtils.AddParameter(cmd, "@userId", cl.UserId);
                    cl.Id = (int)cmd.ExecuteScalar();
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
                    cmd.CommandText = "DELETE FROM CommentLike WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<CommentLike> GetByCommentId(int commentId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, CommentId, UserId
                        FROM CommentLike
                        WHERE CommentId = @commentId";
                    DbUtils.AddParameter(cmd, "@commentId", commentId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        var likes = new List<CommentLike>();
                        while (reader.Read())
                        {
                            likes.Add(new CommentLike()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                CommentId = DbUtils.GetInt(reader, "CommentId"),
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
