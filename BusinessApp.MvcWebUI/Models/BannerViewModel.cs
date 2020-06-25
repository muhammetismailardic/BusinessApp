using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessApp.CarpetWash.MvcWebUI.Models
{
    public class BannerViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ContentId { get; set; }
        public string CurrentImage { get; set; }

        [Display(Name = "Profile Picture")]
        public IFormFile ProfileImage { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string BtnTitle { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public SelectList ContentList { get; set; }
    }
}
