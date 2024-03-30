using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json;

namespace Shop.Controllers
{
    [Area("Admin")]
    
    [Authorize(Roles = "Administrator,Seller")]
    public class ProductController : Controller
    {

        private readonly IShopContext Context = null;

        public ProductController(IShopContext _Context)
        {
            this.Context = _Context;
        }
       

        public IActionResult Index()
        {

            return   Redirect("/Admin/Product/List");
        }

        #region Utilities
        [NonAction]
        private void PrepareProductModelCategories(ProductModel model)
        {
            var categories = Context.Categories.ToList();


            foreach (var item in categories)
            {
                model.Categories.Add(new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.ID.ToString()
                });
            }
        }
        [NonAction]
        private void PrepareListModel(Models.ProductListModel model)
        {
            var _listproducts = Context.Products.Include(p => p.Category).Where(p => (p.Name.Contains(model.SearchName) || string.IsNullOrEmpty(model.SearchName)) && (p.CategoryID == model.CategoryId || model.CategoryId == 0)).ToList()
                .Select(p => new { CategoryName = p.Category.Name, p.CategoryID, p.ID, p.Name, p.OldPrice, p.Price, PublishDate = p.PublishDate.ToPersian(), p.Published, p.Sku }).ToList();

            model.ProductsJson = JsonConvert.SerializeObject(_listproducts);

            //foreach (var product in _listproducts)
            //{
            //    model.Products.Add(new Models.ProductListItem
            //    {
            //        CategoryID = product.CategoryID,
            //        CategoryName = product.CategoryName,
            //        ID = product.ID,
            //        Name = product.Name,
            //        OldPrice = product.OldPrice,
            //        Price = product.Price,
            //        PublishDate = product.PublishDate.ToPersian(),
            //        Published = product.Published,
            //        Sku = product.Sku
            //    });
            //}
        }
        [NonAction]
        private void PrepareCategoriesModel(Models.ProductListModel model)
        {
            var _categories = Context.Categories.Select(p => new { p.Name, p.ID }).ToList();
            model.Categories.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
            {
                Text = "--",
                Value = "0"
            });
            foreach (var item in _categories)
            {
                model.Categories.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                {
                    Text = item.Name,
                    Value = item.ID.ToString()
                });
            }
        }
        #endregion 



        public IActionResult List(Models.ProductListModel model)
        {

            PrepareListModel(model);
            PrepareCategoriesModel(model);
            
            return View(model);
        }
        public IActionResult GetProducts(Models.ProductListModel model)
        {

            var _listproducts = Context.Products.Include(p => p.Category).Where(p => (p.Name.Contains(model.SearchName) || string.IsNullOrEmpty(model.SearchName)) && (p.Seller.UserName.Contains(model.SearchSeller) || string.IsNullOrEmpty(model.SearchSeller)) && (p.CategoryID == model.CategoryId || model.CategoryId == 0)).ToList()
              .Select(p => new { CategoryName = p.Category.Name, SellerName = p.Seller.UserName,p.SellerID, p.CategoryID, p.ID, p.Name, p.OldPrice, p.Price, PublishDate = p.PublishDate.ToPersian(), p.Published, p.Sku }).ToList();
           
            return new OkObjectResult(_listproducts);
        }
        [HttpGet]
        public IActionResult Create(int? id)
        {
            var model = new Models.ProductModel();
            if (id.HasValue)
            {
                var product = Context.Products.Find(id);
                model.CategoryID = product.CategoryID;
                model.ID = product.ID;
                model.Name = product.Name;
                model.OldPrice = product.OldPrice;
                model.Price = product.Price;
                model.PublishDate = product.PublishDate;
                model.Published = product.Published;
                model.Sku = product.Sku;
            }
            PrepareProductModelCategories(model);
            return View(model);
        }

        [HttpGet]
        public IActionResult Detail(int? id)
        {
            var model = new Models.ProductModel();
            if (id.HasValue)
            {
                var product = Context.Products.Find(id);
                model.CategoryID = product.CategoryID;
                model.ID = product.ID;
                model.Name = product.Name;
                model.OldPrice = product.OldPrice;
                model.Price = product.Price;
                model.PublishDate = product.PublishDate;
                model.Published = product.Published;
                model.Sku = product.Sku;
            }
            PrepareProductModelCategories(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ProductModel model)
        {

            if (ModelState.IsValid && ModelState.ErrorCount == 0)
            {
                Domains.Product product = new Domains.Product();
                product.CategoryID = model.CategoryID;
                product.Name = model.Name;
                product.OldPrice = model.OldPrice;
                product.Price = model.Price;
                product.PublishDate = model.PublishDate;
                product.Published = model.Published;
                product.Sku = model.Sku;
                product.ID = model.ID;
                product.SellerID = model.SellerID;
                
                Context.Update(product);
                Context.SaveChanges();
                return RedirectToAction("List");
            }

            PrepareProductModelCategories(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            var _product = Context.Products.Find(id);
            Context.Products.Remove(_product);
            Context.SaveChanges();
            return Ok();
        }


        [HttpPost]
        public IActionResult SaveRelatedProducts(int ID,string IDS)
        {
            var Product = Context.Products.Include(p => p.RelatedProducts).SingleOrDefault(p => p.ID == ID);

            if (Product.RelatedProducts == null)
                Product.RelatedProducts = new List<Domains.RelatedProduct>();

            Product.RelatedProducts.Clear();
            var _listid = IDS.Split(';');
            foreach (var id in _listid)
            {
                Domains.RelatedProduct related = new Domains.RelatedProduct();
                //related.ProductId = ID;
                related.RelatedProductId =Convert.ToInt32(id);

                Product.RelatedProducts.Add(related);

            }
            Context.SaveChanges();

            return Ok();
        }
     

        [HttpGet]
        
        public IActionResult GetRelatedProducts(int ID)
        {
         
            var Product = Context.Products.Include(p => p.RelatedProducts).SingleOrDefault(p => p.ID == ID);


            var listid = Product.RelatedProducts.Select(p => p.RelatedProductId).ToList() ;

            var _list = Context.Products.Where(p => listid.Contains(p.ID)).Select(p=>new { p.Name,p.ID}).ToList();

            return new OkObjectResult(_list);

        }

        [HttpPost]
        public IActionResult SavePicture(int productId, int order, string pictureId)
        {
            var Product = Context.Products.Include(p => p.Pictures).SingleOrDefault(p => p.ID == productId);

            if(Product!=null)
            {
                Product.Pictures.Add(new Domains.ProductPicture {
                    PictureId=pictureId,
                    Order=order
                });
            }

            Context.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public IActionResult GetPictures(int productId)
        {
            var _list = Context.Products.Include(p => p.Pictures).SingleOrDefault(p => p.ID == productId).Pictures.OrderBy(p=>p.Order).Select(p => new {p.ID,p.PictureId,p.Order }).ToList();


            return new OkObjectResult(_list);
        }
    }
}
