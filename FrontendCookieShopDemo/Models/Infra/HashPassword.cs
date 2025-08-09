using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace FrontendCookieShopDemo.Models.Infra
{
    public class HashPassword
    {
        /// <summary>
		/// 使用 BCrypt 密碼加密
		/// </summary>
		/// <param name="plainText">輸入的密碼</param>
		/// <param name="workFactor">
		/// 成本參數，數值越高加密所需計算越多
		/// </param>
		/// <returns>加密後包含 salt 的雜湊密碼</returns>
        public static string HashWithBCrypt(string plainText, int workFactor = 12)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainText, workFactor);
        }

        /// <summary>
        /// 使用 BCrypt 驗證輸入的密碼與雜湊後的密碼是否一致
        /// </summary>
        /// <param name="plainText">輸入的密碼</param>
        /// <param name="hashedPassword">資料庫中的雜湊密碼</param>
        /// <returns>驗證成功回傳 true，否則回傳 false</returns>
        public static bool VerifyWithBCrypt(string plainText, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainText, hashedPassword);
        }
    }
}