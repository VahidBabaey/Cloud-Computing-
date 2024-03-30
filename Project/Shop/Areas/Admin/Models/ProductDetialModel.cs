using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class ProductDetialModel
    {
        public ProductDetialModel()
        {
            ImageUrls = new List<string>();
            RelatedProdcuts = new List<ProductItemModel>();
        }

        public int ID { get; set; }
       
        public string Name { get; set; }
        public bool Published { get; set; }
        public DateTime PublishDate { get; set; }
        public int Price { get; set; }
        public int OldPrice { get; set; }
        public string Imageurl { get; set; }

        public List<string> ImageUrls { get; set; }

        public List<ProductItemModel> RelatedProdcuts { get; set; }
    }
}
