using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mikroportal.DATA;
using Mikroportal.MODEL.Entities;
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
    public class AdminAccountService : IAdminAccountService
    {
        public IConfiguration _configuration { get; }
        IMikroportalUOW context;

        private readonly SpDataContext _dbContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public AdminAccountService(IConfiguration configuration, IMikroportalUOW mikroportalContext, IHostingEnvironment hostingEnvironment, SpDataContext dbContext)
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



        public GetPersonelAndRoleListResponse GetPersonelList()
        {
            List<PersonelAndRoleList> list = new List<PersonelAndRoleList>();

            GetPersonelAndRoleListResponse response = new GetPersonelAndRoleListResponse();
            try
            {

               var userRoles = _dbContext.sp_SSH_GetUserRoles.FromSqlInterpolated($"EXECUTE dbo.sp_SSH_GetUserRoles").ToList();

                if (userRoles != null)
                {
                    foreach (var item in userRoles)
                    {
                        PersonelAndRoleList person = new PersonelAndRoleList();
                        person.UserId = Convert.ToInt32(item.UserId);
                        person.UserEmail = item.UserEmail;
                        person.UserName = item.UserName;
                        person.CompanyName = item.CompanyName;
                        person.UserIsActive = item.UserIsActive == true ? true : false;
                        person.TeknikDokuman = item.ACSServiceTekinkDokuman == 1 ? true : false;
                        person.YedekParca = item.ACSServiceYedekParca == 1 ? true : false;
                        list.Add(person);
                    }
                    response.isSuccess = true;
                    response.PersonelAndRoleList = list;
                }
             
            }
            catch (Exception ex )
            {

                response.isSuccess = false;
                response.PersonelAndRoleList = null;
                response.ErrorMessage = "Hata oluştu!! Lütfen tekrar deneyiniz..";
            }


            return response;


        }


        public GetPersonelAndRoleListResponse UserChangeRole(UserRoleChangeView view)
        {
            GetPersonelAndRoleListResponse response = new GetPersonelAndRoleListResponse();

            if(view.Type == "UserIsActive")
            {
                var UserData = context.TblUsersRepository.Get().SingleOrDefault(b => b.UserId == view.UserId);

                UserData.IsActive = view.Durum == 1 ? true : false;
                context.TblUsersRepository.Update(UserData);
                context.Save();
                response.isSuccess = true;
            }

            if (view.Type == "TeknikDokuman")
            {
                if(view.Durum == 0)
                {
                    var userRolesData = from userRoles in context.TblUserRolesRepository.Get()
                                   join role in context.TblRolesRepository.Get()
                                   on userRoles.RoleId equals role.RoleId
                                   where userRoles.UserId == view.UserId && role.RoleName == "ACSServiceTekinkDokuman"
                                        select new
                                   {
                                       userRoles.UserId,
                                       userRoles.UserRoleId,
                                       role.RoleName,
                                   } into selection
                                   select selection;
                    var data = userRolesData.SingleOrDefault();
                    context.TblUserRolesRepository.Delete(data.UserRoleId);
                    context.Save();
                }
                else
                {
                    var RoleData = context.TblRolesRepository.Get().SingleOrDefault(b => b.RoleName == "ACSServiceTekinkDokuman");
                    TblUserRoles newUserRole = new TblUserRoles();
                    newUserRole.UserId = view.UserId;
                    newUserRole.RoleId = RoleData.RoleId;
                    context.TblUserRolesRepository.Insert(newUserRole);
                    context.Save();


                }
            }

            if (view.Type == "YedekParca")
            {
                if (view.Durum == 0)
                {
                    var userRolesData = from userRoles in context.TblUserRolesRepository.Get()
                                        join role in context.TblRolesRepository.Get()
                                        on userRoles.RoleId equals role.RoleId
                                        where userRoles.UserId == view.UserId && role.RoleName == "ACSServiceYedekParca"
                                        select new
                                        {
                                            userRoles.UserId,
                                            userRoles.UserRoleId,
                                            role.RoleName,
                                        } into selection
                                        select selection;
                    var data = userRolesData.SingleOrDefault();
                    context.TblUserRolesRepository.Delete(data.UserRoleId);
                    context.Save();
                }
                else
                {
                    var RoleData = context.TblRolesRepository.Get().SingleOrDefault(b => b.RoleName == "ACSServiceYedekParca");
                    TblUserRoles newUserRole = new TblUserRoles();
                    newUserRole.UserId = view.UserId;
                    newUserRole.RoleId = RoleData.RoleId;
                    context.TblUserRolesRepository.Insert(newUserRole);
                    context.Save();
                }
            }

            return response;
        }
    }
}
