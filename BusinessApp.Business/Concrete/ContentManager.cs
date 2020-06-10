using BusinessApp.CarpetWash.Business.Abstract;
using BusinessApp.CarpetWash.DataAccess.Abstract;
using BusinessApp.CarpetWash.Entities.Concrete;
using BusinessApp.CarpetWash.Entities.Concrete.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApp.CarpetWash.Business.Concrete
{
    public class ContentManager : IContentService
    {
        private IContentDal _contentDal;
        public ContentManager(IContentDal postDal)
        {
            _contentDal = postDal;
        }
        public async Task CreateAsync(Content post)
        {
            try
            {
                await _contentDal.CreateAsync(post);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task DeleteAsync(int postId)
        {
            try
            {
                await _contentDal.DeleteAsync(new Content { Id = postId });
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<Content> FindContentByIdAsync(int? contentId)
        {
            try
            {
                return await _contentDal.GetWithInludesAsync(
                    c => c.Id == contentId,
                    c => c.Include(c => c.Category),
                    c => c.Include(c => c.User),
                    c => c.Include(con => con.Tags),
                    c => c.Include(con => con.Comments)
                          .ThenInclude(com => com.User));
            }

            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<ICollection<Content>> GetAllContentsAsync(ContentType type)
        {
            try
            {
                Expression<Func<Content, bool>> query = null;
                var contents = await _contentDal.GetListWithIncludesAsync(
                        type == ContentType.All ? query = null : (c => c.Type == type),
                        c => c.Include(con => con.Category),
                        c => c.Include(con => con.User),
                        c => c.Include(con => con.Tags),
                        c => c.Include(con => con.Comments)
                         .ThenInclude(com => com.User));

                return contents;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task UpdateAsync(Content post)
        {
            try
            {
                await _contentDal.UpdateAsync(post);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> Exist(int id)
        {
            var result = await _contentDal.GetWithInludesAsync(x => x.Id == id);

            if (result != null)
            {
                return true;
            }

            return false;
        }
    }
}
