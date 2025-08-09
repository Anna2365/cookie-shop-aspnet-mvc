using FrontendCookieShopDemo.Models.DTOs;
using FrontendCookieShopDemo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontendCookieShopDemo.Models.Services
{
    public interface ICartService
    {
        // 商品加入購物車
        void AddToCart(string memberAccount, CartItemVm itemDto);

        // 取得購物車資訊
        CartDTO GetCart(string memberAccount);

        // 更新購物車商品數量
        void UpdateQuantity(string memberAccount, int productId, int qty);

        // 取得會員的購物車
        int GetCartIdByMemberAccount(string memberAccount);

        // 取得購物車內容
        List<CartItemDTO> GetCartItems(int cartId);

        // 清空購物車
        void ClearCart(int cartId);

        // 移除商品
        void RemoveItem(string memberAccount, int productId);
    }
}
