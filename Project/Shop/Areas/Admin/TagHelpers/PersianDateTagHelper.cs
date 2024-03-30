using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.TagHelpers
{
    [HtmlTargetElement("persian-date",Attributes = ForAttribute)]
    public class PersianDateTagHelper:TagHelper
    {
        private const string ForAttribute = "asp-for";

        [HtmlAttributeName(ForAttribute)]
        public ModelExpression For { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string content = $"<input id='{For.Name}1' name='{For.Name}1' value='{For.Model}'/><input id='{For.Name}' name='{For.Name}' type='hidden' value='{For.Model}'/>";
            content += "<script>$('#"+For.Name+"1').persianDatepicker({  format: 'YYYY-MM-DD',onSelect: function (code) { var date = new Date(code); var year = date.getFullYear();   var month = date.getMonth() + 1;  var day = date.getDate();var str = year + '/' + month + '/' + day; $('#"+For.Name+"').val(str); } }); </script>";

            output.Content.SetHtmlContent(content);
            base.Process(context, output);
        }
    }
}
