using BusinessApp.CarpetWash.Business.Abstract;
using BusinessApp.CarpetWash.DataAccess.Abstract;
using BusinessApp.CarpetWash.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApp.CarpetWash.Business.Concrete
{
    public class TagManager : ITagService
    {
        private ITagDal _tagDal;
        public TagManager(ITagDal tagDal)
        {
            _tagDal = tagDal;
        }

        public async Task CreateAsync(Tag tag)
        {
            try
            {
                await _tagDal.CreateAsync(tag);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task DeleteAsync(int tagId)
        {
            try
            {
                await _tagDal.DeleteAsync(new Tag { Id = tagId });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task DeleteByContentId(int contentId)
        {
            var tagsByContent = await _tagDal.GetListWithIncludesAsync(c => c.ContentId == contentId);

            foreach (var item in tagsByContent)
            {
               await _tagDal.DeleteAsync(item);
            }
        }

        public async Task<Tag> FindTagByIdAsync(int? tagId)
        {
            try
            {
                return await _tagDal.GetWithInludesAsync(
                    id => id.Id == tagId,
                    c => c.Include(con => con.Content)
                          .ThenInclude(com => com.User));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ICollection<Tag>> GetAllTagsAsync()
        {
            try
            {
                return await _tagDal.GetListWithIncludesAsync(
                    null,
                    c => c.Include(con => con.Content)
                          .ThenInclude(com => com.User));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ICollection<Tag>> GetAllTagsByContentId(int? contentId) 
        {
           return await _tagDal.GetListWithIncludesAsync(x => x.ContentId == contentId);
        }
        public async Task UpdateAsync(Tag tag)
        {
            try
            {
                await _tagDal.UpdateAsync(tag);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> Exist(int id)
        {
            var result = await _tagDal.GetWithInludesAsync(x => x.Id == id);

            if (result != null)
            {
                return true;
            }

            return false;
        }
    }
}
