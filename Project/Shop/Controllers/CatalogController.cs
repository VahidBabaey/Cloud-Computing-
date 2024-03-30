using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [AllowAnonymous]
    public class CatalogController : Controller
    {
        private readonly IShopContext Context = null;

        public CatalogController(IShopContext _Context)
        {
            this.Context = _Context;
        }

        [HttpGet("/Category/{categoryName}/")]
        public IActionResult ProdcutCategory(string categoryName)
        {
            var prodcuts = Context.Products.Include(p => p.Category).Where(p => p.Category.Name.Contains(categoryName))
                .Select(p => new { p.ID, p.Name, p.OldPrice, p.Price, p.PublishDate, p.Published, p.Pictures.OrderBy(o => o.Order).FirstOrDefault().PictureId, p.CategoryID }).ToList();
            var model = new Models.ProductCategoryModel();
            foreach (var product in prodcuts)
            {
                model.CategoryID = product.CategoryID;
                model.Prodcuts.Add(new Models.ProductItemModel()
                {
                    ID = product.ID,
                    Name = product.Name,
                    OldPrice = product.OldPrice,
                    Price = product.Price,
                    PublishDate = product.PublishDate,
                    Published = product.Published,
                    Imageurl = $"/Pictures/{product.PictureId}.jpg"
                });
            }
            model.CategoryName = categoryName;

            return View(model);
        }

        [HttpGet("/Catalog/{prodcutName}/")]
        public IActionResult ProdcutDetial(string prodcutName)
        {
            var product = Context.Products.Include(p => p.Category).Include(p => p.Pictures).Include(p => p.RelatedProducts).Where(p => p.Name.Contains(prodcutName)).
                Select(p => new { p.ID, p.Pictures.OrderBy(o => o.Order).FirstOrDefault().PictureId, p.Name, p.OldPrice, p.Pictures, p.Price, p.PublishDate, p.Published, p.RelatedProducts, CategoryName = p.Category.Name, p.CategoryID }).FirstOrDefault();

            var model = new Models.ProductDetialModel()
            {
                ID = product.ID,
                Imageurl = $"/Pictures/{product.PictureId}.jpg",
                Name = product.Name,
                OldPrice = product.OldPrice,
                Price = product.Price,
                PublishDate = product.PublishDate,
                Published = product.Published,

            };
            foreach (var pictureurl in product.Pictures)
            {
                model.ImageUrls.Add($"/Pictures/{pictureurl.PictureId}.jpg");
            }
            var listid = product.RelatedProducts.Select(p => p.RelatedProductId).ToList();
            if (listid != null && listid.Count != 0)
            {
                var _list = Context.Products.Include(p => p.Category).Where(p => listid.Contains(p.ID) && !p.Name.Contains(prodcutName)).Select(p => new { p.ID, p.Name, p.OldPrice, p.Price, p.PublishDate, p.Published, p.Pictures.OrderBy(o => o.Order).FirstOrDefault().PictureId, p.CategoryID }).ToList();
                foreach (var item in _list)
                {
                    model.RelatedProdcuts.Add(new Models.ProductItemModel() {
                        ID = item.ID,
                        Name = item.Name,
                        OldPrice = item.OldPrice,
                        Price = item.Price,
                        PublishDate = item.PublishDate,
                        Published = item.Published,
                        Imageurl = $"/Pictures/{item.PictureId}.jpg",
                        
                    });
                }
            }
            return View(model);
        }
        [HttpGet("/Catalog/ProductSearchName")]
        public IActionResult ProductSearchName(string searchProduct)   
        {
            var prodcuts = Context.Products.Include(p => p.Category).Where(p => p.Name.Contains(searchProduct))
                .Select(p => new { p.ID, p.Name, p.OldPrice, p.Price, p.PublishDate, p.Published, p.Pictures.OrderBy(o => o.Order).FirstOrDefault().PictureId, CategoryName = p.Category.Name, p.CategoryID }).ToList();
            var model = new Models.ProductCategoryModel();
            foreach (var product in prodcuts)
            {
                model.CategoryID = product.CategoryID;
                model.Prodcuts.Add(new Models.ProductItemModel()
                {
                    ID = product.ID,
                    Name = product.Name,
                    OldPrice = product.OldPrice,
                    Price = product.Price,
                    PublishDate = product.PublishDate,
                    Published = product.Published,
                    Imageurl = $"/Pictures/{product.PictureId}.jpg"
                });
            model.CategoryName = product.CategoryName;
            }

            return View(model);
        }
        [HttpGet("/Catalog/ProdcutCategorySearch")]
        public IActionResult ProdcutCategorySearch(int fromPrice, int toPrice)
        {
            var prodcuts = Context.Products.Include(p => p.Category).Where(p => p.Price >= fromPrice && p.Price <= toPrice)
                .Select(p => new { p.ID, p.Name, p.OldPrice, p.Price, p.PublishDate, p.Published, p.Pictures.OrderBy(o => o.Order).FirstOrDefault().PictureId, CategoryName = p.Category.Name, p.CategoryID }).ToList();
            var model = new Models.ProductCategoryModel();
            foreach (var product in prodcuts)
            {
                model.CategoryID = product.CategoryID;
                model.Prodcuts.Add(new Models.ProductItemModel()
                {
                    ID = product.ID,
                    Name = product.Name,
                    OldPrice = product.OldPrice,
                    Price = product.Price,
                    PublishDate = product.PublishDate,
                    Published = product.Published,
                    Imageurl = $"/Pictures/{product.PictureId}.jpg"
                });
            model.CategoryName = product.CategoryName;
            }

            return View(model);
        }
        [HttpGet("/Catalog/ProductList")]
        public IActionResult ProductList(Models.ProductCategoryModel model)
        {
            var _listproduct = Context.Products.Include(p => p.Category).Select(p => new { p.ID,p.SellerID, SellerName = p.Seller.UserName, p.Name, p.Price, p.OldPrice, p.PublishDate, p.Published, p.Pictures.OrderBy(o => o.Order).FirstOrDefault().PictureId, p.CategoryID }).ToList();
            foreach (var item in _listproduct)
            {
                model.CategoryID = item.CategoryID;
                model.Prodcuts.Add(new Models.ProductItemModel
                {
                    ID = item.ID,
                    Name = item.Name,
                    Price = item.Price,
                    OldPrice = item.OldPrice,
                    PublishDate = item.PublishDate,
                    Published = item.Published,
                    SellerName = item.SellerName,
                    Imageurl = $"/Pictures/{item.PictureId}.jpg"
                });
            }
            return View(model);
        }

    }
}
