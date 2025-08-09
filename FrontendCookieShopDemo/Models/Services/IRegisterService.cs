using FrontendCookieShopDemo.Models.DTOs;
using FrontendCookieShopDemo.Models.Repositories;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGrease;

namespace FrontendCookieShopDemo.Models.Services
{
    public interface IRegisterService 
    {
        bool Register(RegisterDTO dto);
    }
}
