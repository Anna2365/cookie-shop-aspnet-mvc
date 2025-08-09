using AutoMapper;
using FrontendCookieShopDemo.Models.DTOs;
using FrontendCookieShopDemo.Models.EFModels;
using FrontendCookieShopDemo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontendCookieShopDemo.Models.Infra
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // 會員註冊
            CreateMap<RegisterVm, RegisterDTO>();

            // 取得商品資訊
            CreateMap<Product, ProductDisplayDTO>();
            CreateMap<ProductDisplayDTO, ProductDisplayVm>();

            CreateMap<CartItemVm, CartItem>();
            // 如有需要，也可從 CartItem 反向映射成 ViewModel
            CreateMap<CartItem, CartItemVm>();

            // 1) Entity → DTO
            CreateMap<Cart, CartDTO>()
                .ForMember(dest => dest.Items,
                            opt => opt.MapFrom(src => src.CartItems));

            // CartItem → CartItemDTO
            CreateMap<CartItem, CartItemDTO>()
                .ForMember(dest => dest.ProductId,
                       opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Qty,
                       opt => opt.MapFrom(src => src.Qty))
                .ForMember(dest => dest.ProductName,
                       opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price,
                       opt => opt.MapFrom(src => src.Product.Price))
                 .ForMember(dest => dest.ProductImage,
                       opt => opt.MapFrom(src => src.Product.ProductImage));


            // 2) DTO → ViewModel
            CreateMap<CartDTO, CartViewModel>();
            CreateMap<CartItemDTO, CartItemVm>();
        }
    }
}