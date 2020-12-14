using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using Mikroportal.MODEL.RequestResponseClasses.ACS.Admin;
using Mikroportal.MODEL.ServiceContracts.ACS.Admin;

namespace Mikroportal.API.Controllers.ACS.Admin
{
    [Route("api/AdminCallCenter/[action]")]
    [ApiController]
    public class AdminCallCenterController : Controller
    {
        IAdminCallCenterService _adminCallCenter;

        public AdminCallCenterController(IAdminCallCenterService adminCallCenter)
        {
            _adminCallCenter = adminCallCenter;
        }
        [HttpGet]

        [HttpGet("{machineryServiceId}")]
        public IActionResult InboxShowById(string machineryServiceId)
        {
            var response = _adminCallCenter.InboxShowById(machineryServiceId);

            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }


        [HttpPost]
        public IActionResult GetMachineHistoryList(CallCenterFilter CallCenterRequest)
        {
            var response = _adminCallCenter.GetCallCenterList(CallCenterRequest);
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }



        [HttpPost]
        public IActionResult AddFileUploadCallCenter(FileView request)
        {
            var response = _adminCallCenter.AddFileUploadCallCenter(request);
            if (response.isSuccess == false)
            {
                return null;
            }
            if (response != null)
                return View(response);
            else
                return View();

        }
    }
}
