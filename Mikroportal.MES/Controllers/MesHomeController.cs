using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.ServiceContracts.MES;
using RestSharp;

namespace Mikroportal.MES.Controllers
{
    public class MesHomeController : Controller
    {
        IMesHomeService _mesHomeService;
        private readonly ILogger<MesHomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MesHomeController(ILogger<MesHomeController> logger, IHttpContextAccessor iHttpContextAccessor, IMesHomeService mesHomeService)
        {
            _logger = logger;
            _httpContextAccessor = iHttpContextAccessor;
            _mesHomeService = mesHomeService;
        }

        public IActionResult Index()
        {
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            GetDashboardMenuResponse response = _mesHomeService.GetDashboardMenu(Convert.ToInt32(userId));
            ViewBag.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
            ViewBag.UrlPath = Globals.URLPath;
            if (response != null)
                return View(response);
            else
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

           
            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}";//$"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}";

            if (baseUrl.Contains("localhost"))
            {
                return Redirect("http://localhost:53143/Account/LogOut");
            }
            else
            {
                return Redirect(baseUrl + "/Account/LogOut");
            }
        }
    }
}