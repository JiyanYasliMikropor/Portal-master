using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using Mikroportal.MODEL.ServiceContracts.ACS;

namespace Mikroportal.API.Controllers.ACS
{
    [Route("api/RepairAndMaintenance/[action]")]
    [ApiController]
    public class RepairAndMaintenanceController : Controller
    {
        IRepairAndMaintenanceService _repairAndMaintenance;

        public RepairAndMaintenanceController(IRepairAndMaintenanceService repairAndMaintenance)
        {
            _repairAndMaintenance = repairAndMaintenance;
        }
        [HttpGet("{UserId}")]
        public IActionResult GetDashboardMenu(int UserId)
        {

            var response = _repairAndMaintenance.GetDashboardMenu(UserId);
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }


        [HttpGet("{serialNo}")]
        public IActionResult SerialNoCheckAndHistory(string serialNo)
        {
            var response = _repairAndMaintenance.SerialNoCheckAndHistory(serialNo);

            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddRepairAndMaintenance(RepairAndMaintenanceView view)
        {
            var response = _repairAndMaintenance.AddRepairAndMaintenance(view);
            return Ok(response);
        }
    }
}
