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
    public class ScrapService : IScrapService
    {
        IConfiguration _config;
        public ScrapService(IConfiguration config)
        {
            _config = config;
        }


        public HurdaMaliyetRaporuResponse HurdaMaliyetRaporuGetir(HurdaMaliyetSearchRequest Srequest)
        {
            var request = new RestRequest("api/Scrap/HurdaMaliyetRaporuGetir", Method.POST, DataFormat.Json)
              .AddJsonBody(Srequest);

            var resp = Globals.ApiClient.Execute<HurdaMaliyetRaporuResponse>(request);
            return resp.Data;
        }

        public HurdaNegatifRaporuResponse HurdaNegatifeDusenlerRaporuGetir(HurdaNegatifSearchRequest Srequest)
        {
            var request = new RestRequest("api/Scrap/HurdaNegatifeDusenlerRaporuGetir", Method.POST, DataFormat.Json)
              .AddJsonBody(Srequest);

            var resp = Globals.ApiClient.Execute<HurdaNegatifRaporuResponse>(request);
            return resp.Data;
        }

        public HurdaNegatifDetayResponse HurdaNegatifDetayGetir(HurdaNegatifDetayRequest Srequest)
        {
            var request = new RestRequest("api/Scrap/HurdaNegatifDetayGetir", Method.POST, DataFormat.Json)
              .AddJsonBody(Srequest);

            var resp = Globals.ApiClient.Execute<HurdaNegatifDetayResponse>(request);
            return resp.Data;
        }

     public HurdaNegatiftekiIsEmirleriResponse HurdaNegatiftekiIsEmirleriGetir(HurdaNegatiftekiIsEmirleriRequest Srequest)
        {
            var request = new RestRequest("api/Scrap/HurdaNegatiftekiIsEmirleriGetir", Method.POST, DataFormat.Json)
              .AddJsonBody(Srequest);

            var resp = Globals.ApiClient.Execute<HurdaNegatiftekiIsEmirleriResponse>(request);
            return resp.Data;
        }


        public GetAllMachinesResponse GetAllMachines()
        {
            var request = new RestRequest("api/Scrap/GetAllMachines/", Method.GET, DataFormat.Json);

            var resp = Globals.ApiClient.Execute<GetAllMachinesResponse>(request);
            return resp.Data;
        }

        public GetAllMudurlukResponse GetAllMudurluk()
        {
            var request = new RestRequest("api/Scrap/GetAllMudurluk/", Method.GET, DataFormat.Json);

            var resp = Globals.ApiClient.Execute<GetAllMudurlukResponse>(request);
            return resp.Data;
        }

    }
}
