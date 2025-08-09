using AutoMapper;
using FrontendCookieShopDemo.Models.Services;
using FrontendCookieShopDemo.Models.ViewModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontendCookieShopDemo.Controllers
{
    [Authorize]

    public class CartsController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public CartsController(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        // 購物車頁面
        [HttpGet]
        public ActionResult CartInfo()
        {
            var dto = _cartService.GetCart(User.Identity.Name);
            logger.Info("載入購物車，會員：{0}", User.Identity.Name);
            var vm = _mapper.Map<CartViewModel>(dto);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(int productId, int qty)
        {
            var memberAccount = User.Identity.Name;

            // 驗證商品數量
            if (qty <= 0)
            {
                TempData["CartError"] = "數量必須大於 0";
                return RedirectToAction("ProductDetails", "Products", new { id = productId });
            }

            try
            {
                _cartService.AddToCart(memberAccount, new CartItemVm
                {
                    ProductId = productId,
                    Qty = qty
                });
                logger.Info("會員 {Account} 加入商品 {ProductId} x{Qty}", memberAccount, productId, qty);

                TempData["CartSuccess"] = "商品已成功加入購物車";
            }
            catch (Exception ex)
            {
                logger.Error(ex, "加入購物車失敗 (ProductId={ProductId},Qty={Qty})", productId, qty);
                TempData["CartError"] = "加入購物車失敗，請稍後再試";
            }


            return RedirectToAction("ProductDetails", "Products", new { id = productId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateQuantity(int productId, int qty)
        {
            string member = User.Identity.Name;


            try
            {
                _cartService.UpdateQuantity(member, productId, qty);
                TempData["CartSuccess"] = "購物車已更新";
            }
            catch (Exception ex)
            {
                logger.Error(ex, "更新購物車數量失敗 (ProductId={ProductId},Qty={Qty})", productId, qty);
                TempData["CartError"] = "更新數量失敗，請稍後再試";
            }

            return RedirectToAction("CartInfo");
        }

        // 移除商品
        [HttpPost]
        public JsonResult RemoveItem(int productId)
        {
            var member = User.Identity.Name;
            try
            {
                _cartService.RemoveItem(member, productId);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}