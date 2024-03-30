using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Validation
{
    public class MyCustomValidation: ValidationAttribute, IClientModelValidator
    {
        public MyCustomValidation()
        {
            
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val-morteza", "نباید تست باشد");
        }

        public override bool IsValid(object value)
        {
            if (value.ToString() == "تست")
            {
                ErrorMessage = " نام نمی تواند تست باشد ";
                return false;
            }

            return true;
        }
    }
}
