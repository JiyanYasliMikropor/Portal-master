using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Mikroportal.DATA;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using Mikroportal.MODEL.RequestResponseClasses.ACS.Admin;
using Mikroportal.MODEL.ServiceContracts.ACS.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.API.Services.ACS.Admin
{
    public class AdminHomeService : IAdminHomeService
    {
        public IConfiguration _configuration { get; }
        IMikroportalUOW context;

        private readonly SpDataContext _dbContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public AdminHomeService(IConfiguration configuration, IMikroportalUOW mikroportalContext, IHostingEnvironment hostingEnvironment, SpDataContext dbContext)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            context = mikroportalContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public GetInboxCountResponse GetInboxCount()
        {
            GetInboxCountResponse response = new GetInboxCountResponse();

            var ReadFlagsNothing = context.TblSshMachineryServicesRepository.Get().Where(q => q.IsApproved == false && q.IsRejected == false);
            var ReadFlagsNothingCount = ReadFlagsNothing.Count();

            response.ReadFlagsNothingCount = ReadFlagsNothingCount;
            response.isSuccess = true;

            return response;

        }

        public GetInboxListResponse GetInboxList(SearchRequest searchRequest)
        {
            GetInboxListResponse response = new GetInboxListResponse();

            var ReadFlagsNothing = context.TblSshMachineryServicesRepository.Get().Where(q => q.IsApproved == false && q.IsRejected == false);
            var ReadFlagsNothingCount = ReadFlagsNothing.Count();

            if(searchRequest.search == null)
            {
                searchRequest.search = "";
            }
            var inboxList = _dbContext.vSSHMachineHistory.Where(q => q.IsApproved == false && q.IsRejected == false && q.SerialNo.Contains(searchRequest.search)).ToList();


            response.ReadFlagsNothingCount = ReadFlagsNothingCount;
            response.inboxList = inboxList;
            response.isSuccess = true;
            return response;
        }

        public InboxShowByIdResponse InboxShowById(string machineryServiceId)
        {
            InboxShowByIdResponse response = new InboxShowByIdResponse();

            var serviceData = _dbContext.vSSHMachineHistory.Where(b => b.MachineryServiceId == Convert.ToInt32(machineryServiceId)).ToList();
            response.ServiceData = serviceData;
            response.isSuccess = true;

            return response;
        }
        public TblSshMachineryServicesResponse IsApproved(string machineryServiceId)
        {
            TblSshMachineryServicesResponse response = new TblSshMachineryServicesResponse();

            var machineryService = context.TblSshMachineryServicesRepository.Get().SingleOrDefault(b => b.MachineryServiceId == Convert.ToInt32(machineryServiceId));

            if (machineryService != null)
            {
                machineryService.IsApproved = true;

                context.TblSshMachineryServicesRepository.Update(machineryService);
                context.Save();
                response.isSuccess = true;
            }
            else
            {
                response.ErrorMessage = "Hata oluştu!! Lütfen tekrar deneyiniz..";
                response.isSuccess = false;
            }

            return response;
        }


        public TblSshMachineryServicesResponse IsRejected(RejectedRequest rejectedRequest)
        {
            TblSshMachineryServicesResponse response = new TblSshMachineryServicesResponse();

            var machineryService = context.TblSshMachineryServicesRepository.Get().SingleOrDefault(b => b.MachineryServiceId == Convert.ToInt32(rejectedRequest.machineryServiceId));

            if (machineryService != null)
            {
                machineryService.IsRejected = true;
                machineryService.IsRejectedComment = rejectedRequest.isRejectedComment;

                context.TblSshMachineryServicesRepository.Update(machineryService);
                context.Save();
                response.isSuccess = true;
            }
            else
            {
                response.ErrorMessage = "Hata oluştu!! Lütfen tekrar deneyiniz..";
                response.isSuccess = false;
            }

            return response;
        }
        public GetDashboardMenuResponse GetACSAdminMenu(int UserId)
        {
            GetDashboardMenuResponse response = new GetDashboardMenuResponse();
            List<GetDashboardMenu> homeMenu = new List<GetDashboardMenu>();

            var userMenu = from menuItem in context.TblMenuItemsRepository.Get()
                           join userMenuItems in context.TblUserMenuItemRepository.Get()
                           on menuItem.MenuItemId equals userMenuItems.MenuItemId
                           where menuItem.MenuValueForProjects == "SSHAdmin" && userMenuItems.UserId == UserId
                           select new
                           {
                               menuItem.MenuItemName,
                               menuItem.MenuItemOrderNo,
                               menuItem.MenuItemUrl,
                               menuItem.MenuItemIcon,
                               menuItem.MenuItemId,
                               menuItem.MenuItemUrlLocalhost,
                               menuItem.MenuItemValueForProjects
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
                    menu.MenuItemId = item.MenuItemId;
                    menu.MenuItemUrlLocalhost = item.MenuItemUrlLocalhost;
                    menu.MenuItemValueForProjects = item.MenuItemValueForProjects;
                    homeMenu.Add(menu);
                }
                response.isSuccess = true;
                response.GetDashboardMenuList = homeMenu;
            }
            else
                response.isSuccess = false;

            return response;
        }
        //public InboxShowByIdResponse InboxShowById(string machineryServiceId)
        //{
        //    InboxShowByIdResponse response = new InboxShowByIdResponse();

        //    var serviceData = _dbContext.vSSHMachineHistory.Where(b => b.MachineryServiceId == Convert.ToInt32(machineryServiceId)).ToList();
        //    response.ServiceData = serviceData;
        //    response.isSuccess = true;

        //    return response;
        //}

    }
}
