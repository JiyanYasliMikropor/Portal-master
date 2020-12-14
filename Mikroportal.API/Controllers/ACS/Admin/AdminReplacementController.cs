using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mikroportal.MODEL.RequestResponseClasses.ACS.Admin;
using Mikroportal.MODEL.ServiceContracts.ACS.Admin;

namespace Mikroportal.API.Controllers.ACS.Admin
{
    [Route("api/AdminReplacement/[action]")]
    [ApiController]
    public class AdminReplacementController : Controller
    {
        IAdminReplacementService _adminReplacement;

        public AdminReplacementController(IAdminReplacementService adminReplacement)
        {
            _adminReplacement = adminReplacement;
        }


        [HttpPost]
        public IActionResult GetReplacementList(SearchPartNo searchPartNo)
        {
            var response = _adminReplacement.GetReplacementList(searchPartNo);

            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpPost]
        public IActionResult GetInboxCount()
        {
            var response = _adminReplacement.GetInboxCount();
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }


        [HttpPost]

        public IActionResult SaveReplacementPrice(UpdateReplacementPrice UpdateReplacementPrice)
        {
            var response = _adminReplacement.SaveReplacementPrice(UpdateReplacementPrice);
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);

        }

    }
}
