using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mikroportal.MODEL.Entities;
using Mikroportal.MODEL.RequestResponseClasses.CustomerPortal;
using Mikroportal.MODEL.ServiceContracts.CustomerPortal;
using RestSharp;
using Microsoft.Extensions.Configuration;


namespace CustomerPortal.Services 
{
    public class SalesOrdersService : ISalesOrdersService
    {
        IConfiguration _config;
        public SalesOrdersService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<SalesOrdersDetailsResponse> GetSalesOrders(SalesOrdersRequestParams requestParams)
        {
            var request =  new RestRequest("/api/GetSalesOrders", Method.POST, DataFormat.Json)
       .AddJsonBody(requestParams);

           var resp = await Globals.ApiClient.ExecuteAsync<SalesOrdersDetailsResponse>(request);
            return resp.Data;
        }
    }
}
