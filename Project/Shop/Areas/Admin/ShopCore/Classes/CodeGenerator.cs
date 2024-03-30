using System;
using System.Collections.Generic;
using System.Text;

namespace ShopCore.Classes
{
    public class CodeGenerator
    {
        public static string ActivateCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
        public static string FactorCode()
        {
            Random random = new Random();
           return random.Next(10000000, 99999999).ToString();
        }
        public static string FileCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
