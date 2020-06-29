﻿using BusinessApp.CarpetWash.Entities.Concrete;
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
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string FeatureTitles { get; set; }
        public string FeatureDetails { get; set; }
        public string CurrentImage { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public ContentType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public IFormFile ProfileImage { get; set; }
    }
}