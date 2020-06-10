using BusinessApp.CarpetWash.Entities.Concrete;
using System.Collections.Generic;

namespace BusinessApp.CarpetWash.MvcWebUI.Models
{
    public class ServicesViewModel
    {
        public ICollection<string> Services { get; set; }
    }
}