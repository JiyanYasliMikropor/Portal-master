using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mikroportal.MODEL.RequestResponseClasses.ACS.Admin;
using Mikroportal.MODEL.ServiceContracts.ACS.Admin;

namespace Mikroportal.API.Controllers.ACS.Admin
{
    [Route("api/AddAuthorizedService/[action]")]
    [ApiController]
    public class AddAuthorizedServiceController : Controller
    {
        IAddAuthorizedServiceService _addAuthorizedService;

        public AddAuthorizedServiceController(IAddAuthorizedServiceService addAuthorizedService)
        {
            _addAuthorizedService = addAuthorizedService;
        }
        [HttpPost]
        public IActionResult GetInboxCount()
        {
            var response = _addAuthorizedService.GetInboxCount();
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }


        [HttpPost]
        public IActionResult AddService(AddAuthorizedService view)
        {
            var response = _addAuthorizedService.AddService(view);

            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }
    }
}
