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
    public class AdminHomeService : IAdminHomeService
    {
        IConfiguration _config;
        public AdminHomeService(IConfiguration config)
        {
            _config = config;
        }

        public GetInboxCountResponse GetInboxCount()
        {
            var request = new RestRequest("api/AdminHome/GetInboxCount/", Method.POST, DataFormat.Json);
            var resp = Globals.ApiClient.Execute<GetInboxCountResponse>(request);
            return resp.Data;
        }


        public GetInboxListResponse GetInboxList(SearchRequest searchRequest)
        {
            var request = new RestRequest("api/AdminHome/GetInboxList", Method.POST, DataFormat.Json)
          .AddJsonBody(searchRequest);

            var resp = Globals.ApiClient.Execute<GetInboxListResponse>(request);
            return resp.Data;
        }

        public InboxShowByIdResponse InboxShowById(string machineryServiceId)
        {
            var request = new RestRequest("api/AdminHome/InboxShowById/" + machineryServiceId, Method.GET, DataFormat.Json)
         .AddUrlSegment("machineryServiceId", machineryServiceId);

            var resp = Globals.ApiClient.Execute<InboxShowByIdResponse>(request);
            return resp.Data;
        }

        public TblSshMachineryServicesResponse IsApproved(string machineryServiceId)
        {
            var request = new RestRequest("api/AdminHome/IsApproved/" + machineryServiceId, Method.GET, DataFormat.Json)
            .AddUrlSegment("machineryServiceId", machineryServiceId);

            var resp = Globals.ApiClient.Execute<TblSshMachineryServicesResponse>(request);
            return resp.Data;
        }

        public TblSshMachineryServicesResponse IsRejected(RejectedRequest rejectedRequest)
        {

            var request = new RestRequest("api/AdminHome/IsRejected", Method.POST, DataFormat.Json)
                 .AddJsonBody(rejectedRequest);

            var resp = Globals.ApiClient.Execute<TblSshMachineryServicesResponse>(request);
            return resp.Data;
        }

        public GetDashboardMenuResponse GetACSAdminMenu(int UserId)
        {
            var request = new RestRequest("api/AdminHome/GetACSAdminMenu/" + UserId, Method.GET, DataFormat.Json)
                .AddUrlSegment("UserId", UserId);

            var resp = Globals.ApiClient.Execute<GetDashboardMenuResponse>(request);
            return resp.Data;
        }

        //public TblSshMachineryServicesResponse IsRejected(RejectedRequest rejectedRequest)
        //{
        //    var request = new RestRequest("api/AdminHome/IsRejected/" + machineryServiceId + "/" + isRejectedComment, Method.GET, DataFormat.Json)
        //     .AddUrlSegment("machineryServiceId", rejectedRequest.machineryServiceId)
        //     .AddUrlSegment("isRejectedComment", rejectedRequest.isRejectedComment);

        //    var resp = Globals.ApiClient.Execute<TblSshMachineryServicesResponse>(request);
        //    return resp.Data;
        //}
    }
}
