using AutoMapper;
using FrontendCookieShopDemo.Models;
using FrontendCookieShopDemo.Models.EFModels;
using FrontendCookieShopDemo.Models.Infra;
using FrontendCookieShopDemo.Models.Repositories;
using FrontendCookieShopDemo.Models.Services;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;


namespace FrontendCookieShopDemo.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();


            // 註冊 EF 的 DbContext
            container.RegisterType<ApplicationContext>(new PerRequestLifetimeManager());


            // 註冊 Dapper 用的 SqlConnection
            container.RegisterFactory<IDbConnection>(c =>
            {
                var connStr = ConfigurationManager.ConnectionStrings["AppDbContext3"].ConnectionString;
                return new SqlConnection(connStr);
            }, new PerRequestLifetimeManager());




            // 註冊依賴注入項目
            container.RegisterType<IRegisterRepository, RegisterRepository>();
            container.RegisterType<IRegisterService, RegisterService>();
            container.RegisterType<IProductDisplayRepository, ProductDisplayRepository>();
            container.RegisterType<IProductDisplayService, ProductDisplayService>();
            container.RegisterType<ICartRepository, CartRepository>();
            container.RegisterType<ICartService, CartService>();
          


            // AutoMapper 設定
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });


            // 由配置產生 IMapper 實例
            IMapper mapper = mapperConfiguration.CreateMapper();

            // 將產生的 IMapper 實例註冊到 Unity 容器中
            container.RegisterInstance<IMapper>(mapper);
        }


        public static void RegisterComponents()
        {
            // 註冊依賴注入
            DependencyResolver.SetResolver(new UnityDependencyResolver(Container));
        }



    }
}