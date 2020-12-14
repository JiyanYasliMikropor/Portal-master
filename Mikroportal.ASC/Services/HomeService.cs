using Microsoft.Extensions.Configuration;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.ServiceContracts.ACS;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.ACS.Services
{
    public class HomeService:IHomeService
    {
        IConfiguration _config;
        public HomeService(IConfiguration config)
        {
            _config = config;
        }

        public GetDashboardMenuResponse GetDashboardMenu(int UserId)
        {
            var request = new RestRequest("api/Home/GetDashboardMenu/" + UserId, Method.GET, DataFormat.Json)
                .AddUrlSegment("UserId", UserId);

            var resp = Globals.ApiClient.Execute<GetDashboardMenuResponse>(request);
            return resp.Data;
        }

        public LoginResponse SavePassword(string password, string UserId)
        {
            var request = new RestRequest("api/Home/SavePassword/" + password + "/" + UserId, Method.GET, DataFormat.Json)
            .AddUrlSegment("password", password)
            .AddUrlSegment("UserId", UserId);

            var resp = Globals.ApiClient.Execute<LoginResponse>(request);
            return resp.Data;
        }
    }
}
