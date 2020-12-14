using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mikroportal.MODEL.Entities;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.CustomerPortal;
using Mikroportal.MODEL.ServiceContracts.CustomerPortal;



namespace CustomerPortal.Controllers
{
    public class SalesOrdersController: Controller
    {
        ISalesOrdersService _isalesOrdersService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SalesOrdersController(IHttpContextAccessor httpContextAccessor,ISalesOrdersService salesOrdersService)
        {
            _httpContextAccessor = httpContextAccessor;
            _isalesOrdersService = salesOrdersService;
        }

        public IActionResult SalesOrderIndex()
        {
            //todo: userid?
            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}";
            string apiUrl = "";
            if (baseUrl.Contains("localhost"))
                apiUrl = "http://localhost:60445";
            else
                apiUrl = "http://192.168.107.11:81";
            ViewBag.UrlPath = apiUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetSalesOrders(SalesOrdersRequestParams requestParam) 
        {
            SalesOrdersDetailsResponse response = await _isalesOrdersService.GetSalesOrders(requestParam);
            return Ok(response);
        }
    }
}
