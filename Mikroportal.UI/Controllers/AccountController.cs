using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mikroportal.MODEL;
using Mikroportal.MODEL.Entities;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.ServiceContracts;
using Mikroportal.UI.Extensions;
using RestSharp;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;

namespace Mikroportal.UI.Controllers
{

    public class AccountController : Controller
    {
        ILoginService _loginService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer _localizer;


        public AccountController(ILoginService loginService, IHttpContextAccessor iHttpContextAccessor, IStringLocalizer<AccountController> localizer)
        {
            _loginService = loginService;
            _httpContextAccessor = iHttpContextAccessor;
            _localizer = localizer;
        }


        [HttpGet]
        public IActionResult Login()
        {
            //HttpContext.Session.SetString(Globals.Session_USER_NAME, _httpContextAccessor.HttpContext.Request.Cookies["UserName"]);
            //HttpContext.Session.SetString(Globals.Session_USER_ID, _httpContextAccessor.HttpContext.Request.Cookies["UserId"]);
            //ViewBag.UserName = HttpContext.Session.GetString(Globals.Session_USER_NAME);

            string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["UserToken"];
            ViewBag.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
            if (cookieValueFromContext == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Dashboard");

            }

        }

        [HttpPost]
        public IActionResult Login(LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.UserAccount))
            {
                ViewBag.Mesaj = _localizer["KullaniciAdiMesaj"];
                return View("Login", request);
            }
            else if (string.IsNullOrEmpty(request.UserPassword))
            {
                ViewBag.Mesaj = _localizer["SifreMesaj"];
                return View("Login", request);
            }

            var response = _loginService.Login(request);

            if (response == null && response.isSuccess == false)
            {
                ViewBag.Mesaj = _localizer["ResponseMesaj"];

                return View("Login", request);
            }
            HttpContext.Response.Cookies.Append("UserToken", response.Token);
            HttpContext.Response.Cookies.Append("UserName", response.UserName);
            HttpContext.Response.Cookies.Append("UserId", response.UserId.ToString());

            var jwt = response.Token;// HttpContext.Session.GetString("UserToken");
            var handler = new JwtSecurityTokenHandler();

            var token = handler.ReadJwtToken(jwt);


            HttpContext.Session.SetString(Globals.Session_USER_NAME, response.UserName);
            HttpContext.Session.SetString(Globals.Session_USER_ID, response.UserId.ToString());


            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove(Globals.LOGGED_IN_USER_SESSION_KEY);
            HttpContext.Session.Remove(Globals.LOGGED_IN_USER_TOKEN_COOKIE_KEY);
            HttpContext.Session.Remove(Globals.Session_USER_NAME);
            HttpContext.Session.Remove(Globals.Session_USER_ID);
            HttpContext.Response.Cookies.Delete("UserToken");
            HttpContext.Response.Cookies.Delete("UserId");
            HttpContext.Response.Cookies.Delete("UserName");
            Globals.ApiClient.RemoveDefaultParameter("Authentication");

            return RedirectToAction("Login");
        }


    }
}
