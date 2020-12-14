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
using Mikroportal.MODEL.ServiceContracts.ACS.Admin;

namespace Mikroportal.ACS.Controllers.Admin
{
    //Authorize attribute ile bu sınıfı sadece yetkisi yani tokenı olan kişilerin girmesini söylüyorum.
    [Authorize]
    [Authorize(Roles = "ACSAdmin")]
    public class AddGuaranteeController : Controller
    {

        IAddGuaranteeService _iAddGuaranteeService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AddGuaranteeController> _logger;

        public AddGuaranteeController(ILogger<AddGuaranteeController> logger, IHttpContextAccessor iHttpContextAccessor, IAddGuaranteeService iAddGuaranteeService)
        {
            _logger = logger;
            _httpContextAccessor = iHttpContextAccessor;
            _iAddGuaranteeService = iAddGuaranteeService;
        }
        public IActionResult Index()
        {
            ViewBag.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
            ViewBag.UrlPath = Globals.URLPath;
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];

            GetDashboardMenuResponse response = _iAddGuaranteeService.GetACSAdminMenu(Convert.ToInt32(userId));
            if (response != null)
                return View(response);
            else
                return View();
        }


        public IActionResult BanGuarantee()
        {
            ViewBag.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
            ViewBag.UrlPath = Globals.URLPath;
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];

            GetDashboardMenuResponse response = _iAddGuaranteeService.GetACSAdminMenu(Convert.ToInt32(userId));
            if (response != null)
                return View(response);
            else
                return View();
        }


        public async Task<IActionResult> GetInboxCount()
        {
            string serialNo = HttpContext.Request.Query["serialNo"].ToString();
            GetInboxCountResponse response = _iAddGuaranteeService.GetInboxCount();
            return Ok(response);
        }

        public async Task<IActionResult> SerialNoCheckGuarantee()
        {
            string serialNo = HttpContext.Request.Query["serialNo"].ToString();
            GetSkuBySerialResponse response = _iAddGuaranteeService.SerialNoCheckGuarantee(serialNo);
            return Ok(response);
        }

        public IActionResult AddGuarantee(GuaranteeView request)
        {
            request.UserId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            var response = _iAddGuaranteeService.AddGuarantee(request);
            return RedirectToAction("Index", "AdminReport");
        }

        public IActionResult AddBanGuarantee(GuaranteeView request)
        {
            request.UserId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            var response = _iAddGuaranteeService.AddBanGuarantee(request);
            return RedirectToAction("Index", "AdminReport");
        }

    }
}
