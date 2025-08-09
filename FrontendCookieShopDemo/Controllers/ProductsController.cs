using AutoMapper;
using FrontendCookieShopDemo.Models.DTOs;
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
    public class ProductsController : Controller
    {
        private readonly IProductDisplayService _productDisplayService;
        private readonly IMapper _mapper;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ProductsController(IProductDisplayService productDisplayService, IMapper mapper)
        {
            _productDisplayService = productDisplayService;
            _mapper = mapper;
        }


        /// <summary>
        /// 顯示商品列表；若沒有選擇 categoryId 則顯示全部商品。
        /// </summary>
        /// <param name="categoryId">商品類別，如果是 null 就顯示全部商品</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ProductList(int? categoryId)
        {
            try
            {
                IEnumerable<ProductDisplayDTO> dtoList;

                if (categoryId.HasValue)
                {
                    // 顯示所選類別的商品
                    dtoList = _productDisplayService.GetProductsByCategory(categoryId.Value);
                    ViewBag.SelectedCategory = categoryId.Value;
                }
                else
                {
                    // 顯示全部商品
                    dtoList = _productDisplayService.GetAllProducts();
                    ViewBag.SelectedCategory = 0;
                }
                var viewModelList = _mapper.Map<List<ProductDisplayVm>>(dtoList);
                return View(viewModelList);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"查詢商品列表失敗 (CategoryId={categoryId})");
                ViewBag.ErrorMessage = "載入商品失敗，請稍後再試。";
                return View("Error");
            }



        }
    }
}