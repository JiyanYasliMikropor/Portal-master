using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using Mikroportal.MODEL.ServiceContracts.ACS.Admin;

namespace Mikroportal.API.Controllers.ACS.Admin
{
    [Route("api/AdminReport/[action]")]
    [ApiController]
    public class AdminReportController : Controller
    {
        IAdminReportService _adminReport;

        public AdminReportController(IAdminReportService adminReport)
        {
            _adminReport = adminReport;
        }

        [HttpGet("{machineryServiceId}")]
        public IActionResult InboxShowById(string machineryServiceId)
        {
            var response = _adminReport.InboxShowById(machineryServiceId);

            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpPost]
        public IActionResult GetMachineHistoryList(ReportFilter request)
        {
            var response = _adminReport.GetMachineHistoryList(request);
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }



        [HttpPost]
        public IActionResult AddFileUploadMachineHistory(FileView request)
        {
            var response = _adminReport.AddFileUploadMachineHistory(request);
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }


    }
}
