using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mikroportal.DATA;
using Mikroportal.MODEL.Entities;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using Mikroportal.MODEL.ServiceContracts.ACS.Admin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.API.Services.ACS.Admin
{
    public class AddGuaranteeService: IAddGuaranteeService
    {
        public IConfiguration _configuration { get; }
        IMikroportalUOW context;

        private readonly SpDataContext _dbContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public AddGuaranteeService(IConfiguration configuration, IMikroportalUOW mikroportalContext, IHostingEnvironment hostingEnvironment, SpDataContext dbContext)
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
        public GetSkuBySerialResponse SerialNoCheckGuarantee(string serialNo)
        {
            GetSkuBySerialResponse response = new GetSkuBySerialResponse();

            List<sp_SSH_GetSkuBySerial> machineDetail = _dbContext.sp_SSH_GetSkuBySerial.FromSqlInterpolated($"EXECUTE dbo.sp_SSH_GetSkuBySerial {serialNo}").ToList();

            if (machineDetail != null && machineDetail.Count > 0)
            {
                bool machineControl = context.TblSshMachinesRepository.Get().Where(a => a.SerialNo == serialNo).Any();

                if (machineControl == true)
                {
                    response.MachineDetail = null;
                    response.isModal = false;
                    response.isSuccess = true;
                    response.ErrorMessage = "";
                    return response;
                }
                else
                {
                    response.MachineDetail = machineDetail;
                    response.isSuccess = true;
                    response.isModal = false;
                    response.ErrorMessage = "";
                    return response;
                }

            }
            else
            {
                response.ErrorMessage = "Seri numarası kontrol ediniz!";
                response.isSuccess = false;
                response.isModal = false;
                response.MachineDetail = null;
                return response;

            }
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


        public TblSshMachineryServicesResponse AddGuarantee(GuaranteeView view)
        {
            string userId = view.UserId;
            TblSshMachineryServicesResponse response = new TblSshMachineryServicesResponse();


            var machineControl = context.TblSshMachinesRepository.Get().SingleOrDefault(a => a.SerialNo == view.SerialNo);

            if (machineControl == null)
            {
                response.ErrorMessage = view.SerialNo + " " + "Seri numarasına ait makine kurulumu bulunmaktadır.";
                response.isSuccess = false;

            }
            else
            {
                try
                {
                    var machineDetail = _dbContext.sp_SSH_GetSkuBySerial.FromSqlInterpolated($"EXECUTE dbo.sp_SSH_GetSkuBySerial {view.SerialNo}").ToList();
                    view.SkuCode = machineDetail[0].Sku;
                    view.TracingName = Convert.ToString(machineDetail[0].SkuId);

                    TblSshMachineryServices machineryServices = new TblSshMachineryServices();

                    //makine hizmetleri tablosunu kurulum olarak kaydediyoruz
                    machineryServices.MachineId = machineControl.MachineId;
                    machineryServices.SkuCode = view.SkuCode;
                    machineryServices.SerialNo = view.SerialNo;
                    machineryServices.TracingName = view.TracingName;
                    machineryServices.GuaranteeComment = view.Comment;
                    machineryServices.GuaranteeStartDate = DateTime.ParseExact(view.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    machineryServices.GuaranteeEndDate = DateTime.ParseExact(view.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    machineryServices.IsApproved = true;
                    machineryServices.IsRejected = false;
                    machineryServices.ReadFlags = false;
                    machineryServices.ServiceTypeId = 5;
                    machineryServices.FileNames = view.FileNames;
                    machineryServices.CreatedBy = Convert.ToInt32(userId);
                    machineryServices.CreatedDate = DateTime.Now;

                    bool kaydedildiMi = context.TblSshMachineryServicesRepository.Insert(machineryServices);
                    context.Save();


                    if (kaydedildiMi == false)
                    {
                        response.isSuccess = false;
                        response.ErrorMessage = "Hata oluştu! Lütfen tekrar deneyiniz..";
                    }
                    else
                    {
                        response.isSuccess = true;
                        response.ErrorMessage = "";

                    }
                }
                catch (Exception)
                {
                    response.isSuccess = false;
                    response.ErrorMessage = "Hata oluştu! Lütfen tekrar deneyiniz..";

                }
            }
            return response;
        }

        public TblSshMachineryServicesResponse AddBanGuarantee(GuaranteeView view)
        {
            string userId = view.UserId;
            TblSshMachineryServicesResponse response = new TblSshMachineryServicesResponse();


            var machineControl = context.TblSshMachinesRepository.Get().SingleOrDefault(a => a.SerialNo == view.SerialNo);

            if (machineControl == null)
            {
                response.ErrorMessage = view.SerialNo + " " + "Seri numarasına ait makine kurulumu bulunmaktadır.";
                response.isSuccess = false;

            }
            else
            {
                try
                {
                    var machineDetail = _dbContext.sp_SSH_GetSkuBySerial.FromSqlInterpolated($"EXECUTE dbo.sp_SSH_GetSkuBySerial {view.SerialNo}").ToList();
                    view.SkuCode = machineDetail[0].Sku;
                    view.TracingName = Convert.ToString(machineDetail[0].SkuId);

                    TblSshMachineryServices machineryServices = new TblSshMachineryServices();

                    //makine hizmetleri tablosunu kurulum olarak kaydediyoruz
                    machineryServices.MachineId = machineControl.MachineId;
                    machineryServices.SkuCode = view.SkuCode;
                    machineryServices.SerialNo = view.SerialNo;
                    machineryServices.TracingName = view.TracingName;
                    machineryServices.GuaranteeComment = view.Comment;
                    machineryServices.GuaranteeBanDate = DateTime.ParseExact(view.BanDate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                    machineryServices.IsApproved = true;
                    machineryServices.IsRejected = false;
                    machineryServices.ReadFlags = false;
                    machineryServices.ServiceTypeId = 6;
                    machineryServices.FileNames = view.FileNames;
                    machineryServices.CreatedBy = Convert.ToInt32(userId);
                    machineryServices.CreatedDate = DateTime.Now;

                    bool kaydedildiMi = context.TblSshMachineryServicesRepository.Insert(machineryServices);
                    context.Save();


                    if (kaydedildiMi == false)
                    {
                        response.isSuccess = false;
                        response.ErrorMessage = "Hata oluştu! Lütfen tekrar deneyiniz..";
                    }
                    else
                    {
                        response.isSuccess = true;
                        response.ErrorMessage = "";

                    }
                }
                catch (Exception)
                {
                    response.isSuccess = false;
                    response.ErrorMessage = "Hata oluştu! Lütfen tekrar deneyiniz..";

                }
            }
            return response;
        }
    }
}
