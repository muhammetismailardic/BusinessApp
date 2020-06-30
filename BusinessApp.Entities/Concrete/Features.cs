using BusinessApp.CarpetWash.Entities.Concrete.Enums;
using BusinessApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessApp.CarpetWash.Entities.Concrete
{
    public class Feature : IEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string FeatureTitles { get; set; }
        public string FeatureDetails { get; set; }
        public string Image { get; set; }
        public string Location { get; set; }
        public ContentType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User User { get; set; }
    }
}
