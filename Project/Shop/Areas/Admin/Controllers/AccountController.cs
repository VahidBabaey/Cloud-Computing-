using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Areas.Admin.Models;
using Shop.Areas.Admin.ShopCore.Services;
using ShopCore.Classes;
using Domains;
using Shop.Areas.Admin.ShopCore.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]

    public class AccountController : Controller
    {
        private readonly UserManager<Domains.Customer> userManager;
        private readonly RoleManager<CustomerRole> roleManager;
        private readonly SignInManager<Domains.Customer> signInManager;
        //private readonly UserManager<Domains.Seller> sellerManager;
        //private readonly RoleManager<Domains.SellerRole> sellerrollManager;
        //private readonly SignInManager<Domains.Seller> sellersignInManager;
        private readonly IShopContext Context;
        private IAccount account;
        // private AccountService(IShopContext _Context, UserManager<Customer> _usermanager) Account;
       // public AccountService account;

        public AccountController(UserManager<Domains.Customer> userManager, RoleManager<CustomerRole> roleManager
          , SignInManager<Domains.Customer> signInManager/*, UserManager<Domains.Seller> sellerManager*/
            //, RoleManager<Domains.SellerRole> sellerrollManager, SignInManager<Domains.Seller> sellersignInManager
            , IShopContext _context, IAccount _account)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            //this.sellerManager = sellerManager;
            //this.sellerrollManager = sellerrollManager;
            //this.sellersignInManager = sellersignInManager;
            this.account = _account;
            this.Context = _context;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            Admin.Models.CustomerRegisterModel customerModel = new Models.CustomerRegisterModel();
            return View(customerModel);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterSeller()
        {
            SellerRegisterModel sellerModel = new SellerRegisterModel();
            return View(sellerModel);
        }
        [AllowAnonymous]
        [HttpPost]
        [CustomValidationFilter]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Models.CustomerRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Customer customer = new Customer();
                customer.Email = model.Email;
                customer.FirstName = model.FirstName;
                customer.LastName = model.LastName;
                customer.PasswordHash = model.Password;
                customer.UserName = model.UserName;
                customer.PhoneNumber = model.PhoneNumber;
                customer.ActivateCode = CodeGenerator.ActivateCode();
                customer.IsActive = false;

                var result = await userManager.CreateAsync(customer, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(customer, "Registred");
                    await signInManager.SignInAsync(customer, isPersistent: false);
                    MessageSender sender = new MessageSender();
                    sender.SMS(customer.PhoneNumber, "Welcome to our Shop" + Environment.NewLine + "Activative Code" + customer.ActivateCode);
                    //return LocalRedirect("~/");
                    return RedirectToAction("Activate");

                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
                //try
                //{
                //    MessageSender sender = new MessageSender();
                //    sender.SMS(customer.PhoneNumber, "به فروشگاه ما خوش آمدین" + Environment.NewLine + "کد فعال‌سازی" + customer.ActivateCode);
                //}
                //catch
                //{

                //    throw;
                //}
            }
                return RedirectToAction("Activate");
           // return Ok();
        }
        [AllowAnonymous]
        [HttpPost]
        [CustomValidationFilter]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterSeller(SellerRegisterModel model)
        {
            var returnUrl = model.returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                Customer Seller = new Customer();
                //Seller.SellerID = model.SellerID;
                Seller.FirstName = null;
                Seller.LastName = null;
                Seller.UserName = model.UserName;                
                Seller.Email = model.Email;
                Seller.Address = model.Address;
                Seller.PasswordHash = model.Password;
                Seller.Tel = model.Tel;
                var result = await userManager.CreateAsync(Seller, model.Password);
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(Seller, "Seller");
                    await signInManager.SignInAsync(Seller, isPersistent: false);
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            return LocalRedirect(returnUrl);
        }
        [HttpGet]
        public IActionResult Activate()
        {
            CustomerActivateModel model = new CustomerActivateModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Activate(CustomerActivateModel model)
        {
           
            if (ModelState.IsValid)
            {
                //var user = userManager.Users.FirstOrDefault(p => p.ActivateCode == model.ActivateCode && p.IsActive == false);

                //if(user != null)
                //{
                //    user.IsActive = true;
                //    user.ActivateCode = CodeGenerator.ActivateCode();
                //    Context.Update(user);
                //    Context.SaveChanges();
                //    return LocalRedirect("~/");

                //}
                //else
                //{
                //    ModelState.AddModelError("ActivateCode", "کد فعال سازی معتبر نیست");
                //}
                if (account.ActivateUser(model.ActivateCode))
                {
                    return LocalRedirect("~/");
                }

                else
                {
                    ModelState.AddModelError("ActivateCode", "کد فعال سازی معتبر نیست");
                }
            }
            return View(model);
        }


        [HttpGet("/Account/Login")]
        [AllowAnonymous]
        public IActionResult LogIn()
        {
            var model = new CustomerLoginModel();
            return View(model);
        }
        [HttpGet("/Account/LoginSeller")]
        [AllowAnonymous]
        public IActionResult LoginSeller()
        {
            var model = new SellerLoginModel();
            return View(model);
        }
        [HttpPost("/Account/LoginSeller")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginSeller(SellerLoginModel model)
        {
            var returnUrl = model.returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.UserName,
                    model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {

                    return LocalRedirect(returnUrl);
                }
                if (result.IsLockedOut)
                {
                    return LocalRedirect("~/Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Password or Email is not correct");
                    return View(model);
                }
            }
            return View(model);
        }
        [HttpPost("/Account/Login")]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn(CustomerLoginModel model)
        {
            var returnUrl = model.returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.UserName,
                    model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {

                    return LocalRedirect(returnUrl);
                }
                if (result.IsLockedOut)
                {
                    return LocalRedirect("~/Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Password or Username is not correct");
                    return View(model);
                }
            }
            return View(model);
        }
        [HttpGet("/Account/Logout")]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            await signInManager.SignOutAsync();
            return LocalRedirect(returnUrl);
        }
        [HttpGet("/Account/LogoutSeller")]
        public async Task<IActionResult> LogoutSeller(string returnUrl)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            await signInManager.SignOutAsync();
            return LocalRedirect(returnUrl);
        }
        [HttpGet("/Account/LogoutAd")]
        public async Task<IActionResult> LogoutAd(string returnUrl)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            await signInManager.SignOutAsync();
            return LocalRedirect(returnUrl);
        }

        [AllowAnonymous]
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> CheckUserNameAsync(string UserName)
        {
            var isuser = await userManager.FindByNameAsync(UserName) == null;
           // var isuser = userManager.Users.FirstOrDefault(p => p.UserName == model.UserName);
            return Json(data: isuser);
        }
        [AllowAnonymous]
        public IActionResult Forget()
        {
            CustomerForgetModel model = new CustomerForgetModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Forget(CustomerForgetModel model)
        {
            if(ModelState.IsValid)
            {
                if(account.ExistMobile(model.PhoneNumber))
                {
                    var user = userManager.Users.FirstOrDefault(p => p.PhoneNumber == model.PhoneNumber).ActivateCode;
                    MessageSender sender = new MessageSender();
                    sender.SMS(model.PhoneNumber, "Password Recovery" + Environment.NewLine + "Activative Code" + user);
                    return RedirectToAction("Reset");
                }
                else
                {
                    ModelState.AddModelError("PhonNumber", "This Phone Number does not exist.");
                }
            }
                return View(model);
        }
        public IActionResult Reset()
        {
            CustomerResetModel model = new CustomerResetModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Reset(CustomerResetModel model)
        {
            if(ModelState.IsValid)
            {
                if(account.ResetPassword(model.ActivateCode, model.Password))
                {
                    return RedirectToAction("LogIn");
                }
                else
                {
                    ModelState.AddModelError("ActivateCode", "Activative Code is not Valid.");
                }
            }
            return View(model);
        }
    }
}
