using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using Mikroportal.MODEL.ServiceContracts.ACS.Admin;

namespace Mikroportal.API.Controllers.ACS.Admin
{
    [Route("api/AddGuarantee/[action]")]
    [ApiController]
    public class AddGuaranteeController : Controller
    {
        IAddGuaranteeService _addGuarantee;

        public AddGuaranteeController(IAddGuaranteeService addGuarantee)
        {
            _addGuarantee = addGuarantee;
        }

        [HttpPost]
        public IActionResult GetInboxCount()
        {
            var response = _addGuarantee.GetInboxCount();
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpGet("{serialNo}")]
        public IActionResult SerialNoCheckGuarantee(string serialNo)
        {
            var response = _addGuarantee.SerialNoCheckGuarantee(serialNo);

            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddGuarantee(GuaranteeView view)
        {
            var response = _addGuarantee.AddGuarantee(view);

            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }


        [HttpPost]
        public IActionResult AddBanGuarantee(GuaranteeView view)
        {
            var response = _addGuarantee.AddBanGuarantee(view);

            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }


    }
}
