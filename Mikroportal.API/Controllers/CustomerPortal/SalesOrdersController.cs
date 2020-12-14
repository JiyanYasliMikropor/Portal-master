using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mikroportal.MODEL.ServiceContracts.CustomerPortal;
using Mikroportal.MODEL.RequestResponseClasses.CustomerPortal;
using Mikroportal.MODEL.Entities;
namespace Mikroportal.API.Controllers.CustomerPortal
{
    [Route("api/[action]")]
    [ApiController]
    public class SalesOrdersController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        ISalesOrdersService _salesOrdersService;

        public SalesOrdersController(ISalesOrdersService salesOrdersService) {
            _salesOrdersService = salesOrdersService;
        }


        [HttpPost]
        public async Task<IActionResult> GetSalesOrders(SalesOrdersRequestParams requestParam) {
            try { 

               var response =await _salesOrdersService.GetSalesOrders(requestParam);
            if (response.ET_DATA.Count <=0)
            {
                return BadRequest();
            }
            else { 
                return Ok(response);
            }
            } catch (Exception ex) { throw; }


   
        }
    }
}
