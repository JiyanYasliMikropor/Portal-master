using Microsoft.Extensions.Configuration;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.ServiceContracts.MES;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.MES.Services
{
    public class MesHomeService : IMesHomeService
    {

        IConfiguration _config;
        public MesHomeService(IConfiguration config)
        {
            _config = config;
        }

       
        public GetDashboardMenuResponse GetDashboardMenu(int UserId)
        {
            var request = new RestRequest("api/MesHome/GetDashboardMenu/" + UserId, Method.GET, DataFormat.Json)
              .AddUrlSegment("UserId", UserId);

            var resp = Globals.ApiClient.Execute<GetDashboardMenuResponse>(request);
            return resp.Data;

        }
    }
}
