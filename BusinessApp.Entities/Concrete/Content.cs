using BusinessApp.CarpetWash.Entities.Concrete.Enums;
using BusinessApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessApp.CarpetWash.Entities.Concrete
{
    public class Content : IEntity
    {
        public Content()
        {
            Comments = new HashSet<Comment>();
            Tags = new HashSet<Tag>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string Text { get; set; }
        public string Slug { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public int VisitCount { get; set; }
        public ContentType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public User User { get; set; }
        public Category Category { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
