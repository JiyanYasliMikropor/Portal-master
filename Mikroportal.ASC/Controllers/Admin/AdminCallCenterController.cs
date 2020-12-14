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
    public class AdminCallCenterController : Controller
    {
        IAdminCallCenterService _iAdminCallCenterService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AdminCallCenterController> _logger;

        public AdminCallCenterController(ILogger<AdminCallCenterController> logger, IHttpContextAccessor iHttpContextAccessor, IAdminCallCenterService iAdminCallCenterService)
        {
            _logger = logger;
            _httpContextAccessor = iHttpContextAccessor;
            _iAdminCallCenterService = iAdminCallCenterService;
        }
        public IActionResult Index()
        {
            ViewBag.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
            ViewBag.UrlPath = Globals.URLPath;
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];

            GetDashboardMenuResponse response = _iAdminCallCenterService.GetACSAdminMenu(Convert.ToInt32(userId));
            if (response != null)
                return View(response);
            else
                return View();
        }

        public async Task<IActionResult> InboxShowById()
        {
            string machineryServiceId = HttpContext.Request.Query["machineryServiceId"].ToString();
            InboxShowByIdResponse response = _iAdminCallCenterService.InboxShowById(machineryServiceId);
            return Ok(response);
        }

        public async Task<IActionResult> GetCallCenterList(CallCenterFilter CallCenterRequest)
        {
            CallCenterRequest.searchStartDate = CallCenterRequest.searchStartDate == DateTime.MinValue ? DateTime.MinValue : CallCenterRequest.searchStartDate;
            CallCenterRequest.searchEndDate = CallCenterRequest.searchEndDate == DateTime.MinValue ? DateTime.MaxValue : CallCenterRequest.searchEndDate;


            GetCallCenterListResponse response = _iAdminCallCenterService.GetCallCenterList(CallCenterRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddFileUploadCallCenter(FileView view)
        {
            var response = _iAdminCallCenterService.AddFileUploadCallCenter(view);
            return RedirectToAction("Index", "AdminCallCenter");
        }


    }
}
