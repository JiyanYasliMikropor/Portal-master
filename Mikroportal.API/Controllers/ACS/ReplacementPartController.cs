using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mikroportal.MODEL.ServiceContracts.ACS;

namespace Mikroportal.API.Controllers.ACS
{
    [Route("api/ReplacementPart/[action]")]
    [ApiController]
    public class ReplacementPartController : Controller
    {

        IReplacementPartService _replacementPart;

        public ReplacementPartController(IReplacementPartService replacementPart)
        {
            _replacementPart = replacementPart;
        }

        [HttpGet("{UserId}")]
        public IActionResult GetDashboardMenu(int UserId)
        {

            var response = _replacementPart.GetDashboardMenu(UserId);
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpGet("{serialNo}")]
        public IActionResult SerialNoCheckReplacementPart(string serialNo)
        {
            var response = _replacementPart.SerialNoCheckReplacementPart(serialNo);

            return Ok(response);
        }
        [HttpGet("{modelNo}")]
        public IActionResult ModelNoCheckReplacementPart(string modelNo)
        {
            var response = _replacementPart.ModelNoCheckReplacementPart(modelNo);

            return Ok(response);
        }

        [HttpGet("{partNo}")]
        public IActionResult GetReplacementPartPriceByPartNo(string partNo)
        {
            var response = _replacementPart.GetReplacementPartPriceByPartNo(partNo);

            return Ok(response);
        }




    }
}
