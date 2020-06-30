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
        ICommentService _commentService;
        IFeatureService _featureService;
        public HomeController(IContentService contentService, IBannerService bannerService, ICommentService commentService, IFeatureService featureService)
        {
            _contetService = contentService;
            _bannerService = bannerService;
            _commentService = commentService;
            _featureService = featureService;
        }
        public async Task<IActionResult> Index()
        {
            var contents = await _contetService.GetAllContentsAsync(ContentType.All);
            var banners = await _bannerService.GetAllBannersAsync();
            var comments = await _commentService.GetAllCommentsAsync();
            var homeFeature = await _featureService.GetByFeatureType(ContentType.HomeFeature);
            var content = new HomeViewModel
            {
                banners = banners,
                Services = contents.Where(x => x.Type == ContentType.Service),
                Posts = contents.Where(x => x.Type == ContentType.Post),
                ServiceRegion = contents.Where(x => x.Type == ContentType.ServiceRegion),
                Comments = comments.OrderByDescending(x => x.CreatedAt).Take(2),
                HomeFeature = homeFeature
            };

            return View(content);
        }

        public async Task<IActionResult> About()
        {
            return View();
        }
        public async Task<IActionResult> Contact(ContentType type)
        {

            if (type == ContentType.ContactFeature)
            {
                var serviceByType = await _featureService.GetByFeatureType(type);

                return View(serviceByType);
            }

            return RedirectToAction(nameof(Index));

        }
    }
}