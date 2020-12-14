using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mikroportal.DATA;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using Mikroportal.MODEL.ServiceContracts.ACS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.API.Services.ACS
{
    public class ReplacementPartService : IReplacementPartService
    {
        public IConfiguration _configuration { get; }
        IMikroportalUOW context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SpDataContext _dbContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ReplacementPartService(IConfiguration configuration, IMikroportalUOW mikroportalContext, IHostingEnvironment hostingEnvironment, SpDataContext dbContext, IHttpContextAccessor iHttpContextAccessor)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            context = mikroportalContext;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = iHttpContextAccessor;
        }

        public GetDashboardMenuResponse GetDashboardMenu(int UserId)
        {
            GetDashboardMenuResponse response = new GetDashboardMenuResponse();
            List<GetDashboardMenu> homeMenu = new List<GetDashboardMenu>();

            var userMenu = from menuItem in context.TblMenuItemsRepository.Get()
                           join userMenuItems in context.TblUserMenuItemRepository.Get()
                           on menuItem.MenuItemId equals userMenuItems.MenuItemId
                           where menuItem.MenuId == 1 && userMenuItems.UserId == UserId
                           select new
                           {
                               menuItem.MenuItemName,
                               menuItem.MenuItemOrderNo,
                               menuItem.MenuItemUrl,
                               menuItem.MenuItemIcon
                           } into selection
                           orderby selection.MenuItemOrderNo ascending
                           select selection;
            if (userMenu != null)
            {
                foreach (var item in userMenu)
                {
                    GetDashboardMenu menu = new GetDashboardMenu();
                    menu.MenuItemName = item.MenuItemName;
                    menu.MenuItemUrl = item.MenuItemUrl;
                    menu.MenuItemIcon = item.MenuItemIcon;
                    homeMenu.Add(menu);
                }
                response.isSuccess = true;
                response.GetDashboardMenuList = homeMenu;
            }
            else
                response.isSuccess = false;

            return response;
        }
        public SerialNoCheckReplacementPartResponse SerialNoCheckReplacementPart(string serialNo)
        {
            SerialNoCheckReplacementPartResponse response = new SerialNoCheckReplacementPartResponse();


            var SerialNumberCheck = _dbContext.sp_SSH_GetSkuBySerial.FromSqlInterpolated($"EXECUTE dbo.sp_SSH_GetSkuBySerial {serialNo}").ToList();
            if (SerialNumberCheck.Count == 0)
            {
                response.PartNo = null;
                response.Path = null;
                response.DocummentType = null;
                response.DocummentRevisionno = null;
                response.ErrorMessage = "Seri numarasını ait makine bulunamadı!";
                response.isSuccess = false;
                return response;
            }
            else
            {

                var GetPartNo = context.TblSshReplacementPartRepository.Get().Where(q => q.IzlemeKoduId == SerialNumberCheck[0].TrackingId && q.StokKoduId == SerialNumberCheck[0].StockId).FirstOrDefault();
                var GetPath = context.TblSettingsRepository.Get().Where(q => q.SettingName == "TechnicalDocPathCoreMikroportal").FirstOrDefault();

                if (GetPartNo == null)
                {
                    response.PartNo = null;
                    response.Path = null;
                    response.ErrorMessage = "Parça bulunamadı.Yetkili servis ile iletişime geçiniz!";
                    response.isSuccess = false;
                    return response;
                }
                else
                {
                    var getDocumment = context.MesEngineeringdrawingRepository.Get().Where(q => q.Code == Convert.ToString(GetPartNo.PartNo)).OrderByDescending(x => x.Revisionno).FirstOrDefault();
                    var getDocummentType = context.MesEngineeringdrawingsubtypeRepository.Get().Where(q => q.Id == 2).FirstOrDefault();

                    if (getDocumment == null)
                    {
                        response.PartNo = null;
                        response.Path = null;
                        response.DocummentType = null;
                        response.DocummentRevisionno = null;
                        response.ErrorMessage = "Parça bulunamadı. Yetkili servis ile iletişime geçiniz!";
                        response.isSuccess = false;
                        return response;
                    }
                    else
                    {
                        response.PartNo = GetPartNo.PartNo;
                        response.Path = GetPath.SettingValue;
                        response.DocummentType = getDocummentType.Name;
                        response.DocummentRevisionno = getDocumment.Revisionno ;

                        response.ErrorMessage = "";
                        response.isSuccess = true;
                        return response;

                    }

                }
            }

        }

        public ModelNoCheckReplacementPartResponse ModelNoCheckReplacementPart(string modelNo)
        {
            ModelNoCheckReplacementPartResponse response = new ModelNoCheckReplacementPartResponse();


            var SerialNumberCheck = _dbContext.sp_SSH_GetTrackingNameByModelNo.FromSqlInterpolated($"EXECUTE dbo.sp_SSH_GetTrackingNameByModelNo {modelNo}").ToList();
            if (SerialNumberCheck.Count == 0)
            {
                response.PartNo = null;
                response.Path = null;
                response.DocummentType = null;
                response.DocummentRevisionno = null;
                response.ErrorMessage = "Moodel ait makine bulunamadı!";
                response.isSuccess = false;
                return response;
            }
            else
            {

                var GetPartNo = context.TblSshReplacementPartRepository.Get().Where(q => q.IzlemeKoduId == SerialNumberCheck[0].TrackingId && q.StokKoduId == SerialNumberCheck[0].StockId).FirstOrDefault();
                var GetPath = context.TblSettingsRepository.Get().Where(q => q.SettingName == "TechnicalDocPathCoreMikroportal").FirstOrDefault();

                if (GetPartNo.PartNo == null)
                {
                    response.PartNo = null;
                    response.Path = null;
                    response.ErrorMessage = "Yetkili servis ile iletişime geçiniz!";
                    response.isSuccess = false;
                    return response;
                }
                else
                {
                    var getDocumment = context.MesEngineeringdrawingRepository.Get().Where(q => q.Code == Convert.ToString(GetPartNo.PartNo)).OrderByDescending(x => x.Revisionno).FirstOrDefault();
                    var getDocummentType = context.MesEngineeringdrawingsubtypeRepository.Get().Where(q => q.Id == 2).FirstOrDefault();

                    if (getDocumment == null)
                    {
                        response.PartNo = null;
                        response.Path = null;
                        response.DocummentType = null;
                        response.DocummentRevisionno = null;
                        response.ErrorMessage = "Yetkili servis ile iletişime geçiniz!";
                        response.isSuccess = false;
                        return response;
                    }
                    else
                    {
                        response.PartNo = GetPartNo.PartNo;
                        response.Path = GetPath.SettingValue;
                        response.DocummentType = getDocummentType.Name;
                        response.DocummentRevisionno = getDocumment.Revisionno;

                        response.ErrorMessage = "";
                        response.isSuccess = true;
                        return response;

                    }

                }
            }

        }

        public GetReplacementPartPriceByPartNoResponse GetReplacementPartPriceByPartNo(string partNo)
        {
            GetReplacementPartPriceByPartNoResponse response = new GetReplacementPartPriceByPartNoResponse();


            var partPriceList = context.TblSshReplacementPartPriceRepository.Get().Where(q => q.PartNo == Convert.ToInt32(partNo)).ToList();
            if (partPriceList.Count == 0)
            {
                response.ErrorMessage = "Part Numarasını Kontrol Ediniz!";
                response.isSuccess = false;
                return response;
            }
            else
            {
                response.PartPriceList = partPriceList;
                response.ErrorMessage = "";
                response.isSuccess = true;
                return response;

            }

        }

    }
}
