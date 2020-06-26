using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessApp.CarpetWash.Entities.Concrete;
using BusinessApp.CarpetWash.MvcWebUI.Data;
using BusinessApp.CarpetWash.Business.Abstract;
using BusinessApp.CarpetWash.Entities.Concrete.Enums;

namespace BusinessApp.CarpetWash.MvcWebUI.Controllers
{
    public class FeatureController : Controller
    {
        IFeaturesService _featureService;
        public FeatureController(IFeaturesService featureService)
        {
            _featureService = featureService;
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Feature/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Title,Excerpt,FeatureTitles,FeatureDetails,Image,Longitude,Latitude,Type,CreatedAt,UpdatedAt")] Feature feature)
        {
            if (ModelState.IsValid)
            {
                await _featureService.CreateAsync(feature);

                return RedirectToAction(nameof(Index));
            }

            return View(feature);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Title,Excerpt,FeatureTitles,FeatureDetails,Image,Longitude,Latitude,Type,CreatedAt,UpdatedAt")] Feature feature)
        {
            if (id != feature.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _featureService.UpdateAsync(feature);
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (! await FeatureExists(feature.Id))
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
