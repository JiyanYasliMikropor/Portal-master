using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using Mikroportal.MODEL.RequestResponseClasses.ACS.Admin;
using Mikroportal.MODEL.ServiceContracts.ACS.Admin;

namespace Mikroportal.ACS.Controllers.Admin
{
    //Authorize attribute ile bu sınıfı sadece yetkisi yani tokenı olan kişilerin girmesini söylüyorum.
    [Authorize]
    [Authorize(Roles = "ACSAdmin")]
    public class AdminAccountController : Controller
    {
        IAdminAccountService _iAdminAccountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AdminAccountController> _logger;

        public AdminAccountController(ILogger<AdminAccountController> logger, IHttpContextAccessor iHttpContextAccessor, IAdminAccountService iAdminAccountService)
        {
            _logger = logger;
            _httpContextAccessor = iHttpContextAccessor;
            _iAdminAccountService = iAdminAccountService;
        }
        public IActionResult Index()
        {
            ViewBag.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
            ViewBag.UrlPath = Globals.URLPath;
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];

            GetDashboardMenuResponse response = _iAdminAccountService.GetACSAdminMenu(Convert.ToInt32(userId));

            if (response != null)
                return View(response);
            else
                return View();
        }




        public async Task<IActionResult> GetInboxCount()
        {
            string serialNo = HttpContext.Request.Query["serialNo"].ToString();
            GetInboxCountResponse response = _iAdminAccountService.GetInboxCount();
            return Ok(response);
        }
        public async Task<IActionResult> GetPersonelList()
        {
            var response = _iAdminAccountService.GetPersonelList();
            return Ok(response);
        }



        public IActionResult UserChangeRole(UserRoleChangeView request)
        {
            request.UserId = Convert.ToInt32(HttpContext.Request.Query["userId"].ToString());
            request.Type = HttpContext.Request.Query["type"].ToString();
            request.Durum = Convert.ToInt32(HttpContext.Request.Query["durum"].ToString());

            var response = _iAdminAccountService.UserChangeRole(request);
            return Ok(response);
        }


    }
}
