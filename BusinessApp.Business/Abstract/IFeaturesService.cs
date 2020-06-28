using BusinessApp.CarpetWash.Entities.Concrete;
using BusinessApp.CarpetWash.Entities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApp.CarpetWash.Business.Abstract
{
    public interface IFeatureService
    {
        Task CreateAsync(Feature feature);
        Task UpdateAsync(Feature feature);
        Task DeleteAsync(int featureId);
        Task<ICollection<Feature>> GetAllFeaturesAsync();
        Task<Feature> FindFeatureByIdAsync(int? FeatureId);
        Task<bool> Exist(int id);
    }
}
