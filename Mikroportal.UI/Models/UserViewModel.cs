using Microsoft.AspNetCore.Http;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.UI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.UI.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {

        }

        readonly HttpContext HttpContext;
        public UserViewModel(HttpContext httpContext)
        {
            this.HttpContext = httpContext;
            LoginUser = this.HttpContext.Session.Get<LoginResponse>(Globals.LOGGED_IN_USER_SESSION_KEY);
        }

        public LoginResponse LoginUser { get; set; }
    }
}
