using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class ProductCategoryModel
    {
        public ProductCategoryModel()
        {
            Prodcuts = new List<ProductItemModel>();
        }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public List<ProductItemModel> Prodcuts { get; set; }
    }


    public class ProductItemModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Published { get; set; }
        public DateTime PublishDate { get; set; }
        public int Price { get; set; }
        public int OldPrice { get; set; }
        public string SellerName { get; set; }
        public string Imageurl { get; set; }
      
    }
}
