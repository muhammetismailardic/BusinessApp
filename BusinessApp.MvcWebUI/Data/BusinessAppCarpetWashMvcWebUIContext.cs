using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusinessApp.CarpetWash.Entities.Concrete;

namespace BusinessApp.CarpetWash.MvcWebUI.Data
{
    public class BusinessAppCarpetWashMvcWebUIContext : DbContext
    {
        public BusinessAppCarpetWashMvcWebUIContext (DbContextOptions<BusinessAppCarpetWashMvcWebUIContext> options)
            : base(options)
        {
        }

        public DbSet<BusinessApp.CarpetWash.Entities.Concrete.Feature> Feature { get; set; }
    }
}
