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
    //[Authorize]
    //[Authorize(Roles = "ACSServiceTekinkDokuman")]
    public class TechnicalDocumentController : Controller
    {
        ITechnicalDocumentService _iTechnicalDocumentService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<InstallationController> _logger;

        public TechnicalDocumentController(ILogger<InstallationController> logger, IHttpContextAccessor iHttpContextAccessor, ITechnicalDocumentService iTechnicalDocumentService)
        {
            _logger = logger;
            _httpContextAccessor = iHttpContextAccessor;
            _iTechnicalDocumentService = iTechnicalDocumentService;
        }

        public IActionResult Index()
        {
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            GetDashboardMenuResponse response = _iTechnicalDocumentService.GetDashboardMenu(Convert.ToInt32(userId));

            ViewBag.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
            ViewBag.UrlPath = Globals.URLPath;
            if (response != null)
                return View(response);
            else
                return View();
        }
        public async Task<IActionResult> SerialNoCheckTechnicalDocument()
        {
            string serialNo = HttpContext.Request.Query["serialNo"].ToString();
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            TechnicalDocumentResponse response = _iTechnicalDocumentService.SerialNoCheckTechnicalDocument(serialNo, userId);
            return Ok(response);
        }


    }
}
