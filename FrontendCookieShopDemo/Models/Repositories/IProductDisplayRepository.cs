using FrontendCookieShopDemo.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendCookieShopDemo.Models.Repositories
{
    public interface IProductDisplayRepository
    {
        // 取得所有商品
        IEnumerable<Product> GetAllProducts();

        // 依據類別取得商品
        IEnumerable<Product> GetProductsByCategory(int categoryId);
    }
}
