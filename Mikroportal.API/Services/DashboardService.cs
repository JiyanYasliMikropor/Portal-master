using Microsoft.Extensions.Configuration;
using Mikroportal.DATA;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.API.Services
{
    public class DashboardService:IDashboardService
    {

        public IConfiguration _configuration { get; }
        IMikroportalUOW context;

        public DashboardService(IConfiguration configuration, IMikroportalUOW mikroportalContext)
        {
            _configuration = configuration;
            context = mikroportalContext;
        }

        public GetDashboardMenuResponse GetDashboardMenu(int UserId)
        {
            GetDashboardMenuResponse response = new GetDashboardMenuResponse();
            List<GetDashboardMenu> homeMenu = new List<GetDashboardMenu>();

            var userMenu = from menuItem in context.TblMenuItemsRepository.Get()
                           join userMenuItems in context.TblUserMenuItemRepository.Get()
                           on menuItem.MenuItemId equals userMenuItems.MenuItemId
                           where menuItem.MenuValueForProjects == "UI" && userMenuItems.UserId == UserId
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

    }
}
