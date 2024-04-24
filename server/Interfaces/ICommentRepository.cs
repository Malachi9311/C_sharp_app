using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Models;
using server.Helpers;

namespace server.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject);
        Task<Comment?> GetById(int id);
        Task<Comment> CreateAsync(Comment commentModel);
        Task<Comment?> UpdateAsync(int id, Comment commentModel);
        Task<Comment?> DeleteAsync(int id);
    }
}