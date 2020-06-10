using BusinessApp.CarpetWash.Business.Abstract;
using BusinessApp.CarpetWash.Entities.Concrete.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessApp.CarpetWash.MvcWebUI.Models;

namespace BusinessApp.CarpetWash.MvcWebUI.ViewComponents
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly IContentService _contentService;
        private readonly ITagService _tagService;

        public SideBarViewComponent(IContentService contentService, ITagService tagService)
        {
            _contentService = contentService;
            _tagService = tagService;
        }
        public async Task<ViewViewComponentResult> InvokeAsync(ContentType type, int? id)
        {
            var contentByType = await _contentService.GetAllContentsAsync(type);
            var services = await _contentService.GetAllContentsAsync(ContentType.Service);
            var tags = await _tagService.GetAllTagsByContentId(id);

            var sideBarItems = new SideBarViewModel()
            {
                ContentByType = contentByType.OrderByDescending(x => x.UpdatedAt).Take(5).ToList(),
                Services = services.OrderByDescending(x => x.UpdatedAt).Take(5).ToList(),
                Tags = tags
            };

            return View(sideBarItems);
        }
    }
}
