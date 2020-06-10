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
    public class BannerManager : IBannerService
    {
        private IBannerDal _bannerDal;
        public BannerManager(IBannerDal bannerDal)
        {
            _bannerDal = bannerDal;
        }

        public async Task CreateAsync(Banner banner)
        {
            try
            {
                await _bannerDal.CreateAsync(banner);
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public async Task DeleteAsync(int bannerId)
        {
            try
            {
                await _bannerDal.DeleteAsync(new Banner { Id = bannerId });
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<Banner> FindBannerByIdAsync(int? bannerId)
        {
            try
            {
                return await _bannerDal.GetWithInludesAsync(id => id.Id == bannerId, c => c.Include(con => con.User));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ICollection<Banner>> GetAllBannersAsync()
        {
            try
            {
                return await _bannerDal.GetListWithIncludesAsync(null, c=> c.Include(con => con.User));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task UpdateAsync(Banner banner)
        {
            try
            {
                await _bannerDal.UpdateAsync(banner);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<bool> Exist(int id)
        {
            var result = await _bannerDal.GetWithInludesAsync(x => x.Id == id);

            if (result != null)
            {
                return true;
            }

            return false;
        }
    }
}
