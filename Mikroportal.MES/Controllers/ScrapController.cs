using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.MES;
using Mikroportal.MODEL.ServiceContracts.MES;

namespace Mikroportal.MES.Controllers
{
    public class ScrapController : Controller
    {
        IScrapService _scrapService;
        IMesHomeService _mesHomeService;
        private readonly ILogger<ScrapController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ScrapController(ILogger<ScrapController> logger, IHttpContextAccessor httpContextAccessor, IScrapService scrapService, IMesHomeService mesHomeService)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _scrapService = scrapService;
            _mesHomeService = mesHomeService;
        }



        public IActionResult ScrapIndex()
        {
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            GetDashboardMenuResponse response = _mesHomeService.GetDashboardMenu(Convert.ToInt32(userId));
            ViewBag.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];

            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}";
            string apiUrl = "";
            if (baseUrl.Contains("localhost"))
                apiUrl = "http://localhost:60445";
            else
                apiUrl = "http://192.168.107.11:81";

            ViewBag.UrlPath = apiUrl;
            if (response != null)
                return View(response);
            else
                return View();
        }

    
        [HttpPost]
        public async Task<IActionResult> HurdaMaliyetRaporuGetir(HurdaMaliyetSearchRequest request)
        {
            //string serialNo = HttpContext.Request.Query["serialNo"].ToString();
            //string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            HurdaMaliyetRaporuResponse response = _scrapService.HurdaMaliyetRaporuGetir(request);
            return Ok(response);
        }
   

    }
}