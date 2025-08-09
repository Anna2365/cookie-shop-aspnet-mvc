using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendCookieShopDemo.Models.Repositories
{
    public interface IRegisterRepository
    {
        // 新增會員
        void AddMember(EFModels.Member member);

        // 檢查帳號是否重複
        bool IsAccountExist(string account);

        // 檢查 Email 是否重複
        bool IsEmailExist(string email);
    }
}
