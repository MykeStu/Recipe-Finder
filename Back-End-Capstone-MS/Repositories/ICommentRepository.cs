using Back_End_Capstone_MS.Models;
using System.Collections.Generic;
namespace Back_End_Capstone_MS.Repositories
{
    public interface ICommentRepository
    {
        List<Comment> GetAll();
        List<Comment> GetByRecipeId(int recipeId);
        Comment GetById(int id);
        void AddComment(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(int id);
    }
}
