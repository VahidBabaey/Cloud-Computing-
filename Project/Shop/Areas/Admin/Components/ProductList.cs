using DataLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Components
{
    public class ProductListViewComponent:ViewComponent
    {
        private readonly IShopContext Context = null;
        public ProductListViewComponent(IShopContext _Context)
        {
            this.Context = _Context;
        }
        public IViewComponentResult Invoke()
        {
            Models.ProductListModel model = new Models.ProductListModel();
            PrepareCategoriesModel(model);
            return View(model);
        }
       
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
    }
}
