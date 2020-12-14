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
    public class AdminAccountService: IAdminAccountService
    {
        IConfiguration _config;
        public AdminAccountService(IConfiguration config)
        {
            _config = config;
        }

        public GetInboxCountResponse GetInboxCount()
        {
            var request = new RestRequest("api/AdminAccount/GetInboxCount/", Method.POST, DataFormat.Json);
            var resp = Globals.ApiClient.Execute<GetInboxCountResponse>(request);
            return resp.Data;
        }
        public GetDashboardMenuResponse GetACSAdminMenu(int UserId)
        {
            var request = new RestRequest("api/AdminHome/GetACSAdminMenu/" + UserId, Method.GET, DataFormat.Json)
                .AddUrlSegment("UserId", UserId);

            var resp = Globals.ApiClient.Execute<GetDashboardMenuResponse>(request);
            return resp.Data;
        }
        public GetPersonelAndRoleListResponse GetPersonelList()
        {
            var request = new RestRequest("api/AdminAccount/GetPersonelList/", Method.POST, DataFormat.Json);
            var resp = Globals.ApiClient.Execute<GetPersonelAndRoleListResponse>(request);
            return resp.Data;
        }


        public GetPersonelAndRoleListResponse UserChangeRole(UserRoleChangeView view)
        {
            var request = new RestRequest("api/AdminAccount/UserChangeRole", Method.POST, DataFormat.Json)
                 .AddJsonBody(view);

            var resp = Globals.ApiClient.Execute<GetPersonelAndRoleListResponse>(request);
            return resp.Data;
        }




    }
}
