using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domains
{
    public class Product:BaseEntity
    {
        
        public string Name { get; set; }
        public string Sku { get; set; }
        public bool Published { get; set; }
        public DateTime PublishDate { get; set; }
        public int Price { get; set; }
        public int OldPrice { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public virtual Customer Seller { get; set; }
        public int SellerID { get; set; }

        public virtual ICollection<RelatedProduct> RelatedProducts { get; set; }
        public virtual ICollection<ProductPicture> Pictures { get; set; }
        
    }

    public class ProductPicture: BaseEntity
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string PictureId { get; set; }
        public int Order { get; set; }
    }
    public class RelatedProduct:BaseEntity
    {
       
        public int ProductId { get; set; }
        public int RelatedProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
