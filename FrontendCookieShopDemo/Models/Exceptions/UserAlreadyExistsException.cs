using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontendCookieShopDemo.Models.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string message) : base(message)
        {
        }
    }
}