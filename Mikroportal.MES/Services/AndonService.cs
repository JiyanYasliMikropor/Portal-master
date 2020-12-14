using Microsoft.Extensions.Configuration;
using Mikroportal.MODEL.RequestResponseClasses.MES;
using Mikroportal.MODEL.ServiceContracts.MES;
using Mikroportal.UI;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.MES.Services
{
    public class AndonService : IAndonService
    {
        IConfiguration _config;
        public AndonService(IConfiguration config)
        {
            _config = config;
        }
        public GetAndonListResponse GetAndonList()
        {
            var request = new RestRequest("api/Andon/GetAndonList/", Method.GET, DataFormat.Json);                

            var resp = Globals.ApiClient.Execute<GetAndonListResponse>(request);
            return resp.Data;
        }
    }
}
