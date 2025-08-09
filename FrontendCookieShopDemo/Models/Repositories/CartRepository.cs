using FrontendCookieShopDemo.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FrontendCookieShopDemo.Models.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationContext _db;

        public CartRepository(ApplicationContext db)
        {
            _db = db;
        }

        /// <summary>
        /// 根據會員帳號取得其購物車 ID
        /// </summary>
        /// <param name="memberAccount">會員帳號</param>
        /// <returns>找到購物車則回傳 Cart.Id，否則回傳 0</returns>
        public int GetCartIdByMemberAccount(string memberAccount)
        {
            var cart = _db.Carts
                           .FirstOrDefault(c => c.MemberAccount == memberAccount);
            return cart?.Id ?? 0;
        }


        /// <summary>
        /// 取得會員購物車中所有商品
        /// </summary>
        /// <param name="cartId">購物車 ID</param>
        /// <returns>購物車 DTO</returns>
        public List<CartItem> GetItems(int cartId)
        {
            return _db.CartItems
                           .Include(c => c.Product)
                           .Where(c => c.CartId == cartId)
                           .ToList();
        }

        // 轉換成訂單之後，清空購物車
        public void ClearCart(int cartId)
        {
            var items = _db.CartItems.Where(c => c.CartId == cartId);
            _db.CartItems.RemoveRange(items);
            _db.SaveChanges();
        }

        /// <summary>
        /// 根據會員帳號取得購物車資訊
        /// </summary>
        /// <param name="memberAccount">會員帳號</param>
        /// <returns>購物車，若沒有則回傳 null</returns>
        public Cart GetCartByMemberAccount(string memberAccount)
        {
            return _db.Carts
                    .Include(c => c.CartItems)
                    .Include(c => c.CartItems.Select(ci => ci.Product))
                    .FirstOrDefault(c => c.MemberAccount == memberAccount);

        }

        /// <summary>
        /// 根據商品 ID 取得商品資訊
        /// </summary>
        /// <param name="productId">商品 ID</param>
        /// <returns>商品，若沒有則回傳 null</returns>
        public Product GetProductById(int productId)
        {
            return _db.Products.FirstOrDefault(p => p.Id == productId);
        }

        /// <summary>
        /// 新增一筆購物車
        /// </summary>
        /// <param name="cart">要新增的購物車</param>
        public void AddCart(Cart cart)
        {
            _db.Carts.Add(cart);
        }

        /// <summary>
        /// 新增一筆商品資訊
        /// </summary>
        /// <param name="cartItem">購物車資訊</param>
        public void AddCartItem(CartItem cartItem)
        {
            _db.CartItems.Add(cartItem);
        }

        /// <summary>
        /// 更新購物車
        /// </summary>
        /// <param name="cartItem">更新數量後的購物車</param>
        public void UpdateCartItem(CartItem cartItem)
        {
            _db.Entry(cartItem).State = EntityState.Modified;
        }

        /// <summary>
        /// 移除會員購物車中的特定商品
        /// </summary>
        /// <param name="cartItem">要移除的商品</param>
        public void RemoveCartItem(CartItem cartItem)
        {
            _db.CartItems.Remove(cartItem);
        }



        /// <summary>
        /// 儲存更新的資料
        /// </summary>
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}