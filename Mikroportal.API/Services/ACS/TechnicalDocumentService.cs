using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mikroportal.DATA;
using Mikroportal.MODEL.Entities;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using Mikroportal.MODEL.ServiceContracts.ACS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.API.Services.ACS
{
    public class TechnicalDocumentService :ITechnicalDocumentService
    {
        public IConfiguration _configuration { get; }
        IMikroportalUOW context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SpDataContext _dbContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public TechnicalDocumentService(IConfiguration configuration, IMikroportalUOW mikroportalContext, IHostingEnvironment hostingEnvironment, SpDataContext dbContext, IHttpContextAccessor iHttpContextAccessor)
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

        public TechnicalDocumentResponse SerialNoCheckTechnicalDocument(string serialNo, string UserId)
        {
            TechnicalDocumentResponse response = new TechnicalDocumentResponse();

            List<sp_SSH_GetSkuBySerial> machineDetail = _dbContext.sp_SSH_GetSkuBySerial.FromSqlInterpolated($"EXECUTE dbo.sp_SSH_GetSkuBySerial {serialNo}").ToList();
            //var GetPath = context.TblSettingsRepository.Get().Where(q => q.SettingName == "TechnicalDocumentPath").FirstOrDefault();

            if (machineDetail != null && machineDetail.Count > 0)
            {

                bool machineControl = context.TblSshMachinesRepository.Get().Where(a => a.SerialNo == serialNo && a.CreatedBy == Convert.ToInt32(UserId)).Any();


                if (machineControl == true)
                {
                    var skuName = machineDetail[0].Sku;
                    List<sp_SSH_GetTechnicalDocument> documentListesi = _dbContext.sp_SSH_GetTechnicalDocument.FromSqlInterpolated($"EXECUTE dbo.sp_SSH_GetTechnicalDocument {skuName}").ToList();
                    response.ErrorMessage = null;
                    response.DocumentListesi = documentListesi;
                    response.MachineDetail = machineDetail;
                    response.isSuccess = true;
                    response.isModal = false;
                    //response.Path = GetPath.SettingValue;

                }
                else
                {
                    response.DocumentListesi = null;
                    response.MachineDetail = null;
                    response.ErrorMessage = "Kurulum Yapılmamış!";
                    response.isSuccess = true;
                    response.isModal = true;
                    response.Path = null;

                }

            }
            else
            {
                //response.DocumentListesi = null;
                //response.MachineDetail = null;
                response.isModal = false;
                response.ErrorMessage = "Seri numarası kontrol ediniz!";
                response.isSuccess = false;
                response.Path = null;
            }
            return response;
        }

    }
}
