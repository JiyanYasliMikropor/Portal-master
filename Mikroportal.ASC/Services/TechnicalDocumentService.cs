using Microsoft.Extensions.Configuration;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using Mikroportal.MODEL.ServiceContracts.ACS;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Mikroportal.ACS.Services
{
    public class TechnicalDocumentService: ITechnicalDocumentService
    {
        IConfiguration _config;
        public TechnicalDocumentService(IConfiguration config)
        {
            _config = config;
        }
        public GetDashboardMenuResponse GetDashboardMenu(int UserId)
        {
            var request = new RestRequest("api/TechnicalDocument/GetDashboardMenu/" + UserId, Method.GET, DataFormat.Json)
                .AddUrlSegment("UserId", UserId);

            var resp = Globals.ApiClient.Execute<GetDashboardMenuResponse>(request);
            return resp.Data;
        }
        public TechnicalDocumentResponse SerialNoCheckTechnicalDocument(string serialNo, string UserId)
        {
            var request = new RestRequest("api/TechnicalDocument/SerialNoCheckTechnicalDocument/" + serialNo + "/" + UserId, Method.POST, DataFormat.Json)
            .AddUrlSegment("serialNo", serialNo)
                 .AddUrlSegment("UserId", UserId);

            var resp = Globals.ApiClient.Execute<TechnicalDocumentResponse>(request);
            return resp.Data;
        }
    }
}
