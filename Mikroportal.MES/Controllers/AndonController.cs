using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.ServiceContracts.MES;
using Mikroportal.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.MES.Controllers
{
    public class AndonController
    {

        IAndonService _andonService;
        private readonly ILogger<AndonController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AndonController(ILogger<AndonController> logger, IAndonService andonService, IHttpContextAccessor iHttpContextAccessor)
        {
            _logger = logger;
            _andonService = andonService;
            _httpContextAccessor = iHttpContextAccessor;
        }   
        
        public string Deneme()
        {
            return "";
        }
        
    }
}
