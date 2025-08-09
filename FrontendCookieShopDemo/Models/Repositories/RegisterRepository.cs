using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FrontendCookieShopDemo.Models;
using FrontendCookieShopDemo.Models.EFModels;


namespace FrontendCookieShopDemo.Models.Repositories
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly ApplicationContext _db;
        public RegisterRepository(ApplicationContext db)
        {
            _db = db;
        }

        // 新增會員
        public void AddMember(EFModels.Member member)
        {
            _db.Members.Add(member);
            _db.SaveChanges();
        }


        // 檢查帳號是否重複
        public bool IsAccountExist(string account)
        {
            return _db.Members.Any(m => m.Account == account);
        }

        // 檢查 Email 是否重複
        public bool IsEmailExist(string email)
        {
            return _db.Members.Any(m => m.Email == email);
        }
    }
}