using BusinessApp.CarpetWash.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApp.CarpetWash.Business.Abstract
{
   public interface ICommentService
    {
        Task CreateAsync(Comment comment);
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(int commentId);
        Task<ICollection<Comment>> GetAllCommentsAsync();
        Task<Comment> FindCommentByIdAsync(int commentId);
    }
}
