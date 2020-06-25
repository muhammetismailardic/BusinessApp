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
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        public async Task<IActionResult> Index(int contentId)
        {
            var commentsByContentId = await _commentService.GetAllCommentsByContentIdAsync(contentId);

            return View(commentsByContentId);
        }

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
                return RedirectToAction("Details", "Contents", new { Id = commentViewModel.ContentId });
            }
            return RedirectToAction("Details", "Contents", new { Id = commentViewModel.ContentId, IsFrontSideDetails = true });
        }
    }
}