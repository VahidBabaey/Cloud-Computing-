using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    [Area("Admin")]
    public class ProductListModel
    {
        public ProductListModel()
        {
            //Products = new List<ProductListItem>();
            Categories = new List<SelectListItem>();
            SearchName = "";
            SearchSeller = "";
        }
        public string SearchName { get; set; }
        public string SearchSeller { get; set; }
        public int SellerID { get; set; }
        public int CategoryId { get; set; }

        public string ProductsJson { get; set; } = "";
        //public IList<ProductListItem> Products { get; set; }

        public IList<SelectListItem> Categories { get; set; }

    }

    //public class ProductListItem
    //{
    //    public int ID { get; set; }
    //    public string Name { get; set; }
    //    public string Sku { get; set; }
    //    public bool Published { get; set; }
    //    public string PublishDate { get; set; }


    //    public int Price { get; set; }
    //    public int OldPrice { get; set; }

    //    public int CategoryID { get; set; }

    //    public string CategoryName { get; set; }
    //}
}
