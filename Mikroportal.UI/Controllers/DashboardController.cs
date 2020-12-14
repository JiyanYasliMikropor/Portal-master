using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.ServiceContracts;
using Mikroportal.UI.Models;

namespace Mikroportal.UI.Controllers
{

    //Authorize attribute ile bu sınıfı sadece yetkisi yani tokenı olan kişilerin girmesini söylüyorum.
    [Authorize]
    public class DashboardController : Controller
    {
        IDashboardService _dashboardService;
        private readonly ILogger<DashboardController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer _localizer;

        public DashboardController(ILogger<DashboardController> logger, IDashboardService dashboardService, IHttpContextAccessor iHttpContextAccessor, IStringLocalizer<DashboardController> localizer)
        {
            _logger = logger;
            _dashboardService = dashboardService;
            _httpContextAccessor = iHttpContextAccessor;
            _localizer = localizer;
        }


        public IActionResult Index()
        {

            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];

            GetDashboardMenuResponse response = _dashboardService.GetDashboardMenu(Convert.ToInt32(userId));

            GetDashboardMenuResponse response2 = new GetDashboardMenuResponse();
            response2.ErrorMessage = response.ErrorMessage;
            response2.isSuccess = response.isSuccess;
            List<GetDashboardMenu> GetMenuList = new List<GetDashboardMenu>();

            foreach (var item in response.GetDashboardMenuList)
            {
                GetDashboardMenu menuItem = new GetDashboardMenu();
                menuItem.MenuId = item.MenuId;
                menuItem.MenuItemClass = item.MenuItemClass;
                menuItem.MenuItemDescription = item.MenuItemDescription;
                menuItem.MenuItemIcon = item.MenuItemIcon;
                menuItem.MenuItemId = item.MenuItemId;
                menuItem.MenuItemOrderNo = item.MenuItemOrderNo;
                menuItem.MenuItemUrl = item.MenuItemUrl;
                menuItem.MenuItemUrlLocalhost = item.MenuItemUrlLocalhost;

                if (item.MenuItemValueForProjects == "UIAnasayfa")
                {
                    menuItem.MenuItemName = _localizer["UIAnasayfa"];
                }
                else if (item.MenuItemValueForProjects == "UISSHService" || item.MenuItemValueForProjects == "UISSHAdmin" || item.MenuItemValueForProjects == "UISSHAdminYedekParca")
                {
                    menuItem.MenuItemName = _localizer["UISSH"];
                }
                else if (item.MenuItemValueForProjects == "UIFazlaMesai")
                {
                    menuItem.MenuItemName = _localizer["UIFazlaMesai"];
                }
                else if (item.MenuItemValueForProjects == "UIMes")
                {
                    menuItem.MenuItemName = _localizer["UIMes"];
                }
                else if (item.MenuItemValueForProjects == "UICustomerPortal")
                {
                    menuItem.MenuItemName = _localizer["UICustomerPortal"];
                }
                GetMenuList.Add(menuItem);

            }
            response2.GetDashboardMenuList = GetMenuList;

            ViewBag.UserName = _httpContextAccessor.HttpContext.Request.Cookies["UserName"];
            if (response != null)
                return View(response2);
            else
                return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            
            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}{returnUrl}";

            return Redirect(baseUrl);
        }


    }
}
