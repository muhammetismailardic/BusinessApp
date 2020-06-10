using BusinessApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessApp.CarpetWash.Entities.Concrete
{
    public class Comment : IEntity
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Ip { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Content Content { get; set; }
        public User User { get; set; }
    }
}
