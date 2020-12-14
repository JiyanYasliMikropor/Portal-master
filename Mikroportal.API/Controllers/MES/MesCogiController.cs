using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mikroportal.MODEL.RequestResponseClasses.MES;
using Mikroportal.MODEL.ServiceContracts.MES;

namespace Mikroportal.API.Controllers.MES
{
    [Route("api/MesCogi/[action]")]
    [ApiController]
    public class MesCogiController : Controller
    {
        IMesCogiService _mesCogiService;

        public MesCogiController(IMesCogiService mesCogiService)
        {
            _mesCogiService = mesCogiService;
        }

        //[HttpPost]
        //public IActionResult BildirimleriGoster(MesCogiRequest request)
        //{
        //    var response = _mesCogiService.BildirimleriGoster(request);
        //    if (response.isSuccess == false)
        //    {
        //        return null;
        //    }
        //    return Ok(response);
        //}



        [HttpGet("{UserId}")]
        public IActionResult BildirimleriGoster(int UserId)
        {
            var response = _mesCogiService.BildirimleriGoster(UserId);
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }




        [HttpPost]
        public IActionResult CogiIslemeAl(CogiIslemeAlRequest request)
        {
            var response = _mesCogiService.CogiIslemeAl(request);
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }




        [HttpPost]
        public IActionResult CogiIslemSil(CogiSilRequest request)
        {
            var response = _mesCogiService.CogiIslemSil(request);
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }


    }
}
