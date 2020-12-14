using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mikroportal.MODEL.RequestResponseClasses.MES;
using Mikroportal.MODEL.ServiceContracts.MES;

namespace Mikroportal.API.Controllers.MES
{
    [Route("api/Scrap/[action]")]
    [ApiController]
    public class ScrapController : Controller
    {
        IScrapService _mesScrapService;
        public ScrapController(IScrapService mesScrapService)
        {
            _mesScrapService = mesScrapService;
        }


        [HttpPost]
        public IActionResult HurdaMaliyetRaporuGetir(HurdaMaliyetSearchRequest request)
        {
            var response = _mesScrapService.HurdaMaliyetRaporuGetir(request);
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpPost]
        public IActionResult HurdaNegatifeDusenlerRaporuGetir(HurdaNegatifSearchRequest request)
        {
            var response = _mesScrapService.HurdaNegatifeDusenlerRaporuGetir(request);
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpPost]
        public IActionResult HurdaNegatifDetayGetir(HurdaNegatifDetayRequest request)
        {
            var response = _mesScrapService.HurdaNegatifDetayGetir(request);
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpPost]
        public IActionResult HurdaNegatiftekiIsEmirleriGetir(HurdaNegatiftekiIsEmirleriRequest request)
        {
            var response = _mesScrapService.HurdaNegatiftekiIsEmirleriGetir(request);
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }



        [HttpGet]
        public IActionResult GetAllMachines()
        {
            var response = _mesScrapService.GetAllMachines();
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetAllMudurluk()
        {
            var response = _mesScrapService.GetAllMudurluk();
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }


    }
}