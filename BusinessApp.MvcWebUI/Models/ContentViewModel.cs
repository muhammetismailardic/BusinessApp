using BusinessApp.CarpetWash.Entities.Concrete;
using BusinessApp.CarpetWash.Entities.Concrete.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessApp.CarpetWash.MvcWebUI.Models
{
    public class ContentViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string Text { get; set; }
        public bool IsActive { get; set; }
        public int VisitCount { get; set; }
        public ContentType Type { get; set; }
        
        [Display(Name = "Profile Picture")]
        public IFormFile ProfileImage { get; set; }
        public string CurrentImage { get; set; }
        public SelectList CategoryList { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Tags { get; set; }

        public ICollection<Content> Contents { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public bool IsFrontSideIndex { get; set; }
        public int CurrentPage { get; set; }
    }
}
