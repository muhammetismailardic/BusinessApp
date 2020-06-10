using BusinessApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessApp.CarpetWash.Entities.Concrete
{
    public class Tag : IEntity
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public string Slug { get; set; }
        public string ItemType { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Content Content { get; set; }
    }
}
