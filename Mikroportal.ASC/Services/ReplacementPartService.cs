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
    public class ReplacementPartService : IReplacementPartService
    {
        IConfiguration _config;
        public ReplacementPartService(IConfiguration config)
        {
            _config = config;
        }
        public GetDashboardMenuResponse GetDashboardMenu(int UserId)
        {
            var request = new RestRequest("api/ReplacementPart/GetDashboardMenu/" + UserId, Method.GET, DataFormat.Json)
                .AddUrlSegment("UserId", UserId);

            var resp = Globals.ApiClient.Execute<GetDashboardMenuResponse>(request);
            return resp.Data;
        }
        public SerialNoCheckReplacementPartResponse SerialNoCheckReplacementPart(string serialNo)
        {
            var request = new RestRequest("api/ReplacementPart/SerialNoCheckReplacementPart/" + serialNo, Method.GET, DataFormat.Json)
            .AddUrlSegment("serialNo", serialNo);

            var resp = Globals.ApiClient.Execute<SerialNoCheckReplacementPartResponse>(request);
            return resp.Data;
        }
        public ModelNoCheckReplacementPartResponse ModelNoCheckReplacementPart(string modelNo)
        {
            var request = new RestRequest("api/ReplacementPart/ModelNoCheckReplacementPart/" + modelNo, Method.GET, DataFormat.Json)
            .AddUrlSegment("modelNo", modelNo);

            var resp = Globals.ApiClient.Execute<ModelNoCheckReplacementPartResponse>(request);
            return resp.Data;
        }
        public GetReplacementPartPriceByPartNoResponse GetReplacementPartPriceByPartNo(string partNo)
        {
            var request = new RestRequest("api/ReplacementPart/GetReplacementPartPriceByPartNo/" + partNo, Method.GET, DataFormat.Json)
            .AddUrlSegment("partNo", partNo);

            var resp = Globals.ApiClient.Execute<GetReplacementPartPriceByPartNoResponse>(request);
            return resp.Data;
        }

    }
}
