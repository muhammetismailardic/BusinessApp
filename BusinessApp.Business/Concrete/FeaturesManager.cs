using BusinessApp.CarpetWash.Business.Abstract;
using BusinessApp.CarpetWash.DataAccess.Abstract;
using BusinessApp.CarpetWash.Entities.Concrete;
using BusinessApp.CarpetWash.Entities.Concrete.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApp.CarpetWash.Business.Concrete
{
    public class FeaturesManager : IFeaturesService
    {
        private IFeaturesDal _featureDal;
        public FeaturesManager(IFeaturesDal featureDal)
        {
            _featureDal = featureDal;
        }

        public async Task CreateAsync(Feature feature)
        {
            try
            {
                await _featureDal.CreateAsync(feature);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task DeleteAsync(int featureId)
        {
            try
            {
                await _featureDal.DeleteAsync(new Feature { Id = featureId });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> Exist(int id)
        {
            var result = await _featureDal.GetWithInludesAsync(x => x.Id == id);

            if (result != null)
            {
                return true;
            }

            return false;
        }

        public async Task<Feature> FindFeatureByIdAsync(int? FeatureId)
        {
            try
            {
                return await _featureDal.GetWithInludesAsync(
                    c => c.Id == FeatureId,
                    c => c.Include(c => c.User));
            }

            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ICollection<Feature>> GetAllFeaturesAsync()
        {
            try
            {
                var feature = await _featureDal.GetListWithIncludesAsync();
                return feature;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task UpdateAsync(Feature feature)
        {
            try
            {
                await _featureDal.UpdateAsync(feature);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
