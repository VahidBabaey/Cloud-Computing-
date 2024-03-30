using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public readonly IShopContext Context = null;
        public HomeController(IShopContext _Context)
        {
            this.Context = _Context;
        }


        public IActionResult Index()
        {
            
            return RedirectToAction("ProductList1");
        }
        public IActionResult ProductList1(Models.ProductCategoryModel model)
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
        public IActionResult Problem()
        {

            return new StatusCodeResult(500);
        }
    }
}
