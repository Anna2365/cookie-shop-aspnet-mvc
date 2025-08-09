using FrontendCookieShopDemo.Models.DTOs;
using FrontendCookieShopDemo.Models.Exceptions;
using FrontendCookieShopDemo.Models.Infra;
using FrontendCookieShopDemo.Models.Repositories;
using Microsoft.Ajax.Utilities;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontendCookieShopDemo.Models.Services
{
    public class RegisterService :IRegisterService
    {
        private readonly IRegisterRepository _repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public RegisterService(IRegisterRepository repository)
        {
            _repository = repository;

        }

        // 註冊會員
        public bool Register(RegisterDTO dto)
        {
            logger.Info("嘗試註冊帳號：{0}", dto.Account);

            // 檢查帳號是否已存在
            if (_repository.IsAccountExist(dto.Account))
            {
                logger.Warn("註冊失敗：帳號已存在：{0}", dto.Account);
                throw new UserAlreadyExistsException("帳號已存在");
            }

            // 檢查 Email 是否已存在
            if (_repository.IsEmailExist(dto.Email))
            {
                logger.Warn("註冊失敗：Email 已存在：{0}", dto.Email);

                throw new UserAlreadyExistsException("Email 已存在");
            }


            // 密碼加密 (Bcrypt)
            var hashedPassword = HashPassword.HashWithBCrypt(dto.PasswordHash);

            // 儲存新會員資料至資料庫
            var member = new EFModels.Member
            {
                Account = dto.Account,
                Email = dto.Email,
                PasswordHash = hashedPassword,
                Name = dto.Name,
                Mobile = dto.Mobile,
                CreatedAt = DateTime.Now,
                Locked = false
            };

            try
            {
                _repository.AddMember(member);
                logger.Info("註冊成功：{0}", dto.Account);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "註冊過程發生例外：{0}", dto.Account);
                throw;
            }

        }
    }
}