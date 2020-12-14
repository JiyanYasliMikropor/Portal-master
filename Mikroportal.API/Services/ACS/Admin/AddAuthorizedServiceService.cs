using Microsoft.AspNetCore.Hosting;
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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mikroportal.API.Services.ACS.Admin
{
    public class AddAuthorizedServiceService : IAddAuthorizedServiceService
    {
        public IConfiguration _configuration { get; }
        IMikroportalUOW context;

        private readonly SpDataContext _dbContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public AddAuthorizedServiceService(IConfiguration configuration, IMikroportalUOW mikroportalContext, IHostingEnvironment hostingEnvironment, SpDataContext dbContext)
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

        public AddAuthorizedServiceResponse AddService(AddAuthorizedService view)
        {
            string userId = view.UserId;
            AddAuthorizedServiceResponse response = new AddAuthorizedServiceResponse();

            if (view.Email == null)
            {
                response.isSuccess = false;
                response.ErrorMessage = "Email alanı zorunludur!";

            }
            else
            {

                try
                {
                    RandomPasswordGenerator randomPasswordGenerator = new RandomPasswordGenerator();

                    TblUsers userData = new TblUsers();
                    userData.UserAccount = view.Email;
                    userData.UserEmail = view.Email;
                    userData.CompanyName = view.CompanyName;
                    userData.UserName = view.UserName + ' ' + view.UserLastName;
                    userData.CreatedDate = DateTime.Now;
                    userData.IsActive = true;
                    var randomPassword = randomPasswordGenerator.GeneratePassword(true, true, true, true, 8);

                    var hashedPass = CreatePasswordHash(randomPassword);

                    userData.UserPassword = hashedPass;

                    bool UserKaydedildiMi = context.TblUsersRepository.Insert(userData);
                    context.Save();

                    if (UserKaydedildiMi == true)
                    {
                        var getRol = context.TblRolesRepository.Get().SingleOrDefault(a => a.RoleName == "ACSService");

                        TblUserRoles userRolesData = new TblUserRoles();
                        userRolesData.RoleId = getRol.RoleId;
                        userRolesData.UserId = userData.UserId;
                        bool UserRoleKaydedildiMi = context.TblUserRolesRepository.Insert(userRolesData);
                        context.Save();

                        if (UserRoleKaydedildiMi == true)
                        {
                            var getMenu = context.TblMenusRepository.Get().SingleOrDefault(a => a.MenuName == "Anasayfa");
                            var getMenuItems = context.TblMenuItemsRepository.Get().Where(a => a.MenuId == getMenu.MenuId).ToList();

                            foreach (var item in getMenuItems)
                            {
                                if (item.MenuItemDescription == "Anasayfa" || item.MenuItemDescription == "Service")
                                {
                                    TblUserMenuItem userMenuItemData = new TblUserMenuItem();
                                    userMenuItemData.UserId = userData.UserId;
                                    userMenuItemData.MenuItemId = item.MenuItemId;
                                    bool UserMenuItemKaydedildiMi = context.TblUserMenuItemRepository.Insert(userMenuItemData);
                                    context.Save();
                                    if (UserMenuItemKaydedildiMi == false)
                                    {
                                        //Rol atamada bir sorun oluşmuş ise oluşturulan kullanıcı kaldırılıyor.
                                        context.TblUsersRepository.Delete(userData.UserId);
                                        //Menu atamada bir sorun oluşmuş ise oluşturulan menu kaldırılıyor.
                                        context.TblUsersRepository.Delete(userMenuItemData.UserMenuItemId);
                                        response.isSuccess = false;
                                        response.ErrorMessage = "Hata oluştu! Lütfen tekrar deneyiniz..";
                                        return response;
                                    }

                                }

                            }


                            Tools tls = new Tools();
                            string body = "<style>*{margin:0;padding:0;font-family:'HelveticaNeue','Helvetica',Helvetica,Arial,sans-serif;box-sizing:border-box;font-size:14px;}img{max-width:100%;}body{-webkit-font-smoothing:antialiased;-webkit-text-size-adjust:none;width:100%!important;height:100%;line-height:1.6;}/*Let'smakesurealltableshavedefaults*/tabletd{vertical-align:top;}body{background-color:#f6f6f6;}.body-wrap{background-color:#f6f6f6;width:100%;}.container{display:block!important;max-width:600px!important;margin:0auto!important;/*makesitcentered*/clear:both!important;}.content{max-width:600px;margin:0auto;display:block;padding:20px;}.main{background:#fff;border:1pxsolid#e9e9e9;border-radius:3px;}.content-wrap{padding:20px;}.content-block{padding:0020px;}.header{width:100%;margin-bottom:20px;}.footer{width:100%;clear:both;color:#999;padding:20px;}.footera{color:#999;}.footerp,.footera,.footerunsubscribe,.footertd{font-size:12px;}h1,h2,h3{font-family:'HelveticaNeue',Helvetica,Arial,'LucidaGrande',sans-serif;color:#000;margin:40px00;line-height:1.2;font-weight:400;}h1{font-size:32px;font-weight:500;}h2{font-size:24px;}h3{font-size:18px;}h4{font-size:14px;font-weight:600;}p,ul,ol{margin-bottom:10px;font-weight:normal;}pli,ulli,olli{margin-left:5px;list-style-position:inside;}a{color:#1ab394;text-decoration:underline;}.btn-primary{text-decoration:none;color:#FFF;background-color:#1ab394;border:solid#1ab394;border-width:5px10px;line-height:2;font-weight:bold;text-align:center;cursor:pointer;display:inline-block;border-radius:5px;text-transform:capitalize;}.last{margin-bottom:0;}.first{margin-top:0;}.aligncenter{text-align:center;}.alignright{text-align:right;}.alignleft{text-align:left;}.clear{clear:both;}.alert{font-size:16px;color:#fff;font-weight:500;padding:20px;text-align:center;border-radius:3px3px00;}.alerta{color:#fff;text-decoration:none;font-weight:500;font-size:16px;}.alert.alert-warning{background:#f8ac59;}.alert.alert-bad{background:#ed5565;}.alert.alert-good{background:#1ab394;}.invoice{margin:40pxauto;text-align:left;width:80%;}.invoicetd{padding:5px0;}.invoice.invoice-items{width:100%;}.invoice.invoice-itemstd{border-top:#eee1pxsolid;}.invoice.invoice-items.totaltd{border-top:2pxsolid#333;border-bottom:2pxsolid#333;font-weight:700;}@mediaonlyscreenand(max-width:640px){h1,h2,h3,h4{font-weight:600!important;margin:20px05px!important;}h1{font-size:22px!important;}h2{font-size:18px!important;}h3{font-size:16px!important;}.container{width:100%!important;}.content,.content-wrap{padding:10px!important;}.invoice{width:100%!important;}}</style>" +
            "<table class='body-wrap'>" +
                        "<tr>" +
                            "<td></td>" +
                            "<td class='container' width='600'>" +
                                "<div class='content'>" +
                                    "<table class='main' width='100%' cellpadding='0' cellspacing='0'>" +
                                        "<tr>" +
                                            "<td class='content-wrap'>" +
                                                "<table cellpadding = '0' cellspacing='0'>" +
                                                    "<tr>" +
                                                        "<td  style='background: #2f3236; padding: 30px;text-align: center; '>" +
                                                            "<img class='img-responsive' src='https://mikropor.com.tr/assets/img/mikropor-main_145x84.png'/>" +
                                                        "</td>" +
                                                    "</tr>" +
                                                    "<tr>" +
                                                        "<td class='content-block'>" +
                                                            "<h3>Satış Sonrası Hizmetlere Hoş Geldiniz.</h3>" +
                                                        "</td>" +
                                                    "</tr>" +
                                                    "<tr>" +
                                                        "<td class='content-block'>" +
                                                           "Sayın " + userData.UserName + ",<br><br>" +
                                                           "Kullanıcı adı ve şifre bilgileriniz aşığıda paylaşılmıştır.Giriş yaptıktan sonra lütfen şifrenizi değiştiriniz." +
                                                          "<br><br>" +
                                                           "<strong>Email:</strong> " + userData.UserEmail + " <br>" +
                                                           "<strong>Şifre:</strong> " + randomPassword + " <br>" +
                                                        "</td>" +
                                                    "</tr>" +
                                                    "<tr>" +
                                                        "<td class='content-block aligncenter'>" +
                                                            "<a href = '#' class='btn-primary'>Giriş Yap</a>" +
                                                        "</td>" +
                                                    "</tr>" +
                                                  "</table>" +
                                            "</td>" +
                                        "</tr>" +
                                    "</table>" +
                                    "<div class='footer'>" +
                                        "<table width = '100%' >" +
                                            "<tr>" +
                                                "<td class='aligncenter content-block'>© Copyright MIKROPOR.All Rights Reserved</td>" +
                                            "</tr>" +
                                        "</table>" +
                                    "</div></div>" +
                            "</td>" +
                            "<td></td>" +
                        "</tr>" +
                    "</table>";
                            var aliciList = "yunus.yigit@mikropor.com";

                            string sonuc = tls.SendMail(aliciList, body, "Satış Sonrası Hizmetler");

                            if (!string.IsNullOrEmpty(sonuc))
                            {
                                TblSshMailExceptions exc = new TblSshMailExceptions();
                                exc.CreateDate = DateTime.Now;
                                exc.ExceptionDetail = sonuc;
                                exc.Subject = "Yetersiz Stok";
                                exc.Body = body;
                                bool TblSshMailKaydedildiMi = context.TblSshMailExceptionsRepository.Insert(exc);
                                context.Save();
                                response.isSuccess = true;
                                response.ErrorMessage = "";
                                return response;
                            }

                        }
                        else
                        {
                            //Rol atamada bir sorun oluşmuş ise oluşturulan kullanıcı kaldırılıyor.
                            context.TblUsersRepository.Delete(userData.UserId);
                            response.isSuccess = false;
                            response.ErrorMessage = "Hata oluştu! Lütfen tekrar deneyiniz..";
                            return response;
                        }

                    }
                    else
                    {
                        response.isSuccess = false;
                        response.ErrorMessage = "Hata oluştu! Lütfen tekrar deneyiniz..";
                        return response;
                    }


                }
                catch (Exception ex)
                {
                    response.isSuccess = false;
                    response.ErrorMessage = "Hata oluştu! Lütfen tekrar deneyiniz..";
                }

            }

            return response;
        }
        private string CreatePasswordHash(string password)
        {
            var salt = _configuration.GetSection("Salt").Value;
            var provider = new SHA1CryptoServiceProvider();
            var encoding = new UnicodeEncoding();
            var passBytes = provider.ComputeHash(encoding.GetBytes(password + salt));
            password = Convert.ToBase64String(passBytes);
            return password;
        }
    }
}
