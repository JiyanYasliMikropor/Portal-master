using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mikroportal.MODEL.ServiceContracts;

namespace Mikroportal.API.Controllers
{
    [Route("api/Dashboard/[action]")]
    [ApiController]
    public class DashboardController : Controller
    {
        IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardServic)
        {
            _dashboardService = dashboardServic;
        }

        [HttpGet("{UserId}")]
        public IActionResult GetDashboardMenu(int UserId)
        {

            var response = _dashboardService.GetDashboardMenu(UserId);
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

    }
}
