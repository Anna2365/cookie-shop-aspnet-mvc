using FrontendCookieShopDemo.Models.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontendCookieShopDemo.Models.ViewModels
{
    // 會員註冊
    public class RegisterVm
    {
        [Display(Name = "帳號")]
        [Required(ErrorMessage = InputHelper.Required)]
        [StringLength(30, ErrorMessage = "{0} 不可超過 {1} 字")]
        [RegularExpression(@"^[a-zA-Z0-9_]{3,30}$", ErrorMessage = "帳號只能是英文或數字，字數為 3 ~ 30 ")]

        public string Account { get; set; }

        [Display(Name = "密碼")]
        [Required(ErrorMessage = InputHelper.Required)]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "{0} 必需介於 {2} ~ {1} 字")]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }

        [Display(Name = "確認密碼")]
        [Required(ErrorMessage = InputHelper.Required)]
        [StringLength(40, ErrorMessage = "{0} 不可超過 {1} 字")]
        [DataType(DataType.Password)]
        [Compare("PasswordHash", ErrorMessage = "密碼與確認密碼不一致")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage = InputHelper.Required)]
        [StringLength(30, ErrorMessage = "{0} 不可超過 {1} 字")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = InputHelper.Required)]
        [StringLength(100, ErrorMessage = "{0} 不可超過 {1} ")]
        [EmailAddress(ErrorMessage = "請輸入有效的電子郵件格式")]
        public string Email { get; set; }


        [Display(Name = "手機")]
        [Required(ErrorMessage = InputHelper.Required)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "手機號碼必須為 10 位數字")]
        [RegularExpression(@"^09\d{8}$", ErrorMessage = "請輸入有效的手機格式")]

        public string Mobile { get; set; }
    }
}