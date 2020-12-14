using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mikroportal.MODEL.ServiceContracts.ACS;

namespace Mikroportal.API.Controllers.ACS
{
    [Route("api/Home/[action]")]
    [ApiController]
    public class HomeController : Controller
    {
        IHomeService _home;

        public HomeController(IHomeService home)
        {
            _home = home;
        }

        [HttpGet("{UserId}")]
        public IActionResult GetDashboardMenu(int UserId)
        {

            var response = _home.GetDashboardMenu(UserId);
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpGet("{password}/{UserId}")]
        public IActionResult SavePassword(string password,string UserId)
        {
            var response = _home.SavePassword(password, UserId);

            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }
    }
}
