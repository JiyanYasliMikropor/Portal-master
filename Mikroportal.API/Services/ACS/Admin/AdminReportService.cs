using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Mikroportal.DATA;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using Mikroportal.MODEL.ServiceContracts.ACS.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.API.Services.ACS.Admin
{
    public class AdminReportService: IAdminReportService
    {
        public IConfiguration _configuration { get; }
        IMikroportalUOW context;

        private readonly SpDataContext _dbContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public AdminReportService(IConfiguration configuration, IMikroportalUOW mikroportalContext, IHostingEnvironment hostingEnvironment, SpDataContext dbContext)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            context = mikroportalContext;
            _hostingEnvironment = hostingEnvironment;
        }
        public InboxShowByIdResponse InboxShowById(string machineryServiceId)
        {
            InboxShowByIdResponse response = new InboxShowByIdResponse();

            var serviceData = _dbContext.vSSHMachineHistory.Where(b => b.MachineryServiceId == Convert.ToInt32(machineryServiceId)).ToList();
            response.ServiceData = serviceData;
            response.isSuccess = true;

            return response;
        }

        public GetMachineHistoryListResponse GetMachineHistoryList(ReportFilter request)
        {
            request.searchStartDate = request.searchStartDate == DateTime.MinValue ? DateTime.MinValue : request.searchStartDate;
            request.searchEndDate = request.searchEndDate == DateTime.MinValue ? DateTime.MaxValue : request.searchEndDate;
            var ReadFlagsNothing = context.TblSshMachineryServicesRepository.Get().Where(q => q.IsApproved == false && q.IsRejected == false);
            var ReadFlagsNothingCount = ReadFlagsNothing.Count();

            var getInstallationList = _dbContext.vSSHMachineHistory.Where(q => q.SerialNo.Contains(request.searchSerialNo == null ? "": request.searchSerialNo) && q.SkuCode.Contains(request.searchSkuCode == null ? "" : request.searchSkuCode)
                && q.TracingName.Contains(request.searchTracingName == null ? "" : request.searchTracingName) && q.ServiceStatus.Contains(request.searchStatus == null ? "" : request.searchStatus) && q.ServiceName.Contains(request.searchServicesType == null ? "" : request.searchServicesType)
              && (q.CreatedDate >= request.searchStartDate && q.CreatedDate <= request.searchEndDate) && q.ServiceStatus != "Beklemede"
               ).ToList();

            GetMachineHistoryListResponse response = new GetMachineHistoryListResponse();
            response.getInstallationList = getInstallationList;
            response.ReadFlagsNothingCount = ReadFlagsNothingCount;
            response.isSuccess = true;

            return response;
        }

        public GetDashboardMenuResponse GetACSAdminMenu(int UserId)
        {
            GetDashboardMenuResponse response = new GetDashboardMenuResponse();
            List<GetDashboardMenu> homeMenu = new List<GetDashboardMenu>();

            var userMenu = from menuItem in context.TblMenuItemsRepository.Get()
                           join userMenuItems in context.TblUserMenuItemRepository.Get()
                           on menuItem.MenuItemId equals userMenuItems.MenuItemId
                           where menuItem.MenuId == 3 && userMenuItems.UserId == UserId
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

        public AddFileUploadMachineHistoryResponse AddFileUploadMachineHistory(FileView request)
        {

            AddFileUploadMachineHistoryResponse response = new AddFileUploadMachineHistoryResponse();
            var MachineryServices = context.TblSshMachineryServicesRepository.Get().SingleOrDefault(q => q.MachineryServiceId == request.MachineryServiceId);

            string fileName = "";
            if (MachineryServices.FileNames == "")
            {
                fileName = "";
            }
            else
            {
                fileName = "||";
            }

            MachineryServices.FileNames = MachineryServices.FileNames + fileName + request.FileNames;

            bool kaydedildiMi = context.TblSshMachineryServicesRepository.Update(MachineryServices);
            context.Save();


            if (kaydedildiMi == false)
            {
                response.isSuccess = false;
                response.ErrorMessage = "Hata oluştu!! Kurulum yapılamadı.. Lütfen tekrar deneyiniz..";

            }
            else
            {
                response.isSuccess = true;
                response.ErrorMessage = "Doküman eklendi..";

            }

            return response;
        }
    }
}
