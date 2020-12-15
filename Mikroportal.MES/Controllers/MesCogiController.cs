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
    public class MesCogiController : Controller
    {
        IMesCogiService _mesCogiService;
        IMesHomeService _mesHomeService;
        private readonly ILogger<MesCogiController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MesCogiController(ILogger<MesCogiController> logger, IHttpContextAccessor httpContextAccessor, IMesCogiService mesCogiService, IMesHomeService mesHomeService)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _mesCogiService = mesCogiService;
            _mesHomeService = mesHomeService;

        }



        // jiyan yaşlı test
        public IActionResult MesCogiIndex()
        {
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            GetDashboardMenuResponse response = _mesHomeService.GetDashboardMenu(Convert.ToInt32(userId));
            ViewBag.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
            ViewBag.UserId = userId;

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


        [HttpGet]
        public async Task<IActionResult> BildirimleriGoster(int UserId)
        {
            string serialNo = HttpContext.Request.Query["serialNo"].ToString();
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];

            //GetDashboardMenuResponse response = _mesHomeService.GetDashboardMenu(Convert.ToInt32(userId));
            GetMesCogiResponse response = _mesCogiService.BildirimleriGoster(UserId);
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> CogiIslemeAl(CogiIslemeAlRequest request)
        {
            //string serialNo = HttpContext.Request.Query["serialNo"].ToString();
            //string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            GetCogiIslemeAlResponse response = _mesCogiService.CogiIslemeAl(request);
            return Ok(response);
        }



        [HttpPost]
        public async Task<IActionResult> CogiIslemSil(CogiSilRequest request)
        {
            //string serialNo = HttpContext.Request.Query["serialNo"].ToString();
            //string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            GetCogiSilResponse response = _mesCogiService.CogiIslemSil(request);
            return Ok(response);
        }


       






    }
}
