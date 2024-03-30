using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    [Area("Admin")]

    public class ProductModel
    {
        public ProductModel()
        {
            Categories = new List<SelectListItem>();
            SellerName = "";
            PublishDate = DateTime.Now;

        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public bool Published { get; set; }
        public DateTime PublishDate { get; set; }


        public int Price { get; set; }
        public int OldPrice { get; set; }
        public string SellerName { get; set; }
        public int SellerID { get; set; }
        public int CategoryID { get; set; }

        public IList<SelectListItem> Categories { get; set; }
    }
}
