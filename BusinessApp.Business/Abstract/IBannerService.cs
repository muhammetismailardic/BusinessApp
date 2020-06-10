using BusinessApp.CarpetWash.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApp.CarpetWash.Business.Abstract
{
    public interface IBannerService
    {
        Task CreateAsync(Banner banner);
        Task UpdateAsync(Banner banner);
        Task DeleteAsync(int bannerId);
        Task<ICollection<Banner>> GetAllBannersAsync();
        Task<Banner> FindBannerByIdAsync(int? bannerId);
        Task<bool> Exist(int id);
    }
}
