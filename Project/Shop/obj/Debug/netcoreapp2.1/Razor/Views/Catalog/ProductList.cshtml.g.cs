#pragma checksum "D:\projects\web3Cloud\Shop\Views\Catalog\ProductList.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5cd06a8eaecc78713b708f23fbf6a9f019ea6c0ddc6ac9aa6080218a4baa3259"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Catalog_ProductList), @"mvc.1.0.view", @"/Views/Catalog/ProductList.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Catalog/ProductList.cshtml", typeof(AspNetCore.Views_Catalog_ProductList))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"5cd06a8eaecc78713b708f23fbf6a9f019ea6c0ddc6ac9aa6080218a4baa3259", @"/Views/Catalog/ProductList.cshtml")]
    public class Views_Catalog_ProductList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Models.ProductCategoryModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(36, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "D:\projects\web3Cloud\Shop\Views\Catalog\ProductList.cshtml"
  
    Layout = "~/Views/Shared/_ColumnTwo.cshtml";

#line default
#line hidden
            BeginContext(95, 132, true);
            WriteLiteral("\r\n<div class=\"container-fluid p-4\">\r\n    <div id=\"divProduct\">\r\n        <h4>Movies</h4>\r\n        <hr />\r\n        <div class=\"row\">\r\n");
            EndContext();
#line 12 "D:\projects\web3Cloud\Shop\Views\Catalog\ProductList.cshtml"
             foreach (var product in Model.Prodcuts)
            {

#line default
#line hidden
            BeginContext(296, 64, true);
            WriteLiteral("                <div class=\"col-sm-3 p-1\">\r\n                    ");
            EndContext();
            BeginContext(361, 36, false);
#line 15 "D:\projects\web3Cloud\Shop\Views\Catalog\ProductList.cshtml"
               Write(Html.Partial("_PictureBox", product));

#line default
#line hidden
            EndContext();
            BeginContext(397, 26, true);
            WriteLiteral("\r\n                </div>\r\n");
            EndContext();
#line 17 "D:\projects\web3Cloud\Shop\Views\Catalog\ProductList.cshtml"
            }

#line default
#line hidden
            BeginContext(438, 48, true);
            WriteLiteral("        </div>\r\n        </div>\r\n\r\n    </div>\r\n\r\n");
            EndContext();
            DefineSection("left", async() => {
                BeginContext(506, 1313, true);
                WriteLiteral(@"

    <div class=""form-group"">
        <label asp-for=""fromPrice"">From price:</label>
        <input class=""form-control"" id=""fromPrice"" type=""number"">
    </div>
    <div class=""form-group"">
        <label asp-for=""fromPrice"">To price:</label>
        <input class=""form-control"" id=""toPrice"" type=""number"">
    </div>
    <button class=""btn btn-block btn-outline-secondary"" onclick=""SearchProduct()""><i class=""fa fa-search""></i>Search</button>



    <script>

        function SearchProduct() {

            $.ajax({
                url: ""/Catalog/ProdcutCategorySearch"",
                dataType: ""text"",
                type: ""Get"",
                data: {
                   
                    fromPrice: $(""#fromPrice"").val(),
                    toPrice: $(""#toPrice"").val(),
                
                }
                ,
                success: function (data, status, jqxhr) {
                    $(""#divProduct"").html(data);
                },
                error: func");
                WriteLiteral(@"tion (jqxhr,status) {

                    alert(status);
                },
                beforeSend: function (jqxhr,setting) {


                },
                complete: function (jqxhr, status) {


                }

            });

        }


    </script>
");
                EndContext();
            }
            );
            BeginContext(1822, 2, true);
            WriteLiteral("\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Models.ProductCategoryModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
