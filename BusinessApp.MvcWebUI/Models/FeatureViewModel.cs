using BusinessApp.CarpetWash.Entities.Concrete;
using BusinessApp.CarpetWash.Entities.Concrete.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessApp.CarpetWash.MvcWebUI.Models
{
    public class FeatureViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Excerpt { get; set; }
        [Required]
        public string FeatureTitles { get; set; }
        public string FeatureDetails { get; set; }
        public string CurrentImage { get; set; }
        public string Location { get; set; }
        public ContentType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<IFormFile> ProfileImage { get; set; }
    }
}
