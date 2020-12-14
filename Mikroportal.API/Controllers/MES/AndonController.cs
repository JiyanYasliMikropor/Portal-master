using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mikroportal.MODEL.ServiceContracts.MES;

namespace Mikroportal.API.Controllers.MES
{
    [Route("api/Andon/[action]")]
    [ApiController]
    public class AndonController : Controller
    {
        IAndonService _mesAndonService;
        public AndonController(IAndonService mesAndonService)
        {
            _mesAndonService = mesAndonService;
        }

        [HttpGet]
        public IActionResult GetAndonList()
        {
            var response = _mesAndonService.GetAndonList();
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

    }
}
