
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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.API.Services.ACS
{
    public class RepairAndMaintenanceService:IRepairAndMaintenanceService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IConfiguration _configuration { get; }
        IMikroportalUOW context;

        private readonly SpDataContext _dbContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public RepairAndMaintenanceService(IConfiguration configuration, IMikroportalUOW mikroportalContext, IHostingEnvironment hostingEnvironment, SpDataContext dbContext, IHttpContextAccessor iHttpContextAccessor)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            context = mikroportalContext;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = iHttpContextAccessor;
        }
        public SerialNoCheckAndHistoryResponse SerialNoCheckAndHistory(string serialNo)
        {
            SerialNoCheckAndHistoryResponse response = new SerialNoCheckAndHistoryResponse();

            List<sp_SSH_GetSkuBySerial> machineDetail = _dbContext.sp_SSH_GetSkuBySerial.FromSqlInterpolated($"EXECUTE dbo.sp_SSH_GetSkuBySerial {serialNo}").ToList();

            if (machineDetail != null && machineDetail.Count > 0)
            {
                bool machineControl = context.TblSshMachinesRepository.Get().Where(a => a.SerialNo == serialNo).Any();

                if (machineControl == true)
                {
                    var machineHistory = _dbContext.vSSHMachineHistory.Where(q => q.SerialNo == serialNo).ToList();
                    response.MachineHistory = machineHistory;
                    response.MachineDetail = machineDetail;
                    response.isModal = false;
                    response.isSuccess = true;
                    return response;
                }
                else
                {
                    response.MachineHistory = null;
                    response.isModal = true;
                    response.isSuccess = true;
                    response.MachineDetail = null;
                    return response;
                }

            }
            else
            {
                response.MachineHistory = null;
                response.ErrorMessage = "Seri numarası kontrol ediniz!";
                response.isSuccess = false;
                response.isModal = false;
                response.MachineDetail = null;
                return response;

            }

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

        public TblSshMachineryServicesResponse AddRepairAndMaintenance(RepairAndMaintenanceView view)
        {
            TblSshMachineryServicesResponse response = new TblSshMachineryServicesResponse();

            var machineDetail = context.TblSshMachinesRepository.Get().AsNoTracking().FirstOrDefault(q => q.SerialNo == view.SerialNo);
            string userId = view.UserId;
            if (machineDetail != null)
            {
                var machineDetailSkuCode = _dbContext.sp_SSH_GetSkuBySerial.FromSqlInterpolated($"EXECUTE dbo.sp_SSH_GetSkuBySerial {view.SerialNo}").ToList();
                view.SkuCode = machineDetailSkuCode[0].Sku;
                view.TracingName = Convert.ToString(machineDetailSkuCode[0].SkuId);

                TblSshMachineryServices machineryServices = new TblSshMachineryServices();

                machineryServices.MachineId = machineDetail.MachineId;
                machineryServices.SkuCode = view.SkuCode;
                machineryServices.TracingName = view.TracingName;
                machineryServices.Date = DateTime.ParseExact(view.RepairAndMaintenanceDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                machineryServices.CompressorInformation = view.CompressorInformation;
                machineryServices.SerialNo = view.SerialNo;
                machineryServices.StaffName = view.StaffName;
                machineryServices.StaffLastName = view.StaffLastName;
                machineryServices.MachineComment = view.MachineComment;
                machineryServices.ChangedPartsComment = view.ChangedPartsComment;
                machineryServices.CustomerComplaint = view.CustomerComplaint;
                machineryServices.CaringCompanyName = view.CaringCompanyName;
                machineryServices.CompanyName = view.CompanyName;
                machineryServices.RelatedPersonName = view.RelatedPersonName;
                machineryServices.RelatedPersonLastName = view.RelatedPersonLastName;
                machineryServices.RelatedPersonEmail = view.RelatedPersonEmail;
                machineryServices.RelatedPersonTitle = view.RelatedPersonTitle;
                machineryServices.RelatedPersonPhone = view.RelatedPersonPhone;
                machineryServices.CompanyAdress = view.CompanyAdress;
                machineryServices.CustomerComment = view.Comment;
                machineryServices.IsApproved = false;
                machineryServices.IsRejected = false;
                machineryServices.ReadFlags = false;
                machineryServices.ServiceTypeId = 3;
                machineryServices.FileNames = view.FileNames;
                machineryServices.CreatedBy = Convert.ToInt32(userId);
                machineryServices.CreatedDate = DateTime.Now;

                bool kaydedildiMi = context.TblSshMachineryServicesRepository.Insert(machineryServices);
                context.Save();

                if (kaydedildiMi == false)
                {
                    response.isSuccess = false;
                    response.ErrorMessage = "Hata oluştu!! Tamir ve/veya Bakım yapılamadı.. Lütfen tekrar deneyiniz..";
                    return response;
                }
                else
                {
                    response.isSuccess = true;
                    response.ErrorMessage = "";
                    return response;
                }
            }
            else
            {
                response.isSuccess = false;
                response.ErrorMessage = view.SerialNo + "Kurulum tamamlanmadan Tamir ve/veya Bakım işlemi yapılamaz.";
                return response;

            }
        }
    }
}
