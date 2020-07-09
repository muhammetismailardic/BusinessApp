using BusinessApp.CarpetWash.Business.Abstract;
using BusinessApp.CarpetWash.Entities.Concrete.Enums;
using BusinessApp.MvcWebUI.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BusinessApp.MvcWebUI.Controllers
{
    public class SitemapController : Controller
    {
        private readonly IContentService _contentService;
        public SitemapController(IContentService contentService)
        {
            _contentService = contentService;
        }

        [Route("sitemap")]
        public async Task<ActionResult> SitemapAsync()
        {
            string baseUrl = "https://www.temizpakhaliyikama.com/";

            // get a list of published articles
            var contents = await _contentService.GetAllContentsAsync(ContentType.All);

            // get last modified date of the home page
            var siteMapBuilder = new SitemapBuilder();

            // add the home page to the sitemap
            siteMapBuilder.AddUrl(baseUrl, modified: DateTime.UtcNow, changeFrequency: ChangeFrequency.Weekly, priority: 1.0);

            // add the blog posts to the sitemap
            foreach (var content in contents)
            {
                siteMapBuilder.AddUrl(baseUrl + content.Slug + "-" + content.Id, modified: content.UpdatedAt, changeFrequency: ChangeFrequency.Weekly, priority: 0.9);
            }

            // generate the sitemap xml
            string xml = siteMapBuilder.ToString();
            return Content(xml, "text/xml");
        }
    }

}