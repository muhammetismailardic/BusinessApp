﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessApp.CarpetWash.Entities.Concrete;
using BusinessApp.CarpetWash.Business.Abstract;
using BusinessApp.CarpetWash.Entities.Concrete.Enums;
using Microsoft.AspNetCore.Identity;
using BusinessApp.CarpetWash.MvcWebUI.Models;
using BusinessApp.CarpetWash.MvcWebUI.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace BusinessApp.CarpetWash.MvcWebUI.Controllers
{
    public class FeatureController : Controller
    {
        private IFeatureService _featureService;
        private readonly UserManager<User> _userManager;
        private FileExtentions _fileExtentions;
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        public FeatureController(IFeatureService featureService, UserManager<User> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _featureService = featureService;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _fileExtentions = new FileExtentions(_webHostEnvironment);
        }

        // GET: Feature
        public async Task<IActionResult> Index()
        {
            return View(await _featureService.GetAllFeaturesAsync());
        }

        // GET: Feature/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var feature = await _context.Feature
        //        .Include(f => f.User)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (feature == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(feature);
        //}

        // GET: Feature/Create
        public async Task<IActionResult> Create(ContentType type)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var createFeature = new FeatureViewModel()
            {
                UserId = user.Id,
                Type = type
            };

            return View(createFeature);
        }

        // POST: Feature/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Title,Excerpt,FeatureTitles,FeatureDetails,ProfileImage,Longitude,Latitude,Type,CreatedAt,UpdatedAt")] FeatureViewModel featureViewModel
            ,IFormFile ProfileImage)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = _fileExtentions.UploadedFile(featureViewModel.ProfileImage, "features");

                var feature = new Feature()
                {
                    Id = featureViewModel.Id,
                    UserId = featureViewModel.UserId,
                    Title = featureViewModel.Title,
                    Excerpt = featureViewModel.Excerpt,
                    FeatureTitles = featureViewModel.FeatureTitles,
                    FeatureDetails = featureViewModel.FeatureDetails,
                    Image = uniqueFileName != null ? uniqueFileName : null,
                    Longitude = featureViewModel.Longitude != null ? featureViewModel.Longitude : null,
                    Latitude = featureViewModel.Latitude != null ? featureViewModel.Latitude : null,
                    Type = featureViewModel.Type,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                await _featureService.CreateAsync(feature);
                return RedirectToAction(nameof(Index), new { type = feature.Type });
            }
            return View(featureViewModel);
        }

        // GET: Feature/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feature = await _featureService.FindFeatureByIdAsync(id);

            if (feature == null)
            {
                return NotFound();
            }

            return View(feature);
        }

        // POST: Feature/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Title,Excerpt,FeatureTitles,FeatureDetails,Image,Longitude,Latitude,Type,UpdatedAt")] Feature feature, IFormFile updateImage)
        {
            if (id != feature.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    feature.UpdatedAt = DateTime.Now;
                    if (updateImage != null)
                    {
                        //Old Image Delete operation goes here
                        var directory = _fileExtentions._rootImageDirectory + "/features/" + feature.Image;
                        _fileExtentions.DeleteFile(directory);

                        //Adding newly added image to directory.
                        feature.Image = _fileExtentions.UploadedFile(updateImage, "features");
                    }
                    await _featureService.UpdateAsync(feature);
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!await FeatureExists(feature.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { type = feature.Type });
            }
            return View(feature);
        }

        // GET: Feature/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feature = await _featureService.FindFeatureByIdAsync(id);

            if (feature == null)
            {
                return NotFound();
            }

            return View(feature);
        }

        // POST: Feature/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feature = await _featureService.FindFeatureByIdAsync(id);
            await _featureService.DeleteAsync(feature.Id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> FeatureExists(int id)
        {
            return await _featureService.Exist(id);
        }
    }
}
