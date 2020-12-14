using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }


        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginRequest request)
        {

            var response = _loginService.Login(request);
            if (response.isSuccess == false)
            {
                return Unauthorized(response);
            }
            return Ok(response);
        }

    }
}
