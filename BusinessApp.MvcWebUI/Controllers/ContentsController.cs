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
using BusinessApp.CarpetWash.Entities.Concrete.Enums;
using Microsoft.AspNetCore.Identity;
using BusinessApp.CarpetWash.MvcWebUI.Models;
using BusinessApp.CarpetWash.MvcWebUI.Shared;
using Microsoft.AspNetCore.Hosting;
using System.Security.Cryptography.X509Certificates;

namespace BusinessApp.CarpetWash.MvcWebUI.Controllers
{
    public class ContentsController : Controller
    {
        private readonly IContentService _contentService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private FileExtentions fileExtentions;

        public ContentsController(IContentService contentService, ICategoryService categoryService, UserManager<User> userManager, IWebHostEnvironment webHostEnvironment, ITagService tagService)
        {
            _contentService = contentService;
            _categoryService = categoryService;
            _userManager = userManager;
            _tagService = tagService;
            _webHostEnvironment = webHostEnvironment;
            fileExtentions = new FileExtentions(_webHostEnvironment);
        }

        // GET: Contents
        public async Task<IActionResult> Index(ContentType type, bool IsFrontSideIndex = false, int page = 1)
        {
            ICollection<Content> contents;
            switch (type)
            {
                case ContentType.Unknown:
                    contents = await _contentService.GetAllContentsAsync(ContentType.Unknown);
                    break;
                case ContentType.Service:
                    contents = await _contentService.GetAllContentsAsync(ContentType.Service);
                    break;
                case ContentType.ServiceRegion:
                    contents = await _contentService.GetAllContentsAsync(ContentType.ServiceRegion);
                    break;
                case ContentType.Post:
                    contents = await _contentService.GetAllContentsAsync(ContentType.Post);
                    break;
                default:
                    contents = null;
                    break;
            }

            if (IsFrontSideIndex)
            {
                int pageSize = 2;

                var frontSideContents = new ContentViewModel()
                {
                    Contents = contents.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                    PageCount = (int)Math.Ceiling(contents.Count / (double)pageSize),
                    PageSize = pageSize,
                    CurrentPage = page,
                    Type = type,
                    IsFrontSideIndex = IsFrontSideIndex
                };

                return View("FrontSide/Index", frontSideContents);
            }
            return View(contents);
        }


        //// GET: Contents/Details/5
        public async Task<IActionResult> Details(int? id, bool IsFrontSideDetails = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var content = await _contentService.FindContentByIdAsync(id);
            if (content == null)
            {
                return NotFound();
            }

            if (IsFrontSideDetails)
            {
                return View("FrontSide/Details", content);
            }
            return View(content);
        }

        // GET: Contents/Create
        public async Task<IActionResult> Create(ContentType type)
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var createContent = new ContentViewModel()
            {
                CategoryList = new SelectList(categories, "Id", "Name"),
                UserId = user.Id,
                Type = type
            };

            return View(createContent);
        }

        //POST: Contents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,CategoryId,Title,Excerpt,Text,ProfileImage,IsActive,Type")] ContentViewModel contentViewModel, string tags)
        {
            string[] tagsarray;
            if (ModelState.IsValid)
            {
                string uniqueFileName = fileExtentions.UploadedFile(contentViewModel.ProfileImage, "content");

                var content = new Content()
                {
                    Id = contentViewModel.Id,
                    Type = contentViewModel.Type,
                    CategoryId = contentViewModel.CategoryId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Excerpt = contentViewModel.Excerpt,
                    Image = uniqueFileName != null ? uniqueFileName : null,
                    IsActive = contentViewModel.IsActive,
                    Text = contentViewModel.Text,
                    Title = contentViewModel.Title,
                    UserId = contentViewModel.UserId,
                    VisitCount = 0
                };
                await _contentService.CreateAsync(content);

                if (!string.IsNullOrEmpty(tags))
                {
                    tagsarray = tags.Split(',');

                    for (int i = 0; i < tagsarray.Length; i++)
                    {
                        var tag = new Tag()
                        {
                            ContentId = content.Id,
                            Name = tagsarray[i]
                        };
                        await _tagService.CreateAsync(tag);
                    }
                }
                return RedirectToAction("Index", "Contents", new { type = content.Type });
            }
            return View(contentViewModel);
        }

        //// GET: Contents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _categoryService.GetAllCategoriesAsync();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var content = await _contentService.FindContentByIdAsync(id);
            string tagsAsString = string.Join(",", content.Tags.Select(x => x.Name).ToList());

            var updateContent = new ContentViewModel()
            {
                CategoryList = new SelectList(categories, "Id", "Name"),
                UserId = user.Id,
                Type = content.Type,
                CategoryId = content.CategoryId,
                CurrentImage = content.Image,
                Excerpt = content.Excerpt,
                Id = content.Id,
                IsActive = content.IsActive,
                Text = content.Text,
                Title = content.Title,
                CreatedAt = content.CreatedAt,
                Tags = tagsAsString
            };

            if (content == null)
            {
                return NotFound();
            }
            return View(updateContent);
        }

        //// POST: Contents/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,CategoryId,Title,Excerpt,Text,ProfileImage,IsActive,Type,CurrentImage")] ContentViewModel contentViewModel, string tags)
        {
            if (id != contentViewModel.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                string uniqueFileName;
                string[] tagsarray;
                var content = new Content();
                try
                {
                    await _tagService.DeleteByContentId(id);
                    content.Id = contentViewModel.Id;
                    content.UserId = contentViewModel.UserId;
                    content.CategoryId = contentViewModel.CategoryId;
                    content.Title = contentViewModel.Title;
                    content.Excerpt = contentViewModel.Excerpt;
                    content.Text = contentViewModel.Text;
                    content.IsActive = contentViewModel.IsActive;
                    content.Type = contentViewModel.Type;
                    content.Image = contentViewModel.CurrentImage;
                    content.CreatedAt = contentViewModel.CreatedAt;
                    content.UpdatedAt = DateTime.Now;

                    if (contentViewModel.ProfileImage != null)
                    {
                        //Old Image Delete operation goes here
                        var directory = fileExtentions._rootImageDirectory + "/content/" + content.Image;
                        fileExtentions.DeleteFile(directory);

                        //Adding newly added image to directory.
                        uniqueFileName = fileExtentions.UploadedFile(contentViewModel.ProfileImage, "content");
                        content.Image = uniqueFileName;
                    }

                    await _contentService.UpdateAsync(content);
                    // This can be change later.
                    if (!string.IsNullOrEmpty(tags))
                    {
                        tagsarray = tags.Split(',');
                        for (int i = 0; i < tagsarray.Length; i++)
                        {
                            var tag = new Tag()
                            {
                                ContentId = content.Id,
                                Name = tagsarray[i]
                            };
                            await _tagService.CreateAsync(tag);
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ContentExists(content.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { type = content.Type });
            }
            return View(contentViewModel);
        }

        //// GET: Contents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var content = await _contentService.FindContentByIdAsync(id);
            if (content == null)
            {
                return NotFound();
            }

            return View(content);
        }

        //// POST: Contents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var content = (await _contentService.FindContentByIdAsync(id));
            if (content.Image != null)
            {
                //Old Image Delete operation goes here
                var directory = fileExtentions._rootImageDirectory + "/content/" + content.Image;
                fileExtentions.DeleteFile(directory);
            }

            await _tagService.DeleteByContentId(id);
            await _contentService.DeleteAsync(id);
            return RedirectToAction(nameof(Index), new { type = content.Type });
        }
        private async Task<bool> ContentExists(int id)
        {
            return await _contentService.Exist(id);
        }
    }
}
