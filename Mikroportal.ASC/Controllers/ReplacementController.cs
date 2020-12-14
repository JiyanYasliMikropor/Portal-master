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
    public class ReplacementController : Controller
    {
        IReplacementService _iReplacementService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<InstallationController> _logger;

        public ReplacementController(ILogger<InstallationController> logger, IHttpContextAccessor iHttpContextAccessor, IReplacementService iReplacementService)
        {
            _logger = logger;
            _httpContextAccessor = iHttpContextAccessor;
            _iReplacementService = iReplacementService;
        }


        public IActionResult Index()
        {
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            GetDashboardMenuResponse response = _iReplacementService.GetDashboardMenu(Convert.ToInt32(userId));

            ViewBag.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
            ViewBag.UrlPath = Globals.URLPath;
            if (response != null)
                return View(response);
            else
                return View();
        }

        public async Task<IActionResult> SerialNoCheckAndHistory()
        {
            string serialNo = HttpContext.Request.Query["serialNo"].ToString();
            SerialNoCheckAndHistoryResponse response = _iReplacementService.SerialNoCheckAndHistory(serialNo);
            return Ok(response);
        }

        public IActionResult AddReplacement(ReplacementView request)
        {
            request.UserId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            var response = _iReplacementService.AddReplacement(request);
            return Ok(response);
        }

    }
}
