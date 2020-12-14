using Microsoft.Extensions.Configuration;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using Mikroportal.MODEL.RequestResponseClasses.ACS.Admin;
using Mikroportal.MODEL.ServiceContracts.ACS.Admin;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.ACS.Services.Admin
{
    public class AdminReplacementService : IAdminReplacementService
    {
        IConfiguration _config;
        public AdminReplacementService(IConfiguration config)
        {
            _config = config;
        }

        public GetReplacementListResponse GetReplacementList(SearchPartNo searchPartNo)
        {
            var request = new RestRequest("api/AdminReplacement/GetReplacementList", Method.POST, DataFormat.Json)
          .AddJsonBody(searchPartNo);

            var resp = Globals.ApiClient.Execute<GetReplacementListResponse>(request);
            return resp.Data;
        }

        public GetInboxCountResponse GetInboxCount()
        {
            var request = new RestRequest("api/AdminReplacement/GetInboxCount/", Method.POST, DataFormat.Json);
            var resp = Globals.ApiClient.Execute<GetInboxCountResponse>(request);
            return resp.Data;
        }


        public GetReplacementListResponse SaveReplacementPrice(UpdateReplacementPrice UpdateReplacementPrice)
        {
            var request = new RestRequest("api/AdminReplacement/SaveReplacementPrice", Method.POST, DataFormat.Json)
        .AddJsonBody(UpdateReplacementPrice);

            var resp = Globals.ApiClient.Execute<GetReplacementListResponse>(request);
            return resp.Data;
        }
        public GetDashboardMenuResponse GetACSAdminMenu(int UserId)
        {
            var request = new RestRequest("api/AdminHome/GetACSAdminMenu/" + UserId, Method.GET, DataFormat.Json)
                .AddUrlSegment("UserId", UserId);

            var resp = Globals.ApiClient.Execute<GetDashboardMenuResponse>(request);
            return resp.Data;
        }

    }
}
