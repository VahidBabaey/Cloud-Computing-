using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Areas.Admin.Models
{
    public class CustomerRegisterModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Name")]
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Display(Name = "Fullname")]
        [Required(ErrorMessage = "Enter Fullname")]
        public string LastName { get; set; }

        [Display(Name = "Username")]
        [Remote("CheckUserNameAsync", "Account", "Admin", ErrorMessage = "This Username has been taken", HttpMethod = "Post")]
        [Required(ErrorMessage = "Enter Username")]
        
        public string UserName { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Enter Phone Number")]
        [MaxLength(11,ErrorMessage = "Phone Number must contain 10 digits")]
        [MinLength(11, ErrorMessage = "Phone Number must contain 10 digits")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [Display(Name = "Pawssword")]
        public string Password { get; set; }

        [Display(Name = "Pawssword Confirmation")]
        [Required(ErrorMessage = "Enter Password again")]
        [Compare("Password", ErrorMessage = "Passwords are not the same.")]
        public string ConfirmPassword { get; set; }

        [EmailAddress(ErrorMessage = "Email Format is not correct")]
        [Required(ErrorMessage = "Enter Email again")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class CustomerLoginModel
    {

        public string returnUrl { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Enter Username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
    public class CustomerActivateModel
    {
        [Display(Name = "Confirmation Code")]
        [Required(ErrorMessage = "Enter Confirmation Code")]
        [MaxLength(6, ErrorMessage = "Confirmation Code must contain 6 digits")]
        [MinLength(6, ErrorMessage = "Confirmation Code must contain 6 digits")]
        public string ActivateCode { get; set; }
    }
    public class CustomerForgetModel
    {
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Enter Phone Number")]
        [MaxLength(11, ErrorMessage = "Phone Number must contain 10 digits")]
        [MinLength(11, ErrorMessage = "Phone Number must contain 10 digits")]
        public string PhoneNumber { get; set; }
    }
    public class CustomerResetModel
    {
        [Display(Name = "Confirmation Code")]
        [Required(ErrorMessage = "Enter Confirmation Code")]
        [MaxLength(6, ErrorMessage = "Confirmation Code must contain 6 digits")]
        [MinLength(6, ErrorMessage = "Confirmation Code must contain 6 digits")]
        public string ActivateCode { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Pawssword Confirmation")]
        [Required(ErrorMessage = "Enter Password again")]
        [Compare("Password", ErrorMessage = "Passwords are not the same.")]
        public string ConfirmPassword { get; set; }
    }
    public class SellerRegisterModel
    {
        public string returnUrl { get; set; }
        public int Id { get; set; }

        [Display(Name = "Username")]
        [Remote("CheckUserNameAsync", "Account", "Admin", ErrorMessage = "This Username has been taken", HttpMethod = "Post")]
        [Required(ErrorMessage = "Enter Username")]

        public string UserName { get; set; }

        //[Display(Name = "نام شرکت")]
        //[Remote("CheckUserNameAsync", "Account", "Admin", ErrorMessage = "این نام شرکت قبلا استفاده شده است", HttpMethod = "Post")]
        //[Required(ErrorMessage = "نام شرکت  را وارد نمایید")]

        //public string CompanyName { get; set; }

        [EmailAddress(ErrorMessage = "Email Format is not correct")]
        [Required(ErrorMessage = "Enter Email again")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Address")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Pawssword Confirmation")]
        [Required(ErrorMessage = "Enter Password again")]
        [Compare("Password", ErrorMessage = "Passwords are not the same.")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "Tel")]
        [Required(ErrorMessage = "ter Tel")]
        [MaxLength(11, ErrorMessage = "Tel must contain 11 digits")]
        [MinLength(8, ErrorMessage = "Tel must contain 11 digits")]
        public string Tel { get; set; }
        public int SellerID { get; set; }
    }
    public class SellerLoginModel
    {
        public string returnUrl { get; set; }

        //[Display(Name = "ایمیل")]
        //[Required(ErrorMessage = "ایمیل را وارد نمایید")]
        //public string Email { get; set; }
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Enter Username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
