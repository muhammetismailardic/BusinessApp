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
using Microsoft.AspNetCore.Authorization;

namespace BusinessApp.CarpetWash.MvcWebUI.Controllers
{
    [Authorize]
    public class TagsController : Controller
    {
        private readonly ITagService _tagService;
        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        // GET: Tags
        public async Task<IActionResult> Index()
        {
            return View(await _tagService.GetAllTagsAsync());
        }

        // GET: Tags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _tagService.FindTagByIdAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // GET: Tags/Create
        public IActionResult Create()
        {
            //TODO: This can be added later on.
            //ViewData["ContentId"] = new SelectList(_context.Contents, "Id", "Id");
            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,ContentId,Slug,ItemType,Name,CreatedAt,UpdatedAt")] Tag tag)
        {
            //TODO: This can be added later on.
            if (ModelState.IsValid)
            {
                //_context.Add(tag);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ContentId"] = new SelectList(_context.Contents, "Id", "Id", tag.ContentId);
            return View(tag);
        }

        // GET: Tags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //TODO: This can be enhanced later on
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _tagService.FindTagByIdAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            //ViewData["ContentId"] = new SelectList(_context.Contents, "Id", "Id", tag.ContentId);
            return View(tag);
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ContentId,Slug,ItemType,Name,CreatedAt")] Tag tag)
        {
            if (id != tag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tag.UpdatedAt = DateTime.Now;
                    await _tagService.UpdateAsync(tag);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TagExists(tag.Id))
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
            return View(tag);
        }

        // GET: Tags/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _tagService.FindTagByIdAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _tagService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        private async Task<bool> TagExists(int id)
        {
            return await _tagService.Exist(id);
        }
    }
}
