using BusinessApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessApp.CarpetWash.Entities.Concrete
{
    public class Banner : IEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string BtnTitle { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public User User { get; set; }
    }
}
