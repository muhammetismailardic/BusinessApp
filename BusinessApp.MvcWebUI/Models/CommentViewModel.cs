using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessApp.CarpetWash.MvcWebUI.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public int ParentId { get; set; }
        public string UserId { get; set; }

        [Required(ErrorMessage = "Please enter Name name")]
        [MaxLength(64)]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Please enter email name")]
        [MaxLength(64)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter text")]
        [MaxLength(512)]
        public string Text { get; set; }
        public bool IsAnonymous { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}