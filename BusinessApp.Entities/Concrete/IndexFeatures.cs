using BusinessApp.CarpetWash.Entities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessApp.CarpetWash.Entities.Concrete
{
    public class IndexFeatures
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string FeatureTitles { get; set; }
        public string FeatureDetails { get; set; }
        public string Images { get; set; }
        public ContentType Type { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
