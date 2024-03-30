using DataLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Components
{
    public class TopMenuViewComponent:ViewComponent
    {
        private readonly IShopContext Context = null;
        public TopMenuViewComponent(IShopContext _Context)
        {
            this.Context = _Context;
        }

        public IViewComponentResult Invoke()
        {
            var _list = Context.Categories.Select(p=>new {p.ID,p.Name }).ToList();

            var model =new Models.TopMenuModel();
            foreach (var item in _list)
            {
                model.ListMenu.Add(new Models.TopMenuItemModel() {
                    ID=item.ID,
                    MenuName=item.Name
                });
            }
            return View(model);
        }
    }
}
