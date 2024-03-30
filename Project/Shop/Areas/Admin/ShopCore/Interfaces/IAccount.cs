using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Areas.Admin.ShopCore.Interfaces
{
    public interface IAccount
    {
        bool ExistMobile(string Mobile);
        bool ActivateUser(string Code);
        bool ResetPassword(string Code, string Password);

    }
}
