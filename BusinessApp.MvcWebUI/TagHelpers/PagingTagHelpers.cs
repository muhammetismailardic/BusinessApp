using BusinessApp.CarpetWash.Entities.Concrete.Enums;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApp.CarpetWash.MvcWebUI.TagHelpers
{
    [HtmlTargetElement("content-list-pager")]
    public class PagingTagHelper : TagHelper
    {
        [HtmlAttributeName("page-size")]
        public int PageSize { get; set; }

        [HtmlAttributeName("page-count")]
        public int PageCount { get; set; }

        [HtmlAttributeName("current-type")]
        public ContentType CurrentType { get; set; }

        [HtmlAttributeName("current-page")]
        public int CurrentPage { get; set; }

        [HtmlAttributeName("is-front-side-index")]
        public bool IsFrontSideIndex { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div class='blog-pagination'";
            
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<ul class='justify-content-center'>");

            stringBuilder.AppendFormat("<li>");
            stringBuilder.AppendFormat("<a href='#'>");
            stringBuilder.AppendFormat("<i class='icofont-rounded-left'>");
            stringBuilder.AppendFormat("</i>");
            stringBuilder.AppendFormat("</a>");
            stringBuilder.AppendFormat("</i>");

            for (int i = 1; i <= PageCount; i++)
            {
                stringBuilder.AppendFormat("<li class='{0}'>", i == CurrentPage ? "active" : "");
                stringBuilder.AppendFormat("<a href='/Contents/index?type={0}&IsFrontSideIndex={1}&page={2}'>{3}</a>",
                    CurrentType, IsFrontSideIndex, i, i);
                stringBuilder.Append("</li>");
            }

            stringBuilder.AppendFormat("<li>");
            stringBuilder.AppendFormat("<a href='#'>");
            stringBuilder.AppendFormat("<i class='icofont-rounded-right'>");
            stringBuilder.AppendFormat("</i>");
            stringBuilder.AppendFormat("</a>");
            stringBuilder.AppendFormat("</i>");

            output.Content.SetHtmlContent(stringBuilder.ToString());

            base.Process(context, output);
        }
    }
}