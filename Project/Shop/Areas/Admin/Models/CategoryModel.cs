using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Validation;
using Microsoft.AspNetCore.Mvc;

namespace Models
{
    [Area("Admin")]
    public class CategoryModel
    {
        public int ID { get; set; }
        [Display(Name="عنوان")]
        [Required(ErrorMessage ="نام را وارد نمایید")]
        [MaxLength(25,ErrorMessage ="طول نباید بیشتر از 25 باشد")]
        [MyCustomValidation]
        public string Name { get; set; }
        [Display(Name = "شرح")]
        [Required(ErrorMessage ="شرح را وارد نمایید")]
     
        public string Description { get; set; }
    }
}
