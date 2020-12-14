using System;
using System.Collections.Generic;
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

namespace Mikroportal.ACS.Controllers.Admin
{
    //Authorize attribute ile bu sınıfı sadece yetkisi yani tokenı olan kişilerin girmesini söylüyorum.
    [Authorize]
    [Authorize(Roles = "ACSAdmin")]
    public class AddAuthorizedServiceController : Controller
    {
        IAddAuthorizedServiceService _iAddAuthorizedServiceService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AddAuthorizedServiceController> _logger;

        public AddAuthorizedServiceController(ILogger<AddAuthorizedServiceController> logger, IHttpContextAccessor iHttpContextAccessor, IAddAuthorizedServiceService iAddAuthorizedServiceService)
        {
            _logger = logger;
            _httpContextAccessor = iHttpContextAccessor;
            _iAddAuthorizedServiceService = iAddAuthorizedServiceService;
        }

        public IActionResult Index()
        {
            ViewBag.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
            ViewBag.UrlPath = Globals.URLPath;
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];

            GetDashboardMenuResponse response = _iAddAuthorizedServiceService.GetACSAdminMenu(Convert.ToInt32(userId));
            if (response != null)
                return View(response);
            else
                return View();
        }

        public async Task<IActionResult> GetInboxCount()
        {
            string serialNo = HttpContext.Request.Query["serialNo"].ToString();
            GetInboxCountResponse response = _iAddAuthorizedServiceService.GetInboxCount();
            return Ok(response);
        }


        public IActionResult AddService(AddAuthorizedService request)
        {
            request.UserId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            var response = _iAddAuthorizedServiceService.AddService(request);
            return Ok(response);
        }
    }
}
