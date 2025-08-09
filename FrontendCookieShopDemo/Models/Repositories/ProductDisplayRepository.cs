using FrontendCookieShopDemo.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontendCookieShopDemo.Models.Repositories
{
    public class ProductDisplayRepository : IProductDisplayRepository
    {
        private readonly ApplicationContext _db;
        public ProductDisplayRepository(ApplicationContext db)
        {
            _db = db;
        }

        // 取得所有商品
        public IEnumerable<Product> GetAllProducts()
        {
            return _db.Products.ToList();
        }

        // 根據商品類別取得商品
        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            // 根據 CategoryId 過濾產品
            return _db.Products.Where(p => p.CategoryId == categoryId).ToList();
        }
    }
}