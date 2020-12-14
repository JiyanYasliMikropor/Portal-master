using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using Mikroportal.MODEL.ServiceContracts;
using Mikroportal.MODEL.ServiceContracts.ACS;

namespace Mikroportal.ACS.Controllers
{
    //Authorize attribute ile bu sınıfı sadece yetkisi yani tokenı olan kişilerin girmesini söylüyorum.
    [Authorize]
    [Authorize(Roles = "ACSService")]
    public class CallCenterController : Controller
    {
        ICallCenterService _iCallCenterService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CallCenterController> _logger;

        public CallCenterController(ILogger<CallCenterController> logger, IHttpContextAccessor iHttpContextAccessor, ICallCenterService iCallCenterService)
        {
            _logger = logger;
            _httpContextAccessor = iHttpContextAccessor;
            _iCallCenterService = iCallCenterService;
        }
        public IActionResult Index()
        {
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            GetDashboardMenuResponse response = _iCallCenterService.GetDashboardMenu(Convert.ToInt32(userId));

            ViewBag.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
            ViewBag.UrlPath = Globals.URLPath;
            if (response != null)
                return View(response);
            else
                return View();

        }

        public IActionResult AddCallCenter(CallCenterView request)
        {
            request.UserId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            var response = _iCallCenterService.AddCallCenter(request);
            return Ok(response);
        }


    }
}
