using Microsoft.Extensions.Configuration;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.PP.PP_OT;
using Mikroportal.MODEL.ServiceContracts.PP.PP_OT;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.PP.Services
{
    public class PP_OTService : IPP_OTService
    {
        IConfiguration _config;
        public PP_OTService(IConfiguration config)
        {
            _config = config;
        }
        public GetDashboardMenuResponse GetPPMenu(int UserId)
        {
            var request = new RestRequest("api/PP_OT/GetPPMenu/" + UserId, Method.GET, DataFormat.Json)
                .AddUrlSegment("UserId", UserId);

            var resp = Globals.ApiClient.Execute<GetDashboardMenuResponse>(request);
            return resp.Data;
        }

        public GetAllOvertimeListResponse GetAllOvertimeList(OvertimeFilter OvertimeFilterRequest)
        {
            var request = new RestRequest("api/PP_OT/GetAllOvertimeList", Method.POST, DataFormat.Json)
            .AddJsonBody(OvertimeFilterRequest);

            var resp = Globals.ApiClient.Execute<GetAllOvertimeListResponse>(request);
            return resp.Data;

        }

        public GetAllOvertimeListResponse GetAllOvertimeListFilter(int UserId, string Durum)
        {
            var request = new RestRequest("api/PP_OT/GetAllOvertimeListFilter/" + UserId + "/" + Durum, Method.GET, DataFormat.Json)
          .AddUrlSegment("UserId", UserId)
               .AddUrlSegment("Durum", Durum);
            var resp = Globals.ApiClient.Execute<GetAllOvertimeListResponse>(request);
            return resp.Data;
        }

        public SelectedItemApprovedResponse SelectedItemApproved(int UserId, string FMList)
        {
            var request = new RestRequest("api/PP_OT/SelectedItemApproved/" + UserId + "/" + FMList, Method.GET, DataFormat.Json)
          .AddUrlSegment("UserId", UserId)
               .AddUrlSegment("FMList", FMList);
            var resp = Globals.ApiClient.Execute<SelectedItemApprovedResponse>(request);
            return resp.Data;
        }

        public AllApprovedResponse AllApproved(int UserId)
        {
            var request = new RestRequest("api/PP_OT/AllApproved/" + UserId, Method.GET, DataFormat.Json)
           .AddUrlSegment("UserId", UserId);
            var resp = Globals.ApiClient.Execute<AllApprovedResponse>(request);
            return resp.Data;
        }

        public SelectedItemDenialResponse SelectedItemDenial(int UserId, string FMList)
        {
            var request = new RestRequest("api/PP_OT/SelectedItemDenial/" + UserId + "/" + FMList, Method.GET, DataFormat.Json)
          .AddUrlSegment("UserId", UserId)
               .AddUrlSegment("FMList", FMList);
            var resp = Globals.ApiClient.Execute<SelectedItemDenialResponse>(request);
            return resp.Data;
        }
        public AllListDenialResponse AllListDenial(int UserId)
        {
            var request = new RestRequest("api/PP_OT/AllListDenial/" + UserId, Method.GET, DataFormat.Json)
           .AddUrlSegment("UserId", UserId);
            var resp = Globals.ApiClient.Execute<AllListDenialResponse>(request);
            return resp.Data;
        }

        public GetAllPersonListResponse GetAllPersonList(GetPersonelView view)
        {
            var request = new RestRequest("api/PP_OT/GetAllPersonList", Method.POST, DataFormat.Json)
            .AddJsonBody(view);
            var resp = Globals.ApiClient.Execute<GetAllPersonListResponse>(request);
            return resp.Data;
        }

        public GetAllOvertimeTypeListResponse GetAllOvertimeTypeList()
        {
            var request = new RestRequest("api/PP_OT/GetAllOvertimeTypeList/", Method.GET, DataFormat.Json);
            var resp = Globals.ApiClient.Execute<GetAllOvertimeTypeListResponse>(request);
            return resp.Data;
        }

        public GetAllShiftListResponse GetAllShiftList()
        {
            var request = new RestRequest("api/PP_OT/GetAllShiftList/", Method.GET, DataFormat.Json);
            var resp = Globals.ApiClient.Execute<GetAllShiftListResponse>(request);
            return resp.Data;
        }

        public GetAllDepartmentListResponse GetAllDepartmentList(int UserId)
        {
            var request = new RestRequest("api/PP_OT/GetAllDepartmentList/" + UserId , Method.GET, DataFormat.Json)
          .AddUrlSegment("UserId", UserId);
    
            var resp = Globals.ApiClient.Execute<GetAllDepartmentListResponse>(request);
            return resp.Data;
        }

        public AddOvertimeRequestResponse AddOvertimeRequest(OvertimeRequestView overtimeRequestView)
        {
            var request = new RestRequest("api/PP_OT/AddOvertimeRequest/", Method.POST, DataFormat.Json)
   .AddJsonBody(overtimeRequestView);

            var resp = Globals.ApiClient.Execute<AddOvertimeRequestResponse>(request);
            return resp.Data;
        }




        //public GetDashboardMenuResponse GetDashboardMenu(int UserId)
        //{
        //    var request = new RestRequest("api/Dashboard/GetDashboardMenu/" + UserId, Method.GET, DataFormat.Json)
        //        .AddUrlSegment("UserId", UserId);

        //    var resp = Globals.ApiClient.Execute<GetDashboardMenuResponse>(request);
        //    return resp.Data;
        //}
    }
}
