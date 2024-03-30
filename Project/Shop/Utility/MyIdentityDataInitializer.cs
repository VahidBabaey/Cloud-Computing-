using Domains;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System
{
    public class MyIdentityDataInitializer
    {
        public static void SeedData(UserManager<Customer> userManager, RoleManager<CustomerRole> roleManager/*, UserManager<Seller> sellerManager, RoleManager<SellerRole> sellerroleManager*/)
        {
            SeedRoles(roleManager);
            SeedRoles(roleManager);
            //SeedSellerRoles(sellerroleManager);
        }

        public static void SeedUsers(UserManager<Customer> userManager)
        {
            if (userManager.FindByNameAsync("Admin").Result == null)
            {
                Customer user = new Customer();
                user.UserName = "Admin";
                user.Email = "Admin@yahoo.com";
                user.FirstName = "Morteza";
                user.LastName = "ghasemi";

                IdentityResult result = userManager.CreateAsync(user, "Aa123#").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "Administrator").Wait();
                }
            }


       

        }
        //public static void SeedSeller(UserManager<Seller> sellerManager)
        //{
        //    if (sellerManager.FindByNameAsync("Admin1").Result == null)
        //    {
        //        Seller user = new Seller();
        //        user.UserName = "Admin1";
        //        user.Email = "Admin1@yahoo.com";
        //        user.FirstName = "Morteza1";
        //        user.LastName = "ghasemi1";

        //        IdentityResult result = sellerManager.CreateAsync(user, "Aa123#1").Result;

        //        if (result.Succeeded)
        //        {
        //            sellerManager.AddToRoleAsync(user,
        //                                "AdministratorSeller").Wait();
        //        }
        //    }




        //}

        public static void SeedRoles(RoleManager<CustomerRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Registred").Result)
            {
                CustomerRole role = new CustomerRole();
                role.Name = "Registred";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                CustomerRole role = new CustomerRole();
                role.Name = "Administrator";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Seller").Result)
            {
                CustomerRole role = new CustomerRole();
                role.Name = "Seller";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
        //public static void SeedSellerRoles(RoleManager<SellerRole> sellerroleManager)
        //{
        //    if (!sellerroleManager.RoleExistsAsync("RegistredSeller").Result)
        //    {
        //        SellerRole role = new SellerRole();
        //        role.Name = "RegistredSeller";
        //        IdentityResult roleResult = sellerroleManager.CreateAsync(role).Result;
        //    }
        //    if (!sellerroleManager.RoleExistsAsync("AdministratorSeller").Result)
        //    {
        //        SellerRole role = new SellerRole();
        //        role.Name = "AdministratorSeller";
        //        IdentityResult roleResult = sellerroleManager.CreateAsync(role).Result;
        //    }
        //}
    }
}
