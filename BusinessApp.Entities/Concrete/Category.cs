using BusinessApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessApp.CarpetWash.Entities.Concrete
{
    public class Category : IEntity
    {
        public Category()
        {
            Contents = new HashSet<Content>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<Content> Contents { get; set; }
        public User User { get; set; }
    }
}
