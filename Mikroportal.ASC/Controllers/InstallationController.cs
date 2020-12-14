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
using Mikroportal.MODEL.ServiceContracts.ACS;

namespace Mikroportal.ACS.Controllers
{
    //Authorize attribute ile bu sınıfı sadece yetkisi yani tokenı olan kişilerin girmesini söylüyorum.
    [Authorize]
    [Authorize(Roles = "ACSService")]
    public class InstallationController : Controller
    {
        IInstallationService _iInstallationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<InstallationController> _logger;

        public InstallationController(ILogger<InstallationController> logger, IHttpContextAccessor iHttpContextAccessor, IInstallationService iInstallationService)
        {
            _logger = logger;
            _httpContextAccessor = iHttpContextAccessor;
            _iInstallationService = iInstallationService;
        }
   
        public IActionResult Index()
        {

            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            GetDashboardMenuResponse response = _iInstallationService.GetDashboardMenu(Convert.ToInt32(userId));

            ViewBag.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
            ViewBag.UrlPath = Globals.URLPath;
            if (response != null)
                return View(response);
            else
                return View();

    
        }

       
        public async Task<IActionResult> SerialNoCheck()
        {
            string serialNo = HttpContext.Request.Query["serialNo"].ToString();
            GetSkuBySerialResponse response = _iInstallationService.SerialNoCheck(serialNo);
            return Ok(response);
        }


        public IActionResult AddMachine(InstallationView request)
        {
            request.UserId= _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            var response = _iInstallationService.AddMachine(request);
            return Ok(response);
        }


    }
}
