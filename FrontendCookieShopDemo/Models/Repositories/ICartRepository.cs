using FrontendCookieShopDemo.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendCookieShopDemo.Models.Repositories
{
    // 購物車功能
    public interface ICartRepository
    {
        Cart GetCartByMemberAccount(string memberAccount);

        Product GetProductById(int productId);
        void AddCart(Cart cart);
        void AddCartItem(CartItem cartItem);
        void UpdateCartItem(CartItem cartItem);
        void RemoveCartItem(CartItem cartItem);
        void Save();

        List<CartItem> GetItems(int cartId);
        void ClearCart(int cartId);

        // 根據會員帳號取得購物車 Id
        int GetCartIdByMemberAccount(string memberAccount);
    }
}
