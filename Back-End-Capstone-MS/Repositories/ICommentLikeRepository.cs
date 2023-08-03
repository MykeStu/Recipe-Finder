using Back_End_Capstone_MS.Models;
using System.Collections.Generic;
namespace Back_End_Capstone_MS.Repositories
{
    public interface ICommentLikeRepository
    {
        List<CommentLike> GetByCommentId(int commentId);
        void Add(CommentLike commentLike);
        void Delete(int id);
    }
}
