using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mikroportal.DATA;
using Mikroportal.MODEL.Entities;
using Mikroportal.MODEL.RequestResponseClasses.MES;
using Mikroportal.MODEL.ServiceContracts.MES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.API.Services.MES
{
    public class MesCogiService : IMesCogiService
    {
        public IConfiguration _configuration { get; }
        IMikroportalUOW context;
        private readonly SpDataContext _dbContext;


        public MesCogiService(IConfiguration configuration, IMikroportalUOW mikroportalContext, SpDataContext dbContext)
        {
            _configuration = configuration;
            context = mikroportalContext;
            _dbContext = dbContext;
        }


        public GetMesCogiResponse BildirimleriGoster(int UserId)
        {
            GetMesCogiResponse resp = new GetMesCogiResponse();


            List<sp_mkrprtl_Mes_MesCogiList> mesCogiList = _dbContext.sp_mkrprtl_Mes_MesCogiList.FromSqlInterpolated($"EXECUTE dbo.sp_mkrprtl_Mes_MesCogiList {UserId}").ToList();


            if (mesCogiList != null)
            {
                resp.MesCogiList = mesCogiList;
                resp.isSuccess = true;
            }
            else
            {
                resp.isSuccess = false;
                resp.ErrorMessage = "Bir Hata Oluştu! Lütfen Daha Sonra Tekrar Deneyiniz.";
            }
            return resp;
        }




        public GetCogiIslemeAlResponse CogiIslemeAl(CogiIslemeAlRequest request)
        {
            GetCogiIslemeAlResponse resp = new GetCogiIslemeAlResponse();


            List<sp_mkrprtl_Mes_MesCogiTamamlandi> CogiİslemeAlList = _dbContext.sp_mkrprtl_Mes_MesCogiTamamlandi.FromSqlInterpolated($"EXECUTE dbo.sp_mkrprtl_Mes_MesCogiTamamlandi {request.CogiId},{request.CompletedBy}").ToList();
          


            if (CogiİslemeAlList != null)
            {
                resp.CogiİslemeAlList = CogiİslemeAlList;
                resp.isSuccess = true;
            }
            else
            {
                resp.isSuccess = false;
                resp.ErrorMessage = "Bir Hata Oluştu! Lütfen Daha Sonra Tekrar Deneyiniz.";
            }
            return resp;
        }



        public GetCogiSilResponse CogiIslemSil(CogiSilRequest request)
        {
            GetCogiSilResponse resp = new GetCogiSilResponse();


            List<sp_mkrprtl_Mes_MesCogiSil> CogiSilList = _dbContext.sp_mkrprtl_Mes_MesCogiSil.FromSqlInterpolated($"EXECUTE dbo.sp_mkrprtl_Mes_MesCogiSil {request.CogiId},{request.CanceledBy}").ToList();



            if (CogiSilList != null)
            {
                resp.CogiSilList = CogiSilList;
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
