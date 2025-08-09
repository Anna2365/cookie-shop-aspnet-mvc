using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontendCookieShopDemo.Models.ViewModels
{
    // 購物車
    public class CartViewModel
    {
        public List<CartItemVm> Items { get; set; }
        public int SubTotal { get; set; }
        public int Total { get; set; }
    }


    public class CartItemVm
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }

        /// <summary>商品單價</summary>
        public int Price { get; set; }

        /// <summary>單筆項目的小計（數量 × 單價）</summary>
        public int SubTotal => Qty * Price;
        public string ProductImage { get; set; }
    }
}