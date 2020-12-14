using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mikroportal.MODEL.ServiceContracts.ACS;

namespace Mikroportal.API.Controllers.ACS
{
    [Route("api/TechnicalDocument/[action]")]
    [ApiController]
    public class TechnicalDocumentController : Controller
    {
        ITechnicalDocumentService _technicalDocument;

        public TechnicalDocumentController(ITechnicalDocumentService technicalDocument)
        {
            _technicalDocument = technicalDocument;
        }

        [HttpGet("{UserId}")]
        public IActionResult GetDashboardMenu(int UserId)
        {

            var response = _technicalDocument.GetDashboardMenu(UserId);
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }
        [HttpPost("{serialNo}/{UserId}")]
        public IActionResult SerialNoCheckTechnicalDocument(string serialNo, string UserId)
        {
            var response = _technicalDocument.SerialNoCheckTechnicalDocument(serialNo, UserId);


            return Ok(response);
        }
    }
}
