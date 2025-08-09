using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontendCookieShopDemo.Models.DTOs
{
    // 會員註冊
    public class RegisterDTO
    {
        public string Account { get; set; }

        public string PasswordHash { get; set; }

        public string ConfirmPassword { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Mobile { get; set; }
    }
}