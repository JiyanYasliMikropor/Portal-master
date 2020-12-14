using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mikroportal.MODEL.RequestResponseClasses.PP.PP_OT;
using Mikroportal.MODEL.ServiceContracts.PP.PP_OT;

namespace Mikroportal.API.Controllers.PP.PP_OT
{

    [Route("api/PP_OT/[action]")]
    [ApiController]
    public class PP_OTController : Controller
    {
        IPP_OTService _pp_otService;
        public PP_OTController(IPP_OTService pp_otService)
        {
            _pp_otService = pp_otService;
        }

        [HttpGet("{UserId}")]
        public IActionResult GetPPMenu(int UserId)
        {

            var response = _pp_otService.GetPPMenu(UserId);
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }


        [HttpPost]
        public IActionResult GetAllOvertimeList(OvertimeFilter request)
        {
            var response = _pp_otService.GetAllOvertimeList(request);
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }



        [HttpGet("{UserId}/{Durum}")]
        public IActionResult GetAllOvertimeListFilter(int UserId, string Durum)
        {
            var response = _pp_otService.GetAllOvertimeListFilter(UserId, Durum);

            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpGet("{UserId}/{FMList}")]
        public IActionResult SelectedItemApproved(int UserId, string FMList)
        {
            var response = _pp_otService.SelectedItemApproved(UserId, FMList);

            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }


        [HttpGet("{UserId}")]
        public IActionResult AllApproved(int UserId)
        {
            var response = _pp_otService.AllApproved(UserId);

            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }


        [HttpGet("{UserId}/{FMList}")]
        public IActionResult SelectedItemDenial(int UserId, string FMList)
        {
            var response = _pp_otService.SelectedItemDenial(UserId, FMList);

            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpGet("{UserId}")]
        public IActionResult AllListDenial(int UserId)
        {
            var response = _pp_otService.AllListDenial(UserId);

            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpPost]
        public IActionResult GetAllPersonList(GetPersonelView view)
        {
            var response = _pp_otService.GetAllPersonList(view);

            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetAllOvertimeTypeList()
        {
            var response = _pp_otService.GetAllOvertimeTypeList();

            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetAllShiftList()
        {
            var response = _pp_otService.GetAllShiftList();

            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpGet("{UserId}")]
        public IActionResult GetAllDepartmentList(int UserId)
        {
            var response = _pp_otService.GetAllDepartmentList(UserId);

            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddOvertimeRequest(OvertimeRequestView view)
        {
            var response = _pp_otService.AddOvertimeRequest(view);
            if (response.isSuccess == false)
            {
                return null;
            }
            return Ok(response);
        }


    }

}
