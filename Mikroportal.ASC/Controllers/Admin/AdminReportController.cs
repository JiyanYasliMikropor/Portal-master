using System;
using System.Collections.Generic;
using System.IO;
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
    public class AdminReportController : Controller
    {
        IAdminReportService _iAdminReportService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AdminReportController> _logger;

        public AdminReportController(ILogger<AdminReportController> logger, IHttpContextAccessor iHttpContextAccessor, IAdminReportService iAdminReportService)
        {
            _logger = logger;
            _httpContextAccessor = iHttpContextAccessor;
            _iAdminReportService = iAdminReportService;
        }
        public IActionResult Index()
        {
            ViewBag.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
            ViewBag.UrlPath = Globals.URLPath;
            string[] paths = { @"C:\", "Files", "ACS", "Services" };
            string UrlPathStatic = Path.Combine(paths);
            ViewBag.UrlPathStatic = UrlPathStatic;
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];

            GetDashboardMenuResponse response = _iAdminReportService.GetACSAdminMenu(Convert.ToInt32(userId));
            if (response != null)
                return View(response);
            else
                return View();

        }

        public async Task<IActionResult> InboxShowById()
        {
            string machineryServiceId = HttpContext.Request.Query["machineryServiceId"].ToString();
            InboxShowByIdResponse response = _iAdminReportService.InboxShowById(machineryServiceId);
            return Ok(response);
        }

        public IActionResult GetMachineHistoryList(ReportFilter ReportFilterRequest)
        {
            ReportFilterRequest.searchStartDate = ReportFilterRequest.searchStartDate == DateTime.MinValue ? DateTime.MinValue : ReportFilterRequest.searchStartDate;
            ReportFilterRequest.searchEndDate = ReportFilterRequest.searchEndDate == DateTime.MinValue ? DateTime.MaxValue : ReportFilterRequest.searchEndDate;
            var response = _iAdminReportService.GetMachineHistoryList(ReportFilterRequest);
            return Ok(response);
        }


        public IActionResult AddFileUploadMachineHistory(FileView view)
        {
            var response = _iAdminReportService.AddFileUploadMachineHistory(view);
            return RedirectToAction("Index", "AdminReport");

        }

    }
}
