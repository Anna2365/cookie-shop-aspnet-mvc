using FrontendCookieShopDemo.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendCookieShopDemo.Models.Services
{
    public interface IProductDisplayService
    {
        // 取得所有商品
        IEnumerable<ProductDisplayDTO> GetAllProducts();

        // 依類別取得商品 
        IEnumerable<ProductDisplayDTO> GetProductsByCategory(int categoryId);
    }
}
