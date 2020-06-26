using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessApp.CarpetWash.Entities.Concrete
{
    public class User : IdentityUser
    {
        public User()
        {
            Contents = new HashSet<Content>();
            Banners = new HashSet<Banner>();
            Comments = new HashSet<Comment>();
            Categories = new HashSet<Category>();
            Features = new HashSet<Feature>();
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Image { get; set; }
        public string Biography { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<Content> Contents { get; set; }
        public ICollection<Banner> Banners { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Feature> Features { get; set; }
    }
}
