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
using BusinessApp.CarpetWash.MvcWebUI.Models;
using BusinessApp.CarpetWash.MvcWebUI.Shared;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace BusinessApp.CarpetWash.MvcWebUI.Controllers
{
    public class CommentController : Controller
    {
        IContentService _contentService;
        ICommentService _commentService;
        public CommentController(IContentService contentService, ICommentService commentService)
        {
            _commentService = commentService;
            _contentService = contentService;
        }

        // GET: Category
        public async Task<IActionResult> Index(int contentId)
        {
            var commentsByContentId = await _commentService.GetAllCommentsByContentIdAsync(contentId);

            return View(commentsByContentId);
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ContentId,ParentId,UserId,Name,Email,Text,IsAnonymous")] CommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {
                var comment = new Comment()
                {
                    ContentId = commentViewModel.ContentId,
                    ParentId = commentViewModel.ParentId,
                    UserId = commentViewModel.UserId,
                    Name = commentViewModel.Name,
                    Email = commentViewModel.Email,
                    Text = commentViewModel.Text,
                    IsAnonymous = commentViewModel.IsAnonymous,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                await _commentService.CreateAsync(comment);
                return RedirectToAction("Details", "Contents", new { Id = commentViewModel.ContentIdmo});
            }
            return RedirectToAction("Details", "Contents", new { Id = commentViewModel.ContentId, IsFrontSideDetails = true });
        }

        // GET: Category/Edit/5
        // public async Task<IActionResult> Edit(int? id)
        // {
        //     var category = await _categoryService.FindCategoryByIdAsync(id);
        //     if (category == null)
        //     {
        //         return NotFound();
        //     }
        //     var categoryViewModel = new CategoryViewModel()
        //     {
        //         Id = category.Id,
        //         Name = category.Name,
        //         CurrentImage = category.Image,
        //         CreatedAt = category.CreatedAt
        //     };

        //     return View(categoryViewModel);
        // }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int Id, CategoryViewModel categoryViewModel)
        // {
        //     if (Id != categoryViewModel.Id)
        //     {
        //         return NotFound();
        //     }

        //     if (ModelState.IsValid)
        //     {
        //         var category = new Category();
        //         string uniqueFileName;

        //         category.Id = categoryViewModel.Id;
        //         category.Name = categoryViewModel.Name;
        //         category.CreatedAt = categoryViewModel.CreatedAt;
        //         category.UpdatedAt = DateTime.Today;

        //         if (categoryViewModel.ProfileImage != null)
        //         {
        //             uniqueFileName = fileExtentions.UploadedFile(categoryViewModel.ProfileImage, "category");
        //             category.Image = uniqueFileName;

        //             //Old Image Delete operation goes here
        //             var categoryImage = (await _categoryService.FindCategoryByIdAsync(Id)).Image;
        //             var directory = fileExtentions._rootImageDirectory + "/category/" + categoryImage;
        //             fileExtentions.DeleteFile(directory);
        //         }

        //         else { category.Image = categoryViewModel.CurrentImage; }

        //         try
        //         {
        //             await _categoryService.UpdateAsync(category);
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!await CategoryExists(category.Id))
        //             {
        //                 return NotFound();
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(categoryViewModel);
        // }

        // GET: Category/Delete/5
        // public async Task<IActionResult> Delete(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var category = await _categoryService.FindCategoryByIdAsync(id);
        //     if (category == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(category);
        // }

        // POST: Category/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(int id)
        // {
        //     // var categoryImage = (await _categoryService.FindCategoryByIdAsync(id)).Image;

        //     // if(categoryImage != null) 
        //     // {
        //     //     //Old Image Delete operation goes here
        //     //     var directory = fileExtentions._rootImageDirectory + "/category/" + categoryImage;
        //     //     fileExtentions.DeleteFile(directory);
        //     // }

        //     // await _categoryService.DeleteAsync(id);
        //     // return RedirectToAction(nameof(Index));
        // }
        // private async Task<bool> CategoryExists(int id)
        // {
        //     return await _commentService.Exist(id);
        // }
    }
}