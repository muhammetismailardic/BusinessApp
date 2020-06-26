using BusinessApp.CarpetWash.DataAccess.Abstract;
using BusinessApp.CarpetWash.Entities.Concrete;
using BusinessApp.Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessApp.CarpetWash.DataAccess.Concrete.EntityFramework
{
    public class EfFeaturesDal : EfEntityRepositoryBase<Feature, CarpetWashContext>, IFeaturesDal
    {
    }
}
