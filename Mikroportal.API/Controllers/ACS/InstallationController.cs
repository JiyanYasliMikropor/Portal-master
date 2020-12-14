using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using Mikroportal.MODEL.ServiceContracts.ACS;

namespace Mikroportal.API.Controllers.ACS
{
    [Route("api/Installation/[action]")]
    [ApiController]
    public class InstallationController : Controller
    {
        IInstallationService _installation;

        public InstallationController(IInstallationService installation)
        {
            _installation = installation;
        }

        [HttpGet("{UserId}")]
        public IActionResult GetDashboardMenu(int UserId)
        {

            var response = _installation.GetDashboardMenu(UserId);
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpGet("{serialNo}")]
        public IActionResult SerialNoCheck(string serialNo)
        {
            var response = _installation.SerialNoCheck(serialNo);
        
            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddMachine(InstallationView view)
        {
            var response = _installation.AddMachine(view);
            return Ok(response);
        }

    }
}
