using DataLayer;
using Domains;
using Microsoft.AspNetCore.Identity;
using Shop.Areas.Admin.ShopCore.Interfaces;
using ShopCore.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Areas.Admin.ShopCore.Services
{
    public class AccountService : IAccount
    {
        private readonly IShopContext Context;
        private readonly UserManager<Customer> usermanager;
        public AccountService(IShopContext _Context, UserManager<Customer> _usermanager)
        {
            this.Context = _Context;
            this.usermanager = _usermanager;
        }

        public bool ActivateUser(string Code)
        {
            var user = usermanager.Users.FirstOrDefault(p => p.IsActive == false && p.ActivateCode == Code);
            if(user != null)
            {
                user.IsActive = true;
                user.ActivateCode = CodeGenerator.ActivateCode();
                Context.Update(user);
                Context.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }
        }

        public bool ExistMobile(string Mobile)
        {
            var user = usermanager.Users.FirstOrDefault(p => p.PhoneNumber == Mobile);
            if(user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ResetPassword(string Code, string Password)
        {
            var user = usermanager.Users.FirstOrDefault(p => p.ActivateCode == Code);
            if(user != null)
            {
                user.PasswordHash = Password;
                user.ActivateCode = CodeGenerator.ActivateCode();
                Context.Update(user);
                Context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
