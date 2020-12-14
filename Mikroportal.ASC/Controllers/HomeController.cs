using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mikroportal.ACS.Models;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.ServiceContracts.ACS;
using RestSharp;

namespace Mikroportal.ACS.Controllers
{   //Authorize attribute ile bu sınıfı sadece yetkisi yani tokenı olan kişilerin girmesini söylüyorum.
    [Authorize]
    [Authorize(Roles = "ACSService")]
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<HomeController> _logger;
        IHomeService _iHomeService;
        

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor iHttpContextAccessor, IHomeService homeService )
        {
            _logger = logger;
            _httpContextAccessor = iHttpContextAccessor;
            _iHomeService = homeService;
        }

        public IActionResult Index()
        {
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            GetDashboardMenuResponse response = _iHomeService.GetDashboardMenu(Convert.ToInt32(userId));
            ViewBag.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
            ViewBag.UrlPath = Globals.URLPath;
            if (response != null)
                return View(response);
            else
                return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            //return Redirect("http://192.168.107.11/ACS");


            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}{returnUrl}";

            return Redirect(baseUrl);
        }



        public async Task<IActionResult> SavePassword()
        {
            string password = HttpContext.Request.Query["password"].ToString();
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            LoginResponse response = _iHomeService.SavePassword(password, userId);
            return Ok(response);
        }



        public IActionResult ChangePassword()
        {
            ViewBag.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
            ViewBag.UrlPath = Globals.URLPath;
            return View();
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

            return Redirect("http://192.168.107.11/Account/LogOut");
        }
    }
}
