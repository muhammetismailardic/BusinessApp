using BusinessApp.CarpetWash.Business.Abstract;
using BusinessApp.CarpetWash.DataAccess.Abstract;
using BusinessApp.CarpetWash.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BusinessApp.CarpetWash.Business.Concrete
{
    public class CommentManager : ICommentService
    {
        private ICommentDal _commentDal;
        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }
        public async Task CreateAsync(Comment comment)
        {
            try
            {
                await _commentDal.CreateAsync(comment);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task DeleteAsync(int commentId)
        {
            try
            {
               await _commentDal.DeleteAsync(new Comment { Id = commentId });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Comment> FindCommentByIdAsync(int commentId)
        {
            try
            {
                return await _commentDal.GetWithInludesAsync(id => id.Id == commentId);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ICollection<Comment>> GetAllCommentsAsync()
        {
            try
            {
                return await _commentDal.GetListWithIncludesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ICollection<Comment>> GetAllCommentsByContentIdAsync(int contentId)
        {
            try
            {
                var comments = await _commentDal.GetListWithIncludesAsync(
                    (c => c.Id == contentId),
                        c => c.Include(con => con.User));
                
                return comments;
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
        public async Task UpdateAsync(Comment comment)
        {
            try
            {
                await _commentDal.UpdateAsync(comment);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
