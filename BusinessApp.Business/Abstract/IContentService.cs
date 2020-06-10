using BusinessApp.CarpetWash.Entities.Concrete;
using BusinessApp.CarpetWash.Entities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApp.CarpetWash.Business.Abstract
{
    public interface IContentService
    {
        Task CreateAsync(Content post);
        Task UpdateAsync(Content post);
        Task DeleteAsync(int postd);
        Task<ICollection<Content>> GetAllContentsAsync(ContentType type);
        Task<Content> FindContentByIdAsync(int? contentId);
        Task<bool> Exist(int id);
    }
}
