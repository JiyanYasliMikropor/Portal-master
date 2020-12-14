using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mikroportal.MODEL.RequestResponseClasses.ACS.Admin;
using Mikroportal.MODEL.ServiceContracts.ACS.Admin;

namespace Mikroportal.API.Controllers.ACS.Admin
{
    [Route("api/AdminAccount/[action]")]
    [ApiController]
    public class AdminAccountController : Controller
    {
        IAdminAccountService _adminAccount;

        public AdminAccountController(IAdminAccountService adminAccount)
        {
            _adminAccount = adminAccount;
        }

        [HttpPost]
        public IActionResult GetInboxCount()
        {
            var response = _adminAccount.GetInboxCount();
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpPost]
        public IActionResult GetPersonelList()
        {
            var response = _adminAccount.GetPersonelList();
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpPost]
        public IActionResult UserChangeRole(UserRoleChangeView view)
        {
            var response = _adminAccount.UserChangeRole(view);

            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }
    }
}
