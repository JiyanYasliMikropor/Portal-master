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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.API.Services.ACS
{
    public class CallCenterService : ICallCenterService
    {
        public IConfiguration _configuration { get; }
        IMikroportalUOW context;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SpDataContext _dbContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public CallCenterService(IConfiguration configuration, IMikroportalUOW mikroportalContext, IHostingEnvironment hostingEnvironment, SpDataContext dbContext, IHttpContextAccessor iHttpContextAccessor)
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

        public TblSshMachineryServicesResponse AddCallCenter(CallCenterView view)
        {
            TblSshMachineryServicesResponse response = new TblSshMachineryServicesResponse();
            string userId = view.UserId;

            TblSshMachineryServices machinery = new TblSshMachineryServices();
            var machineDetail = _dbContext.sp_SSH_GetSkuBySerial.FromSqlInterpolated($"EXECUTE dbo.sp_SSH_GetSkuBySerial {view.SerialNo}").ToList();
            if (machineDetail.Count > 0)
            {
                view.SkuCode = machineDetail[0].Sku;
                view.TracingName = Convert.ToString(machineDetail[0].SkuId);
            }


            machinery.SkuCode = view.SkuCode;
            machinery.TracingName = view.TracingName;
            machinery.SerialNo = view.SerialNo;
            machinery.CallCenterComment = view.CallCenterComment;
            machinery.CompressorInformation = view.CompressorInformation;
            machinery.RelatedPersonName = view.RelatedPersonName;
            machinery.CompanyName = view.CompanyName;
            machinery.RelatedPersonLastName = view.RelatedPersonLastName;
            machinery.RelatedPersonEmail = view.RelatedPersonEmail;
            machinery.RelatedPersonPhone = view.RelatedPersonPhone;
            machinery.ReadFlags = false;
            machinery.IsApproved = false;
            machinery.IsRejected = false;
            machinery.CreatedBy = Convert.ToInt32(userId);
            machinery.CreatedDate = DateTime.Now;
            machinery.ServiceTypeId = 4;
            machinery.FileNames = view.FileNames;


            bool kaydedildiMi = context.TblSshMachineryServicesRepository.Insert(machinery);
            context.Save();
            if (kaydedildiMi == false)
            {
                response.isSuccess = false;
                response.ErrorMessage = "Hata oluştu!! Lütfen tekrar deneyiniz..";
                return response;
            }
            else
            {
                response.isSuccess = true;
                response.ErrorMessage = "";
                return response;
            }


        }



    }
}
