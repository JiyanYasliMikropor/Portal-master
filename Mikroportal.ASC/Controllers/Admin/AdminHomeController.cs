using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using Mikroportal.MODEL.RequestResponseClasses.ACS.Admin;
using Mikroportal.MODEL.ServiceContracts.ACS.Admin;
using RestSharp;

namespace Mikroportal.ACS.Controllers.Admin
{
    //Authorize attribute ile bu sınıfı sadece yetkisi yani tokenı olan kişilerin girmesini söylüyorum.
    //[Authorize]
    //[Authorize(Roles = "ACSAdmin")]
    public class AdminHomeController : Controller
    {
        IAdminHomeService _iAdminHomeService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AdminHomeController> _logger;

        public AdminHomeController(ILogger<AdminHomeController> logger, IHttpContextAccessor iHttpContextAccessor, IAdminHomeService iAdminHomeService)
        {
            _logger = logger;
            _httpContextAccessor = iHttpContextAccessor;
            _iAdminHomeService = iAdminHomeService;
        }
        public IActionResult Index()
        {
            ViewBag.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
            ViewBag.UrlPath = Globals.URLPath;
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];

            GetDashboardMenuResponse response = _iAdminHomeService.GetACSAdminMenu(Convert.ToInt32(userId));
            if (response != null)
                return View(response);
            else
                return View();

        }


        public async Task<IActionResult> GetInboxCount()
        {
            string serialNo = HttpContext.Request.Query["serialNo"].ToString();
            GetInboxCountResponse response = _iAdminHomeService.GetInboxCount();
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> GetInboxList(SearchRequest searchRequest)
        {

            GetInboxListResponse response = _iAdminHomeService.GetInboxList(searchRequest);
            return Ok(response);
        }


        public async Task<IActionResult> InboxShowById()
        {
            string machineryServiceId = HttpContext.Request.Query["machineryServiceId"].ToString();
            InboxShowByIdResponse response = _iAdminHomeService.InboxShowById(machineryServiceId);
            return Ok(response);
        }


        public async Task<IActionResult> IsApproved()
        {
            string machineryServiceId = HttpContext.Request.Query["MachineryServiceId"].ToString();
            TblSshMachineryServicesResponse response = _iAdminHomeService.IsApproved(machineryServiceId);
            return Ok(response);
        }


        public async Task<IActionResult> IsRejected(RejectedRequest rejectedRequest)
        {
            TblSshMachineryServicesResponse response = _iAdminHomeService.IsRejected(rejectedRequest);
            return Ok(response);
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

            //return Redirect("http://192.168.107.11/Account/LogOut");
            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}";

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
