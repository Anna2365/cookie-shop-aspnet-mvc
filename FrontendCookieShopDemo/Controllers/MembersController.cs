using AutoMapper;
using FrontendCookieShopDemo.Models.DTOs;
using FrontendCookieShopDemo.Models.Exceptions;
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
    public class MembersController : Controller
    {
        private readonly IRegisterService _registerservice;
        private readonly IMapper _mapper;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();


        public MembersController
        (
             IRegisterService registerservice,
             IMapper mapper
        )

        {
            _registerservice = registerservice;
            _mapper = mapper;

        }


        // 註冊會員表單
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // 註冊會員功能 
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVm model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);

            var dto = _mapper.Map<RegisterDTO>(model);

            logger.Info("Controller: 接收到註冊請求：{0}", dto.Account);

            try
            {
                // 呼叫 Service 建立會員
                _registerservice.Register(dto);

                logger.Info("Controller: 註冊成功：{0}", dto.Account);

                // 清空表單
                ModelState.Clear();

                // 如果註冊成功，設定 TempData 旗標
                TempData["RegisterSuccess"] = true;

                // 導向 Home/Index
                return Redirect(Url.Action("Index", "Home"));

            }
            catch (UserAlreadyExistsException)
            {
                ModelState.AddModelError("", "帳號或 Email 已被使用");
                return View(model);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Controller: 註冊失敗：{0}", dto.Account);
                ModelState.AddModelError("", "發生錯誤，請稍後再試");
                return View(model);
            }

        }
    }
}