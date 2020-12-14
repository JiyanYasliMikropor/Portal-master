using Microsoft.EntityFrameworkCore;
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
    public class AndonService:IAndonService
    {
        private readonly SpDataContext _dbContext;
        public AndonService( SpDataContext dbContext)
        {
            _dbContext = dbContext;           
        }      

        public GetAndonListResponse GetAndonList()
        {
            GetAndonListResponse response = new GetAndonListResponse();
            try
            {               
                var andonList = _dbContext.MSP_ANDON_GETPRODUCTIONLINE_SERIAL.FromSqlInterpolated($"EXECUTE dbo.MSP_ANDON_GETPRODUCTIONLINE_SERIAL {null},{null},{null},{null}").ToList();
                if (andonList != null && andonList.Count > 0)
                {
                    response.isSuccess = true;
                    response.andonList = andonList;
                }
                else
                {
                    response.isSuccess = false;
                    response.andonList = null;
                    response.ErrorMessage = "Hata oluştu tekrar deneyiniz!";
                }
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.andonList = null;
                response.ErrorMessage = "Hata oluştu tekrar deneyiniz!";
            }
            return response;

        }
    }
}
