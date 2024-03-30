using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.TagHelpers
{
    [HtmlTargetElement("caption-view",Attributes = TitleFor)]
    public class CaptionViewTagHelper:TagHelper
    {
        private const string TitleFor= "asp-title";

        [HtmlAttributeName(TitleFor)]
        public string TitleAttribute { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            string content = $"<i class='fa fa-codepen'></i> <span class='h5'>{TitleAttribute}</span><hr />";

            output.Content.SetHtmlContent(content);
            base.Process(context, output);
        }
    }
}
