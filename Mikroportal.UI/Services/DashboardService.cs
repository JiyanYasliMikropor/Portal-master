using Microsoft.Extensions.Configuration;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.ServiceContracts;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.UI.Services
{
  

    public class DashboardService : IDashboardService
    {
        IConfiguration _config;
        public DashboardService(IConfiguration config)
        {
            _config = config;
        }
        public GetDashboardMenuResponse GetDashboardMenu(int UserId)
        {
            var request = new RestRequest("api/Dashboard/GetDashboardMenu/" + UserId, Method.GET, DataFormat.Json)
                .AddUrlSegment("UserId", UserId);

            var resp = Globals.ApiClient.Execute<GetDashboardMenuResponse>(request);
            return resp.Data;
        }
    }
}
