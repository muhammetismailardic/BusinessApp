using BusinessApp.CarpetWash.Entities.Concrete;
using System.Collections.Generic;

namespace BusinessApp.CarpetWash.MvcWebUI.Models
{
    public class HomeViewModel
    {
        public ICollection<Banner> banners { get; set; }
        public IEnumerable<Content> Services { get; internal set; }
        public IEnumerable<Content> Posts { get; internal set; }
        public IEnumerable<Content> ServiceRegion { get; internal set; }
    }
}