using BusinessApp.CarpetWash.Entities.Concrete;
using BusinessApp.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessApp.CarpetWash.DataAccess.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>
    {
    }
}
