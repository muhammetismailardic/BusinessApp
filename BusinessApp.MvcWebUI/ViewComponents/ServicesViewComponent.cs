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
    public class ServicesViewComponent : ViewComponent
    {
        private readonly IContentService _contentService;

        public ServicesViewComponent(IContentService contentService)
        {
            _contentService = contentService;
        }
        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            var servicesByContent = await _contentService.GetAllContentsAsync(ContentType.Service);

            var services = new ServicesViewModel
            {
              Services = servicesByContent.Select(x=> x.Title).ToList()
            };

            return View(services);
        }
    }
}
