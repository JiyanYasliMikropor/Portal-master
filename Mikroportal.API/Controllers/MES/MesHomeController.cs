using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mikroportal.MODEL.ServiceContracts.MES;

namespace Mikroportal.API.Controllers.MES
{
    [Route("api/MesHome/[action]")]
    [ApiController]
    public class MesHomeController : Controller
    {
        IMesHomeService _mesHomeService;
        public MesHomeController(IMesHomeService mesHomeService)
        {
            _mesHomeService = mesHomeService;
        }

        [HttpGet("{UserId}")]
        public IActionResult GetDashboardMenu(int UserId)
        {
            var response = _mesHomeService.GetDashboardMenu(UserId);
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }
    }
}