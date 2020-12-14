using Microsoft.Extensions.Configuration;
using Mikroportal.MODEL.RequestResponseClasses.MES;
using Mikroportal.MODEL.ServiceContracts.MES;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.MES.Services
{


    public class MesCogiService : IMesCogiService
    {
        IConfiguration _config;
        public MesCogiService(IConfiguration config)
        {
            _config = config;
        }



        public GetMesCogiResponse BildirimleriGoster(int UserId)
        {
            var request = new RestRequest("api/MesCogi/BildirimleriGoster/" + UserId, Method.GET, DataFormat.Json)
              .AddUrlSegment("UserId", UserId);

            var resp = Globals.ApiClient.Execute<GetMesCogiResponse>(request);
            return resp.Data;

        }


        public GetCogiIslemeAlResponse CogiIslemeAl(CogiIslemeAlRequest srequest)
        {
            var request = new RestRequest("api/MesCogi/CogiIslemeAl/" , Method.POST, DataFormat.Json)
              .AddJsonBody(srequest);

            var resp = Globals.ApiClient.Execute<GetCogiIslemeAlResponse>(request);
            return resp.Data;

            //}


        }


        public GetCogiSilResponse CogiIslemSil(CogiSilRequest srequest)
        {
            var request = new RestRequest("api/MesCogi/CogiIslemSil/", Method.POST, DataFormat.Json)
              .AddJsonBody(srequest);

            var resp = Globals.ApiClient.Execute<GetCogiSilResponse>(request);
            return resp.Data;

            //}


        }


    }

}
