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
{   //Authorize attribute ile bu sınıfı sadece yetkisi yani tokenı olan kişilerin girmesini söylüyorum.
    [Authorize]
    [Authorize(Roles = "ACSServiceYedekParca")]
    public class ReplacementPartController : Controller
    {
        IReplacementPartService _iReplacementPartService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ReplacementPartController> _logger;

        public ReplacementPartController(ILogger<ReplacementPartController> logger, IHttpContextAccessor iHttpContextAccessor, IReplacementPartService iReplacementPartService)
        {
            _logger = logger;
            _httpContextAccessor = iHttpContextAccessor;
            _iReplacementPartService = iReplacementPartService;
        }

        public IActionResult Index()
        {
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            GetDashboardMenuResponse response = _iReplacementPartService.GetDashboardMenu(Convert.ToInt32(userId));

            ViewBag.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
            ViewBag.UrlPath = Globals.URLPath;
            if (response != null)
                return View(response);
            else
                return View();
        }

        public async Task<IActionResult> SerialNoCheckReplacementPart()
        {
            string serialNo = HttpContext.Request.Query["serialNo"].ToString();
            SerialNoCheckReplacementPartResponse response = _iReplacementPartService.SerialNoCheckReplacementPart(serialNo);
            return Ok(response);
        }
        public async Task<IActionResult> ModelNoCheckReplacementPart()
        {
            string modelNo = HttpContext.Request.Query["modelNo"].ToString();
            ModelNoCheckReplacementPartResponse response = _iReplacementPartService.ModelNoCheckReplacementPart(modelNo);
            return Ok(response);
        }
        public async Task<IActionResult> GetReplacementPartPriceByPartNo()
        {
            string partNo = HttpContext.Request.Query["partNo"].ToString();
            GetReplacementPartPriceByPartNoResponse response = _iReplacementPartService.GetReplacementPartPriceByPartNo(partNo);
            return Ok(response);
        }


        

    }
}
