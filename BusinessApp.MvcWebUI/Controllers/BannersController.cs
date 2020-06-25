using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessApp.CarpetWash.DataAccess.Concrete.EntityFramework;
using BusinessApp.CarpetWash.Entities.Concrete;
using BusinessApp.CarpetWash.Business.Abstract;
using Microsoft.AspNetCore.Identity;
using BusinessApp.CarpetWash.MvcWebUI.Models;
using Microsoft.AspNetCore.Hosting;
using BusinessApp.CarpetWash.MvcWebUI.Shared;
using BusinessApp.CarpetWash.Entities.Concrete.Enums;
using Microsoft.AspNetCore.Authorization;

namespace BusinessApp.CarpetWash.MvcWebUI.Controllers
{
    [Authorize]
    public class BannersController : Controller
    {
        private readonly IBannerService _bannerService;
        private readonly IContentService _contentService;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private FileExtentions fileExtentions;

        public BannersController(IBannerService bannerService, UserManager<User> userManager, IWebHostEnvironment webHostEnvironment, IContentService contentService)
        {
            _bannerService = bannerService;
            _contentService = contentService;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            fileExtentions = new FileExtentions(_webHostEnvironment);
        }

        // GET: Banners
        public async Task<IActionResult> Index()
        {
            return View(await _bannerService.GetAllBannersAsync());
        }

        //// GET: Banners/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var banner = await _context.Banners
        //        .Include(b => b.User)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (banner == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(banner);
        //}

        //// GET: Banners/Create
        public async Task<IActionResult> Create()
        {
            var contents = await _contentService.GetAllContentsAsync(ContentType.All);

            var banner = new BannerViewModel()
            {
                ContentList = new SelectList(contents, "Id", "Title"),
            };

            return View(banner);
        }

        //// POST: Banners/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ContentId,CurrentImage,ProfileImage,Title,SubTitle,BtnTitle,IsActive,CreatedAt")] BannerViewModel bannerViewModel)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = fileExtentions.UploadedFile(bannerViewModel.ProfileImage, "banner");
                var user = await _userManager.GetUserAsync(HttpContext.User);

                var banner = new Banner()
                {
                    Id = bannerViewModel.Id,
                    UserId = user.Id,
                    ContentId = bannerViewModel.ContentId,
                    Image = uniqueFileName != null ? uniqueFileName : null,
                    Title = bannerViewModel.Title,
                    SubTitle = bannerViewModel.SubTitle,
                    BtnTitle = bannerViewModel.BtnTitle,
                    IsActive = bannerViewModel.IsActive,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                await _bannerService.CreateAsync(banner);
                return RedirectToAction(nameof(Index));
            }
            return View(bannerViewModel);
        }

        //// GET: Banners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contents = await _contentService.GetAllContentsAsync(ContentType.All);
            var banner = await _bannerService.FindBannerByIdAsync(id);

            var bannerViewModel = new BannerViewModel()
            {
                Id = banner.Id,
                UserId = banner.UserId,
                ContentId = banner.ContentId,
                Title = banner.Title,
                BtnTitle = banner.BtnTitle,
                CurrentImage = banner.Image,
                IsActive = banner.IsActive,
                SubTitle = banner.SubTitle,
                CreatedAt = banner.CreatedAt,
                ContentList = new SelectList(contents, "Id", "Title")
            };

            if (banner == null)
            {
                return NotFound();
            }
            return View(bannerViewModel);
        }

        //// POST: Banners/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ContentId,CurrentImage,ProfileImage,Title,SubTitle,BtnTitle,IsActive,CreatedAt,UpdatedAt")] BannerViewModel bannerViewModel)
        {
            if (id != bannerViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string uniqueFileName;
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var banner = new Banner();
                try
                {
                    banner.Id = bannerViewModel.Id;
                    banner.UserId = user.Id;
                    banner.ContentId = bannerViewModel.ContentId;
                    banner.Title = bannerViewModel.Title;
                    banner.BtnTitle = bannerViewModel.BtnTitle;
                    banner.SubTitle = bannerViewModel.SubTitle;
                    banner.Image = bannerViewModel.CurrentImage;
                    banner.IsActive = bannerViewModel.IsActive;
                    banner.CreatedAt = bannerViewModel.CreatedAt;
                    banner.UpdatedAt = DateTime.Now;

                    if (bannerViewModel.ProfileImage != null)
                    {
                        //Old Image Delete operation goes here
                        var directory = fileExtentions._rootImageDirectory + "/banner/" + banner.Image;
                        fileExtentions.DeleteFile(directory);

                        //Adding newly added image to directory.
                        uniqueFileName = fileExtentions.UploadedFile(bannerViewModel.ProfileImage, "banner");
                        banner.Image = uniqueFileName;
                    }
                    await _bannerService.UpdateAsync(banner);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await BannerExists(banner.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bannerViewModel);
        }

        //// GET: Banners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banner = await _bannerService.FindBannerByIdAsync(id);
            if (banner == null)
            {
                return NotFound();
            }

            return View(banner);
        }

        //// POST: Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var banner = await _bannerService.FindBannerByIdAsync(id);
            //Old Image Delete operation goes here
            var directory = fileExtentions._rootImageDirectory + "/banner/" + banner.Image;
            fileExtentions.DeleteFile(directory);

            await _bannerService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BannerExists(int id)
        {
            return await _bannerService.Exist(id);
        }
    }
}
