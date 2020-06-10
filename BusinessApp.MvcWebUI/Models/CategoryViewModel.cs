using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessApp.CarpetWash.MvcWebUI.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter category name")]
        [Display(Name = "Cateogry Name")]
        public string Name { get; set; }

        
        [Display(Name = "Profile Picture")]
        public IFormFile ProfileImage { get; set; }

        public string CurrentImage { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
