using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.PP.PP_OT;
using Mikroportal.MODEL.ServiceContracts.PP.PP_OT;
using Mikroportal.PP.Models;
using RestSharp;

namespace Mikroportal.PP.Controllers
{
    //Authorize attribute ile bu sınıfı sadece yetkisi yani tokenı olan kişilerin girmesini söylüyorum.
    [Authorize]
    [Authorize(Roles = "PP-OT")]
    public class PP_OTController : Controller
    {
        IPP_OTService _iPP_OTService;
        private readonly ILogger<PP_OTController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PP_OTController(ILogger<PP_OTController> logger, IPP_OTService iPP_OTService, IHttpContextAccessor iHttpContextAccessor)
        {
            _logger = logger;
            _iPP_OTService = iPP_OTService;
            _httpContextAccessor = iHttpContextAccessor;
        }

        public IActionResult Index()
        {
            ViewBag.UrlPath = Globals.URLPath;
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];

            GetDashboardMenuResponse response = _iPP_OTService.GetPPMenu(Convert.ToInt32(userId));

            ViewBag.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];

            ViewBag.UserId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            if (response != null)
                return View(response);
            else
                return View();

        }

        public IActionResult OvertimeRequest()
        {
            ViewBag.UrlPath = Globals.URLPath;
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];

            GetDashboardMenuResponse response = _iPP_OTService.GetPPMenu(Convert.ToInt32(userId));

            ViewBag.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
            if (response != null)
                return View(response);
            else
                return View();

        }

        public async Task<IActionResult> GetAllOvertimeList(OvertimeFilter OvertimeFilterRequest)
        {
            OvertimeFilterRequest.searchStartDate = OvertimeFilterRequest.searchStartDate == DateTime.MinValue ? DateTime.MinValue : OvertimeFilterRequest.searchStartDate;
            OvertimeFilterRequest.searchEndDate = OvertimeFilterRequest.searchEndDate == DateTime.MinValue ? DateTime.MaxValue : OvertimeFilterRequest.searchEndDate;
            OvertimeFilterRequest.UserId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            GetAllOvertimeListResponse response = _iPP_OTService.GetAllOvertimeList(OvertimeFilterRequest);
            return Ok(response);
        }

        public async Task<IActionResult> GetAllOvertimeListFilter()
        {
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            string durum = HttpContext.Request.Query["durum"].ToString();
            GetAllOvertimeListResponse response = _iPP_OTService.GetAllOvertimeListFilter(Convert.ToInt32(userId), durum);
            return Ok(response);
        }

        public async Task<IActionResult> SelectedItemApproved()
        {
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            string FMList = HttpContext.Request.Query["FMList"].ToString();
            SelectedItemApprovedResponse response = _iPP_OTService.SelectedItemApproved(Convert.ToInt32(userId), FMList);
            return Ok(response);
        }

        public async Task<IActionResult> AllApproved()
        {
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            AllApprovedResponse response = _iPP_OTService.AllApproved(Convert.ToInt32(userId));
            return Ok(response);
        }
        

        public async Task<IActionResult> SelectedItemDenial()
        {
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            string FMList = HttpContext.Request.Query["FMList"].ToString();
            SelectedItemDenialResponse response = _iPP_OTService.SelectedItemDenial(Convert.ToInt32(userId), FMList);
            return Ok(response);
        }

        public async Task<IActionResult> AllListDenial()
        {
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            AllListDenialResponse response = _iPP_OTService.AllListDenial(Convert.ToInt32(userId));
            return Ok(response);
        }


        public async Task<IActionResult> GetAllPersonList(GetPersonelView view)
        {
            GetAllPersonListResponse response = _iPP_OTService.GetAllPersonList(view);
            return Ok(response);
        }

        public async Task<IActionResult> GetAllOvertimeTypeList()
        {
            GetAllOvertimeTypeListResponse response = _iPP_OTService.GetAllOvertimeTypeList();
            return Ok(response);
        }

        public async Task<IActionResult> GetAllShiftList()
        {
            GetAllShiftListResponse response = _iPP_OTService.GetAllShiftList();
            return Ok(response);
        }

        public async Task<IActionResult> GetAllDepartmentList()
        {
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            GetAllDepartmentListResponse response = _iPP_OTService.GetAllDepartmentList(Convert.ToInt32(userId));
            return Ok(response);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove(Globals.LOGGED_IN_USER_SESSION_KEY);
            HttpContext.Session.Remove(Globals.LOGGED_IN_USER_TOKEN_COOKIE_KEY);
            HttpContext.Session.Remove(Globals.Session_USER_NAME);
            HttpContext.Session.Remove(Globals.Session_USER_ID);
            HttpContext.Response.Cookies.Delete("UserToken");
            HttpContext.Response.Cookies.Delete("UserId");
            HttpContext.Response.Cookies.Delete("UserName");
            Globals.ApiClient.RemoveDefaultParameter("Authentication");

            return Redirect("http://192.168.107.11/Account/LogOut");
        }

        public IActionResult AddOvertimeRequest(OvertimeRequestView request)
        {
            request.UserId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            var response = _iPP_OTService.AddOvertimeRequest(request);
            return RedirectToAction("Index", "PP_OT");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
