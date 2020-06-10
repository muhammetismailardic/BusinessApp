using BusinessApp.CarpetWash.Entities.Concrete;
using System.Collections.Generic;

namespace BusinessApp.CarpetWash.MvcWebUI.Models
{
    public class SideBarViewModel
    {
        public ICollection<Content> ContentByType { get; set; }
        public ICollection<Content> Services { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}