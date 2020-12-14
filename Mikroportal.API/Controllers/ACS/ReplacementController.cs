using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using Mikroportal.MODEL.ServiceContracts.ACS;

namespace Mikroportal.API.Controllers.ACS
{
    [Route("api/Replacement/[action]")]
    [ApiController]
    public class ReplacementController : Controller
    {
        IReplacementService _replacement;

        public ReplacementController(IReplacementService replacement)
        {
            _replacement = replacement;
        }

        [HttpGet("{UserId}")]
        public IActionResult GetDashboardMenu(int UserId)
        {

            var response = _replacement.GetDashboardMenu(UserId);
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpGet("{serialNo}")]
        public IActionResult SerialNoCheckAndHistory(string serialNo)
        {
            var response = _replacement.SerialNoCheckAndHistory(serialNo);

            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddReplacement(ReplacementView view)
        {
            var response = _replacement.AddReplacement(view);
            return Ok(response);
        }

    }
}
