using BusinessApp.CarpetWash.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApp.CarpetWash.Business.Abstract
{
    public interface ITagService
    {
        Task CreateAsync(Tag tag);
        Task UpdateAsync(Tag tag);
        Task DeleteAsync(int tagId);
        Task<ICollection<Tag>> GetAllTagsAsync();
        Task<Tag> FindTagByIdAsync(int? tagId);
        Task<ICollection<Tag>> GetAllTagsByContentId(int? contentId);
        Task DeleteByContentId(int contentId);
        Task<bool> Exist(int id);
    }
}
