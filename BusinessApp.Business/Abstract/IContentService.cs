﻿using BusinessApp.CarpetWash.Entities.Concrete;
using BusinessApp.CarpetWash.Entities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApp.CarpetWash.Business.Abstract
{
    public interface IContentService
    {
        Task CreateAsync(Content content);
        Task UpdateAsync(Content content);
        Task DeleteAsync(int contentId);
        Task<ICollection<Content>> GetAllContentsAsync(ContentType type);
        Task<Content> FindContentByIdAsync(int? contentId);
        Task<bool> Exist(int id);
    }
}
