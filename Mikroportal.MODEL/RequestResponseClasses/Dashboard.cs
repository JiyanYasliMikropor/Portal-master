using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.RequestResponseClasses
{
    class Dashboard
    {
    }
    public class GetDashboardMenuResponse : ResponseBase
    {
        public IEnumerable<GetDashboardMenu> GetDashboardMenuList { get; set; }
    }


    public class GetDashboardMenu
    {
        public int MenuItemId { get; set; }
        public int? MenuId { get; set; }
        public string MenuItemName { get; set; }
        public string MenuItemDescription { get; set; }
        public string MenuItemUrl { get; set; }
        public string MenuItemIcon { get; set; }
        public int? MenuItemOrderNo { get; set; }
        public string MenuItemClass { get; set; }

        public string MenuItemUrlLocalhost { get; set; }
        public string MenuValueForProjects { get; set; }
        public string MenuItemValueForProjects { get; set; }

    }

}
