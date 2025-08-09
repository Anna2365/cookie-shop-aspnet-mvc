using AutoMapper;
using FrontendCookieShopDemo.Models.DTOs;
using FrontendCookieShopDemo.Models.EFModels;
using FrontendCookieShopDemo.Models.Repositories;
using FrontendCookieShopDemo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontendCookieShopDemo.Models.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartService(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// 根據會員帳號取得其購物車 ID
        /// </summary>
        /// <param name="memberAccount">會員帳號</param>
        /// <returns>找到購物車則回傳 Cart.Id，否則回傳 0</returns>
        public int GetCartIdByMemberAccount(string memberAccount)
        {
            return _cartRepository.GetCartIdByMemberAccount(memberAccount);
        }


        /// <summary>
        /// 取得會員購物車中所有商品
        /// </summary>
        /// <param name="cartId">購物車 ID</param>
        /// <returns>購物車 DTO </returns>
        public List<CartItemDTO> GetCartItems(int cartId)
        {
            var items = _cartRepository.GetItems(cartId);
            return _mapper.Map<List<CartItemDTO>>(items);
        }

        // 轉換成訂單之後，清空購物車
        public void ClearCart(int cartId)
        {
            _cartRepository.ClearCart(cartId);
        }

        /// <summary>
        /// 將商品加入會員的購物車；若購物車不存在，則先建立再加入商品
        /// </summary>
        /// <param name="memberAccount">會員帳號</param>
        /// <param name="itemVm">包含商品 Id 與數量的 ViewModel</param>
        public void AddToCart(string memberAccount, CartItemVm itemVm)
        {
            // 取得會員購物車，若不存在則建立新購物車
            var cart = _cartRepository.GetCartByMemberAccount(memberAccount);

            if (cart == null)
            {
                cart = new Cart { MemberAccount = memberAccount };
                _cartRepository.AddCart(cart);
                _cartRepository.Save();

            }

            // 判斷購物車內是否有同一個商品
            var existing = cart.CartItems
                           .FirstOrDefault(ci => ci.ProductId == itemVm.ProductId);

            if (existing != null)
            {
                // 若有同一個商品就增加數量
                existing.Qty += itemVm.Qty;
                _cartRepository.UpdateCartItem(existing);
            }
            else
            {
                // 若沒有同一個商品就新增到購物車
                var newItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = itemVm.ProductId,
                    Qty = itemVm.Qty
                };
                _cartRepository.AddCartItem(newItem);
            }

            _cartRepository.Save();
        }

        /// <summary>
        /// 根據會員帳號取得完整購物車資訊
        /// </summary>
        /// <param name="memberAccount">會員帳號</param>
        /// <returns>購物車 DTO；如果沒有就回傳空清單</returns>
        public CartDTO GetCart(string memberAccount)
        {
            var cart = _cartRepository.GetCartByMemberAccount(memberAccount);
            if (cart == null)
                return new CartDTO
                {
                    Items = new List<CartItemDTO>()
                };


            var dto = _mapper.Map<CartDTO>(cart);

            // 分別計算小計與總計
            dto.SubTotal = dto.Items.Sum(i => i.SubTotal);
            dto.Total = dto.SubTotal;

            return dto;

        }


        /// <summary>
        /// 更新購物車中商品的數量；若數量小於等於 0，則移除此商品。
        /// </summary>
        /// <param name="memberAccount">會員帳號</param>
        /// <param name="productId">商品 ID</param>
        /// <param name="qty">更新後的數量</param>
        public void UpdateQuantity(string memberAccount, int productId, int qty)
        {
            var cart = _cartRepository.GetCartByMemberAccount(memberAccount);
            if (cart == null) throw new Exception("購物車不存在");

            var item = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (item == null) throw new Exception("項目不存在");

            if (qty <= 0)
            {
                // 數量 <= 0 時移除該項商品
                _cartRepository.RemoveCartItem(item);
            }
            else
            {
                // 否則更新數量
                item.Qty = qty;
                _cartRepository.UpdateCartItem(item);
            }

            _cartRepository.Save();

        }

        /// <summary>
        /// 移除購物車中指定商品
        /// </summary>
        /// <param name="memberAccount">會員帳號</param>
        /// <param name="productId">商品 ID</param>
        public void RemoveItem(string memberAccount, int productId)
        {
            var cart = _cartRepository.GetCartByMemberAccount(memberAccount);
            if (cart == null) throw new Exception("購物車不存在");

            var item = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (item == null) throw new Exception("商品不存在於購物車");

            _cartRepository.RemoveCartItem(item);
            _cartRepository.Save();
        }

    }
}