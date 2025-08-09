using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontendCookieShopDemo.Models.DTOs
{
    // 商品列表
    public class ProductDisplayDTO
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public bool Status { get; set; }

        public string ProductImage { get; set; }

        public int Stock { get; set; }
    }
}