using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mikroportal.MODEL.RequestResponseClasses.ACS.Admin;
using Mikroportal.MODEL.ServiceContracts.ACS.Admin;

namespace Mikroportal.API.Controllers.ACS.Admin
{
    [Route("api/AdminHome/[action]")]
    [ApiController]
    public class AdminHomeController : Controller
    {
        IAdminHomeService _adminHome;

        public AdminHomeController(IAdminHomeService adminHome)
        {
            _adminHome = adminHome;
        }

        [HttpPost]
        public IActionResult GetInboxCount()
        {
            var response = _adminHome.GetInboxCount();
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpPost]
        public IActionResult GetInboxList(SearchRequest searchRequest)
        {
            var response = _adminHome.GetInboxList(searchRequest);

            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpGet("{machineryServiceId}")]
        public IActionResult InboxShowById(string machineryServiceId)
        {
            var response = _adminHome.InboxShowById(machineryServiceId);

            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpGet("{machineryServiceId}")]
        public IActionResult IsApproved(string machineryServiceId)
        {
            var response = _adminHome.IsApproved(machineryServiceId);

            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }


        [HttpPost]
        public IActionResult IsRejected(RejectedRequest rejectedRequest)
        {
            var response = _adminHome.IsRejected(rejectedRequest);

            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpGet("{UserId}")]
        public IActionResult GetACSAdminMenu(int UserId)
        {

            var response = _adminHome.GetACSAdminMenu(UserId);
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }


    }
}
