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
    [Authorize(Roles = "ACSAdminYedekParca")]
    public class AdminReplacementController : Controller
    {

        IAdminReplacementService _iAdminReplacementService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AdminReplacementController> _logger;

        public AdminReplacementController(ILogger<AdminReplacementController> logger, IHttpContextAccessor iHttpContextAccessor, IAdminReplacementService iAdminReplacementService)
        {
            _logger = logger;
            _httpContextAccessor = iHttpContextAccessor;
            _iAdminReplacementService = iAdminReplacementService;
        }
        public IActionResult Index()
        {
            ViewBag.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
            ViewBag.UrlPath = Globals.URLPath;
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];

            GetDashboardMenuResponse response = _iAdminReplacementService.GetACSAdminMenu(Convert.ToInt32(userId));
            if (response != null)
                return View(response);
            else
                return View();
        }

        public async Task<IActionResult> GetReplacementList(SearchPartNo searchPartNo)
        {
            searchPartNo.partNo = HttpContext.Request.Query["search"].ToString();
            GetReplacementListResponse response = _iAdminReplacementService.GetReplacementList(searchPartNo);
            return Ok(response);
        }
        public async Task<IActionResult> GetInboxCount()
        {
            string serialNo = HttpContext.Request.Query["serialNo"].ToString();
            GetInboxCountResponse response = _iAdminReplacementService.GetInboxCount();
            return Ok(response);
        }

        public async Task<IActionResult> SaveReplacementPrice(UpdateReplacementPrice UpdateReplacementPrice )
        {
            UpdateReplacementPrice.PartNo = Convert.ToInt32(HttpContext.Request.Query["PartNo"].ToString());
            UpdateReplacementPrice.Qty = Convert.ToInt32(HttpContext.Request.Query["Qty"].ToString());
            UpdateReplacementPrice.Price = Convert.ToDecimal(HttpContext.Request.Query["Price"].ToString());
            UpdateReplacementPrice.DryerModel = HttpContext.Request.Query["DryerModel"].ToString();
            UpdateReplacementPrice.Voltage = HttpContext.Request.Query["Voltage"].ToString();
            UpdateReplacementPrice.EdDrawingItemNo = Convert.ToInt32(HttpContext.Request.Query["EdDrawingItemNo"].ToString());
            UpdateReplacementPrice.Currency = HttpContext.Request.Query["Currency"].ToString();

            GetReplacementListResponse response = _iAdminReplacementService.SaveReplacementPrice(UpdateReplacementPrice);
            return Ok(response);
        }

    }
}
