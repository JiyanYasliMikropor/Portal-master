using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mikroportal.DATA;
using Mikroportal.MODEL.Entities;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.MES;
using Mikroportal.MODEL.ServiceContracts.MES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.API.Services.MES
{
    public class ScrapService : IScrapService
    {
        public IConfiguration _configuration { get; }
        IMikroportalUOW context;
        private readonly SpDataContext _dbContext;


        public ScrapService(IConfiguration configuration, IMikroportalUOW mikroportalContext, SpDataContext dbContext)
        {
            _configuration = configuration;
            context = mikroportalContext;
            _dbContext = dbContext;
        }


        public GetAllMachinesResponse GetAllMachines()
        {
            GetAllMachinesResponse response = new GetAllMachinesResponse();

            try
            {
                List<Machines> machineList = context.MachinesRepository.Get().Where(a => a.Ispassive == false).ToList();
                if (machineList != null && machineList.Count > 0)
                {
                    response.isSuccess = true;
                    response.MachineList = machineList;
                }
                else
                {
                    response.isSuccess = false;
                    response.MachineList = null;
                    response.ErrorMessage = "Hata oluştu tekrar deneyiniz!";
                }
            }
            catch (Exception ex)
            {

                response.isSuccess = false;
                response.MachineList = null;
                response.ErrorMessage = "Hata oluştu tekrar deneyiniz!";
            }
            return response;
        }

        public GetAllMudurlukResponse GetAllMudurluk()
        {
            GetAllMudurlukResponse response = new GetAllMudurlukResponse();

            try
            {
                List<TblSapMudurlukler> mudurlukList = context.TblSapMudurluklerRepository.Get().ToList();
                if (mudurlukList != null && mudurlukList.Count > 0)
                {
                    response.isSuccess = true;
                    response.MudurlukList = mudurlukList;
                }
                else
                {
                    response.isSuccess = false;
                    response.MudurlukList = null;
                    response.ErrorMessage = "Hata oluştu tekrar deneyiniz!";
                }
            }
            catch (Exception ex)
            {

                response.isSuccess = false;
                response.MudurlukList = null;
                response.ErrorMessage = "Hata oluştu tekrar deneyiniz!";
            }
            return response;
        }


        public HurdaMaliyetRaporuResponse HurdaMaliyetRaporuGetir(HurdaMaliyetSearchRequest request)
        {
            HurdaMaliyetRaporuResponse resp = new HurdaMaliyetRaporuResponse();

            List<sp_Mes_V2_HurdaMaliyetRaporuGetir> hurdaMaliyet = _dbContext.sp_Mes_V2_HurdaMaliyetRaporuGetir.FromSqlInterpolated($"EXECUTE dbo.sp_Mes_V2_HurdaMaliyetRaporuGetir {request.BaslangicTarihi},{request.BitisTarihi} ,{request.SelectedMakineListesi}, {request.SelectedMudurlukListesi}").ToList();
            //{request.BaslangicTarihi},{request.BitisTarihi} ,

            if (hurdaMaliyet != null)
            {
                resp.HurdaMaliyetRaporList = hurdaMaliyet;
                resp.isSuccess = true;
            }
            else
            {
                resp.isSuccess = false;
                resp.ErrorMessage = "Bir Hata Oluştu! Lütfen Daha Sonra Tekrar Deneyiniz.";
            }
            return resp;
        }

        public HurdaNegatifRaporuResponse HurdaNegatifeDusenlerRaporuGetir(HurdaNegatifSearchRequest request)
        {
            HurdaNegatifRaporuResponse resp = new HurdaNegatifRaporuResponse();

            List<sp_Mes_V2_HurdaNegatifeDusenler> hurdaNegatif = _dbContext.sp_Mes_V2_HurdaNegatifeDusenler.FromSqlInterpolated($"EXECUTE dbo.sp_Mes_V2_HurdaNegatifeDusenler {request.NegatifValue},{request.BaslangicTarihi},{request.BitisTarihi},{request.SelectedMakineListesi},{request.SelectedMudurlukListesi}").ToList();

            if (hurdaNegatif != null)
            {
                resp.HurdaNegatifRaporList = hurdaNegatif;
                resp.isSuccess = true;
            }
            else
            {
                resp.isSuccess = false;
                resp.ErrorMessage = "Bir Hata Oluştu! Lütfen Daha Sonra Tekrar Deneyiniz.";
            }
            return resp;
        }


        public HurdaNegatifDetayResponse HurdaNegatifDetayGetir(HurdaNegatifDetayRequest request)
        {
            HurdaNegatifDetayResponse resp = new HurdaNegatifDetayResponse();

            List<sp_Mes_V2_HurdaNegatifDetayGetir> hurdaNegatifDetay = _dbContext.sp_Mes_V2_HurdaNegatifDetayGetir.FromSqlInterpolated($"EXECUTE dbo.sp_Mes_V2_HurdaNegatifDetayGetir {request.StockId},{request.TrackingId},{request.MachineId}").ToList();


            //context.TblScrapNettingRepository.Get().Where(a => a.StockId == request.StockId && a.TrackingId == request.TrackingId && a.MachineId == request.MachineId).ToList();

            if (hurdaNegatifDetay != null)
            {
                resp.HurdaNegatifDetayList = hurdaNegatifDetay;
                resp.isSuccess = true;
            }
            else
            {
                resp.isSuccess = false;
                resp.ErrorMessage = "Bir Hata Oluştu! Lütfen Daha Sonra Tekrar Deneyiniz.";
            }
            return resp;
        }

        public HurdaNegatiftekiIsEmirleriResponse HurdaNegatiftekiIsEmirleriGetir(HurdaNegatiftekiIsEmirleriRequest request)
        {
            HurdaNegatiftekiIsEmirleriResponse resp = new HurdaNegatiftekiIsEmirleriResponse();

            List<sp_Mes_V2_HurdaNegatiftekiIsEmirleri> hurdaNegatiftekiIsEmirleri = _dbContext.sp_Mes_V2_HurdaNegatiftekiIsEmirleri.FromSqlInterpolated($"EXECUTE dbo.sp_Mes_V2_HurdaNegatiftekiIsEmirleri {request.ScrapNettingId}").ToList();

            if (hurdaNegatiftekiIsEmirleri != null)
            {
                resp.HurdaNegatiftekiIsEmirleriList = hurdaNegatiftekiIsEmirleri;
                resp.isSuccess = true;
            }
            else
            {
                resp.isSuccess = false;
                resp.ErrorMessage = "Bir Hata Oluştu! Lütfen Daha Sonra Tekrar Deneyiniz.";
            }
            return resp;
        }
    }
}
