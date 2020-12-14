using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using Mikroportal.MODEL.ServiceContracts.ACS;

namespace Mikroportal.API.Controllers.ACS
{
    [Route("api/CallCenter/[action]")]
    [ApiController]
    public class CallCenterController : Controller
    {

        ICallCenterService _callCenter;

        public CallCenterController(ICallCenterService callCenter)
        {
            _callCenter = callCenter;
        }

        [HttpGet("{UserId}")]
        public IActionResult GetDashboardMenu(int UserId)
        {

            var response = _callCenter.GetDashboardMenu(UserId);
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddCallCenter(CallCenterView view)
        {
            var response = _callCenter.AddCallCenter(view);
            return Ok(response);
        }
    }
}
