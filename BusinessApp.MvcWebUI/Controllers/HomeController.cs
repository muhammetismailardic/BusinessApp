using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessApp.CarpetWash.Business.Abstract;
using BusinessApp.CarpetWash.Entities.Concrete.Enums;
using BusinessApp.CarpetWash.MvcWebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BusinessApp.CarpetWash.MvcWebUI.Controllers
{
    public class HomeController : Controller
    {
        IContentService _contetService;
        IBannerService _bannerService;
        public HomeController(IContentService contentService, IBannerService bannerService)
        {
            _contetService = contentService;
            _bannerService = bannerService;
        }
        public async Task<IActionResult> Index()
        {
            var contents = await _contetService.GetAllContentsAsync(ContentType.All);
            var banners = await _bannerService.GetAllBannersAsync();

            var content = new HomeViewModel
            {
                banners = banners,
                Services = contents.Where(x => x.Type == ContentType.Service),
                Posts = contents.Where(x => x.Type == ContentType.Post),
                ServiceRegion = contents.Where(x => x.Type == ContentType.ServiceRegion)
            };
            return View(content);
        }

        public async Task<IActionResult> About()
        {
            return View();
        }
        public async Task<IActionResult> Contact()
        {
            return View();
        }
    }
}