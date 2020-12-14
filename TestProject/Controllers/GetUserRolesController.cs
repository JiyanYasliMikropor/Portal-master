using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestProject.Controllers
{
    public class GetUserRolesController : Controller
    {

        //public GetUserRolesController(IHttpContextAccessor httpContextAccessor, IGetUserRoleServices getUserRoleServices)
        //{

        //}

        //public SalesOrdersController(IHttpContextAccessor httpContextAccessor, ISalesOrdersService salesOrdersService)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //    _isalesOrdersService = salesOrdersService;
        //}


        public IActionResult GetUserIndex()
        {
            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}";
            string apiUrl = "";
            if (baseUrl.Contains("localhost"))
                apiUrl = "http://localhost:60445";
            else
                apiUrl = "http://192.168.107.11:81";
            ViewBag.UrlPath = apiUrl;
            return View();
            
        }
        //[HttpPost]
        //public async Task<IActionResult> GetUserRoles(UserRoleRequestParams requestParams)
        //{

        //}


    }
}
