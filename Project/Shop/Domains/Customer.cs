using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domains
{
    public class Customer:IdentityUser<int>
    {
        //public int SellerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string ActivateCode { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }

    public class CustomerRole : IdentityRole<int>
    {

    }

}
