using Back_End_Capstone_MS.Models;
using Back_End_Capstone_MS.Utils;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Back_End_Capstone_MS.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IConfiguration configuration) : base(configuration) { }
        public void AddComment(Comment comment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Comment (UserId, RecipeId, Message, DateCreated)
                        OUTPUT INSERTED.Id
                        VALUES (@userId, @recipeId, @message, @dateCreated)";
                    DbUtils.AddParameter(cmd, "@userId", comment.UserId);
                    DbUtils.AddParameter(cmd, "@recipeId", comment.RecipeId);
                    DbUtils.AddParameter(cmd, "@message", comment.Message);
                    DbUtils.AddParameter(cmd, "@dateCreated", comment.DateCreated);
                    comment.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void DeleteComment(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Comment WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Comment> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT c.Id, c.UserId, c.RecipeId, c.[Message], c.DateCreated, cl.Id AS likeId, cl.UserId AS likeUserId, cl.CommentId as likeCommentId
                        FROM Comment c";
                    using (var reader = cmd.ExecuteReader())
                    {
                        var comments = new List<Comment>();
                        var comment = new Comment();
                        while (reader.Read())
                        {                           
                            comment.Id = DbUtils.GetInt(reader, "Id");
                            comment.UserId = DbUtils.GetInt(reader, "UserId");
                            comment.RecipeId = DbUtils.GetInt(reader, "RecipeId");
                            comment.Message = DbUtils.GetString(reader, "Message");
                            comment.DateCreated = DbUtils.GetDateTime(reader, "DateCreated");                         
                            comments.Add(comment);
                        }
                        return comments;
                    }
                }
            }
        }

        public Comment GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT c.Id, c.UserId, c.RecipeId, c.[Message], c.DateCreated, cl.Id AS likeId, cl.UserId AS likeUserId, cl.CommentId as likeCommentId
                        FROM Comment c
                        JOIN CommentLike cl ON c.Id = cl.CommentId
                        WHERE c.Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        var comment = new Comment();
                        if (reader.Read())
                        {                           
                            comment.Id = DbUtils.GetInt(reader, "Id");
                            comment.UserId = DbUtils.GetInt(reader, "UserId");
                            comment.RecipeId = DbUtils.GetInt(reader, "RecipeId");
                            comment.Message = DbUtils.GetString(reader, "Message");
                            comment.DateCreated = DbUtils.GetDateTime(reader, "DateCreated");                                                         
                        }
                        return comment;
                    }
                }
            }
        }
        //commentLikes query from the commentLike repo
        public List<Comment> GetByRecipeId(int recipeId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT c.Id, c.UserId, c.RecipeId, c.[Message], c.DateCreated, cl.Id AS likeId, cl.UserId AS likeUserId, cl.CommentId as likeCommentId
                        FROM Comment c
                        WHERE c.RecipeId = @recipeId";
                    DbUtils.AddParameter(cmd, "@recipeId", recipeId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        var comments = new List<Comment>();
                        var comment = new Comment();
                        if (reader.Read())
                        {                                                    
                            comment.Id = DbUtils.GetInt(reader, "Id");
                            comment.UserId = DbUtils.GetInt(reader, "UserId");
                            comment.RecipeId = DbUtils.GetInt(reader, "RecipeId");
                            comment.Message = DbUtils.GetString(reader, "Message");
                            comment.DateCreated = DbUtils.GetDateTime(reader, "DateCreated");                                                   
                            comments.Add(comment);
                        }
                        return comments;
                    }
                }
            }
        }

        public void UpdateComment(Comment comment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Comment 
                        SET ([Message] = @message)
                        WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", comment.Id);
                    DbUtils.AddParameter(cmd, "@message", comment.Message);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
