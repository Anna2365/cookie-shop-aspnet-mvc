using AutoMapper;
using FrontendCookieShopDemo.Models.DTOs;
using FrontendCookieShopDemo.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontendCookieShopDemo.Models.Services
{
    public class ProductDisplayService : IProductDisplayService
    {
        private readonly IProductDisplayRepository _repository;
        private readonly IMapper _mapper;

        public ProductDisplayService(IProductDisplayRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // 取得所有商品 
        public IEnumerable<ProductDisplayDTO> GetAllProducts()
        {
            var products = _repository.GetAllProducts();
            var dtoList = _mapper.Map<IEnumerable<ProductDisplayDTO>>(products);

            // 過濾商品狀態 = true 且 庫存 > 0 
            return dtoList
                   .Where(d => d.Status && d.Stock > 0);
        }

        // 依類別取得商品 
        public IEnumerable<ProductDisplayDTO> GetProductsByCategory(int categoryId)
        {
            // 取得符合類別的商品
            var products = _repository.GetProductsByCategory(categoryId);

            var dtos = _mapper.Map<IEnumerable<ProductDisplayDTO>>(products);

            // 過濾商品狀態 = true 且 庫存 > 0
            return dtos
                    .Where(d => d.Status && d.Stock > 0);
        }
    }
}