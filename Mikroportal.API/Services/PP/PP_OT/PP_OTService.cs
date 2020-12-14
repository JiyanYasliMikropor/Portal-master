using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Mikroportal.DATA;
using Mikroportal.MODEL.Entities;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.PP.PP_OT;
using Mikroportal.MODEL.ServiceContracts.PP.PP_OT;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.API.Services.PP.PP_OT
{
    public class PP_OTService : IPP_OTService
    {
        public IConfiguration _configuration { get; }
        IMikroportalUOW context;
        private readonly SpDataContext _dbContext;
        public PP_OTService(IConfiguration configuration, IMikroportalUOW mikroportalContext, SpDataContext dbContext)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            context = mikroportalContext;
        }

        public GetDashboardMenuResponse GetPPMenu(int UserId)
        {
            GetDashboardMenuResponse response = new GetDashboardMenuResponse();
            List<GetDashboardMenu> homeMenu = new List<GetDashboardMenu>();

            var userMenu = from menuItem in context.TblMenuItemsRepository.Get()
                           join userMenuItems in context.TblUserMenuItemRepository.Get()
                           on menuItem.MenuItemId equals userMenuItems.MenuItemId
                           where menuItem.MenuId == 5 && userMenuItems.UserId == UserId
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

        public GetAllOvertimeListResponse GetAllOvertimeList(OvertimeFilter request)
        {
            request.searchStartDate = request.searchStartDate == DateTime.MinValue ? DateTime.MinValue : request.searchStartDate;
            request.searchEndDate = request.searchEndDate == DateTime.MinValue ? DateTime.MaxValue : request.searchEndDate;

            GetAllOvertimeListResponse response = new GetAllOvertimeListResponse();

            DateTime now = DateTime.Now;

            var UserData = context.TblUsersRepository.Get().SingleOrDefault(q => q.UserId == Convert.ToInt32(request.UserId));
            if (UserData.UserAccount != "03316" && UserData.UserAccount != "03850")
            {
                var OvertimeListFillter = _dbContext.vFMList.Where(
                    q => q.CreatedDate.Value.Year == now.Year && q.CreatedBy == UserData.UserAccount &&
                    q.DepartmentName.Contains(request.searchDepartmentName == null ? "" : request.searchDepartmentName)&&
                    q.UsersEmployeeCode.Contains(request.searchUsersEmployeeCodeNo == null ? "" : request.searchUsersEmployeeCodeNo) &&
                    q.FULLNAME.Contains(request.searchFullname == null ? "" : request.searchFullname) &&
                    q.shiftName.Contains(request.searchShiftName == null ? "" : request.searchShiftName) &&
                    q.OvertimeTypeName.Contains(request.searchOvertimeTypeName == null ? "" : request.searchOvertimeTypeName) &&
                    q.Confirmation.Contains(request.searchConfirmation == null ? "" : request.searchConfirmation)
                    && (q.OvertimeDateTime >= request.searchStartDate && q.OvertimeDateTime <= request.searchEndDate)
                    ).OrderByDescending(q => q.CreatedDate).ToList();


                var OvertimeListFillterSum = OvertimeListFillter.Sum(q => q.ShiftTime);
                response.OvertimeSum = Convert.ToInt32(OvertimeListFillterSum);
                    response.AllOvertimeList = OvertimeListFillter;
              
                    response.isSuccess = true;
                    response.ErrorMessage = "";
                
              

            }
            else
            {
                var OvertimeList = _dbContext.vFMList.Where(
                    q => q.CreatedDate.Value.Year == now.Year &&                     
                    q.DepartmentName.Contains(request.searchDepartmentName == null ? "" : request.searchDepartmentName) &&
                    q.UsersEmployeeCode.Contains(request.searchUsersEmployeeCodeNo == null ? "" : request.searchUsersEmployeeCodeNo) &&
                    q.FULLNAME.Contains(request.searchFullname == null ? "" : request.searchFullname) &&
                    q.shiftName.Contains(request.searchShiftName == null ? "" : request.searchShiftName) &&
                    q.OvertimeTypeName.Contains(request.searchOvertimeTypeName == null ? "" : request.searchOvertimeTypeName) &&
                    q.Confirmation.Contains(request.searchConfirmation == null ? "" : request.searchConfirmation)
                    && (q.CreatedDate >= request.searchStartDate && q.CreatedDate <= request.searchEndDate)
                    ).OrderByDescending(q => q.CreatedDate).ToList();
                response.AllOvertimeList = OvertimeList;
                var OvertimeListSum = OvertimeList.Sum(q => q.ShiftTime);
                response.OvertimeSum = Convert.ToInt32(OvertimeListSum);
                response.isSuccess = true;
                    response.ErrorMessage = "";
            
            }


            return response;
        }


        public GetAllOvertimeListResponse GetAllOvertimeListFilter(int UserId, string Durum)
        {
            GetAllOvertimeListResponse response = new GetAllOvertimeListResponse();

            DateTime now = DateTime.Now;


            //List<sp_SSH_GetSkuBySerial> machineDetail = _dbContext.sp_SSH_GetSkuBySerial.FromSqlInterpolated($"EXECUTE dbo.sp_SSH_GetSkuBySerial {serialNo}").ToList();

            var UserData = context.TblUsersRepository.Get().SingleOrDefault(q => q.UserId == UserId);

            if (UserData.UserAccount != "03316" && UserData.UserAccount != "03850")
            {
                var OvertimeListFillter = _dbContext.vFMList.Where(q => q.CreatedDate.Value.Year == now.Year && q.CreatedBy == UserData.UserAccount && q.Confirmation == Durum).OrderByDescending(q => q.CreatedDate).ToList();
                response.AllOvertimeList = OvertimeListFillter;
                if (OvertimeListFillter != null && OvertimeListFillter.Count > 0)
                {
                    response.isSuccess = true;
                    response.ErrorMessage = "";
                }
                else
                {
                    response.isSuccess = false;
                    response.ErrorMessage = "Hata oluştu tekrar deneyiniz!";
                }

            }
            else
            {
                var OvertimeList = _dbContext.vFMList.Where(q => q.CreatedDate.Value.Year == now.Year && q.Confirmation == Durum).OrderByDescending(q => q.CreatedDate).ToList();
                response.AllOvertimeList = OvertimeList;

                if (OvertimeList != null && OvertimeList.Count > 0)
                {
                    response.isSuccess = true;
                    response.ErrorMessage = "";
                }
                else
                {
                    response.isSuccess = false;
                    response.ErrorMessage = "Hata oluştu tekrar deneyiniz!";
                }
            }


            return response;
        }

        public SelectedItemApprovedResponse SelectedItemApproved(int UserId, string FMList)
        {
            SelectedItemApprovedResponse response = new SelectedItemApprovedResponse();
            var UserData = context.TblUsersRepository.Get().SingleOrDefault(q => q.UserId == UserId);
            DateTime now = DateTime.Now;
            try
            {
                if (FMList == null || FMList == "")
                {
                    response.isSuccess = false;
                    response.ErrorMessage = "Hata oluştu tekrar deneyiniz!";
                }
                else
                {
                    //Fazla Mesaiye katılacak personelleri onaylama
                    var person = FMList.Split(',');
                    foreach (var item in person)
                    {
                        var staff = context.TblFmOvertimeRequestStaffRepository.Get().SingleOrDefault(q => q.OvertimeRequestStaffId == Convert.ToInt32(item));

                        staff.ModifiedBy = UserData.UserAccount;
                        staff.ModifiedDate = DateTime.Now;
                        staff.Confirmation = true;
                        staff.ConfirmationModifiedBy = UserData.UserAccount;
                        context.TblFmOvertimeRequestStaffRepository.Update(staff);
                        context.Save();
                    }
                    response.isSuccess = true;
                }
            }
            catch (Exception)
            {

                response.isSuccess = false;
                response.ErrorMessage = "Hata oluştu tekrar deneyiniz!";
            }

            return response;
        }
        public AllApprovedResponse AllApproved(int UserId)
        {
            AllApprovedResponse response = new AllApprovedResponse();

            DateTime now = DateTime.Now;
            try
            {
                var UserData = context.TblUsersRepository.Get().SingleOrDefault(q => q.UserId == UserId);
                var staffList = context.TblFmOvertimeRequestStaffRepository.Get().Where(q => q.Confirmation == null).ToList();
                if (staffList != null && staffList.Count > 0)
                {
                    //Fazla Mesai listesini onayla
                    foreach (var staff in staffList)
                    {
                        staff.ModifiedBy = UserData.UserAccount;
                        staff.ModifiedDate = DateTime.Now;
                        staff.Confirmation = true;
                        staff.ConfirmationModifiedBy = UserData.UserAccount;
                        context.TblFmOvertimeRequestStaffRepository.Update(staff);
                        context.Save();
                    }
                    response.isSuccess = true;
                }
                else
                {

                    response.isSuccess = false;
                    response.ErrorMessage = "Rededilecek fazla mesai bulunamadı!";
                }
            }
            catch (Exception)
            {

                response.isSuccess = false;
                response.ErrorMessage = "Hata oluştu tekrar deneyiniz!";
            }

            return response;
        }

        public SelectedItemDenialResponse SelectedItemDenial(int UserId, string FMList)
        {
            SelectedItemDenialResponse response = new SelectedItemDenialResponse();
            var UserData = context.TblUsersRepository.Get().SingleOrDefault(q => q.UserId == UserId);
            DateTime now = DateTime.Now;
            try
            {
                if (FMList == null || FMList == "")
                {
                    response.isSuccess = false;
                    response.ErrorMessage = "Hata oluştu tekrar deneyiniz!";
                }
                else
                {
                    var person = FMList.Split(',');
                    foreach (var item in person)
                    {
                        var staff = context.TblFmOvertimeRequestStaffRepository.Get().SingleOrDefault(q => q.OvertimeRequestStaffId == Convert.ToInt32(item));

                        staff.ModifiedBy = UserData.UserAccount;
                        staff.ModifiedDate = DateTime.Now;
                        staff.Confirmation = false;
                        staff.ConfirmationModifiedBy = UserData.UserAccount;
                        context.TblFmOvertimeRequestStaffRepository.Update(staff);
                        context.Save();


                    }
                    response.isSuccess = true;
                }
            }
            catch (Exception)
            {

                response.isSuccess = false;
                response.ErrorMessage = "Hata oluştu tekrar deneyiniz!";
            }

            return response;
        }

        public AllListDenialResponse AllListDenial(int UserId)
        {
            AllListDenialResponse response = new AllListDenialResponse();
            try
            {
                var UserData = context.TblUsersRepository.Get().SingleOrDefault(q => q.UserId == UserId);
                var staffList = context.TblFmOvertimeRequestStaffRepository.Get().Where(q => q.Confirmation == null).ToList();
                if (staffList != null && staffList.Count > 0)
                {
                    //Fazla Mesai listesini onayla
                    foreach (var staff in staffList)
                    {
                        staff.ModifiedBy = UserData.UserAccount;
                        staff.ModifiedDate = DateTime.Now;
                        staff.Confirmation = false;
                        staff.ConfirmationModifiedBy = UserData.UserAccount;
                        context.TblFmOvertimeRequestStaffRepository.Update(staff);
                        context.Save();
                    }
                    response.isSuccess = true;
                }
                else
                {

                    response.isSuccess = false;
                    response.ErrorMessage = "Rededilecek fazla mesai bulunamadı!";
                }
            }
            catch (Exception)
            {

                response.isSuccess = false;
                response.ErrorMessage = "Hata oluştu tekrar deneyiniz!";
            }

            return response;
        }


        public GetAllPersonListResponse GetAllPersonList(GetPersonelView view)
        {
            GetAllPersonListResponse response = new GetAllPersonListResponse();
            view.OvertimeDate = view.OvertimeDate == DateTime.MinValue ? DateTime.MinValue : view.OvertimeDate;
            try
            {
                if (view.OvertimeDate == DateTime.MinValue)
                {
                    response.isSuccess = true;
                    response.PersonelList = null;
                }
                else
                {

                    DateTime OvertimeRequestDate = Convert.ToDateTime(view.OvertimeDate);
                    var OvertimeDateFormat = String.Format("{0:yyyy-MM-dd HH:mm:ss}", OvertimeRequestDate);

                    var PersonelList = _dbContext.sp_FM_GetPersonelList.FromSqlInterpolated($"EXECUTE dbo.sp_FM_GetPersonelList {OvertimeDateFormat},{view.DepartmentId}").ToList();
                    if (PersonelList != null && PersonelList.Count > 0)
                    {
                        response.isSuccess = true;
                        response.PersonelList = PersonelList;
                    }
                    else
                    {
                        response.isSuccess = false;
                        response.PersonelList = null;
                        response.ErrorMessage = "Hata oluştu tekrar deneyiniz!";
                    }
                }

            }
            catch (Exception)
            {

                response.isSuccess = false;
                response.PersonelList = null;
                response.ErrorMessage = "Hata oluştu tekrar deneyiniz!";
            }
            return response;
        }

        public GetAllOvertimeTypeListResponse GetAllOvertimeTypeList()
        {
            GetAllOvertimeTypeListResponse response = new GetAllOvertimeTypeListResponse();

            try
            {
                var overtimeType = context.TblFmOvertimeTypeRepository.Get().ToList();
                if (overtimeType != null && overtimeType.Count > 0)
                {
                    response.isSuccess = true;
                    response.OvertimeType = overtimeType;
                }
                else
                {
                    response.isSuccess = false;
                    response.OvertimeType = null;
                    response.ErrorMessage = "Hata oluştu tekrar deneyiniz!";
                }
            }
            catch (Exception)
            {

                response.isSuccess = false;
                response.OvertimeType = null;
                response.ErrorMessage = "Hata oluştu tekrar deneyiniz!";
            }
            return response;
        }


        public GetAllShiftListResponse GetAllShiftList()
        {
            GetAllShiftListResponse response = new GetAllShiftListResponse();

            try
            {
                var GetAllShiftList = context.TblFmShiftRepository.Get().ToList();
                if (GetAllShiftList != null && GetAllShiftList.Count > 0)
                {
                    response.isSuccess = true;
                    response.GetAllShiftList = GetAllShiftList;
                }
                else
                {
                    response.isSuccess = false;
                    response.GetAllShiftList = null;
                    response.ErrorMessage = "Hata oluştu tekrar deneyiniz!";
                }
            }
            catch (Exception)
            {

                response.isSuccess = false;
                response.GetAllShiftList = null;
                response.ErrorMessage = "Hata oluştu tekrar deneyiniz!";
            }
            return response;
        }

        public GetAllDepartmentListResponse GetAllDepartmentList(int UserId)
        {
            GetAllDepartmentListResponse response = new GetAllDepartmentListResponse();

            try
            {
                var UserData = context.TblUsersRepository.Get().SingleOrDefault(q => q.UserId == UserId);
                var User = context.UsersRepository.Get().SingleOrDefault(q => q.EmployeeCode == UserData.UserAccount);

                List<Department> DepartmentListData = context.MesGelirgideryeriRepository.Get().Select(m => new Department
                {
                    GgyAdi = m.GgyAdi,
                    UstId = m.UstId
                }).Distinct().ToList();
                if (DepartmentListData != null && DepartmentListData.Count > 0)
                {
                    response.isSuccess = true;
                    response.UstId = User.GustoDepartmanId;
                    response.DepartmentList = DepartmentListData;
                }
                else
                {
                    response.isSuccess = false;
                    response.DepartmentList = null;
                    response.UstId = null;
                    response.ErrorMessage = "Hata oluştu tekrar deneyiniz!";
                }
            }
            catch (Exception)
            {

                response.isSuccess = false;
                response.DepartmentList = null;
                response.ErrorMessage = "Hata oluştu tekrar deneyiniz!";
            }
            return response;
        }

        public AddOvertimeRequestResponse AddOvertimeRequest(OvertimeRequestView view)
        {
            string userId = view.UserId;
            AddOvertimeRequestResponse response = new AddOvertimeRequestResponse();

            if (view.OvertimeDate == null || view.OvertimeDate == "")
            {
                response.ErrorMessage = "Tarih Seçilmemiş";
                response.isSuccess = false;
                return response;
            }
            if (view.PersonList == null || view.PersonList == "")
            {
                response.ErrorMessage = "Personel Seçilmemiş";
                response.isSuccess = false;
                return response;
            }


            try
            {
                var users = context.TblUsersRepository.Get().SingleOrDefault(p => p.UserId == Convert.ToInt32(userId));
                //Fazla Mesai Tablosuna kayıt oluşturma
                TblFmOvertimeRequest overtime = new TblFmOvertimeRequest();
                overtime.ShiftId = view.ShiftId;
                overtime.ShiftTime = view.ShiftTime;
                overtime.OvertimeTypeId = view.OvertimeTypeId;
                overtime.OvertimeDate = DateTime.ParseExact(view.OvertimeDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                overtime.DepartmentId = view.DepartmentId;
                overtime.OvertimeReason = view.OvertimeReason;
                overtime.OvertimeProductionInformation = view.OvertimeProductionInformation;
                overtime.CreatedBy = users.UserAccount;
                overtime.CreatedDate = DateTime.Now;
                bool kaydedildiMi = context.TblFmOvertimeRequestRepository.Insert(overtime);
                context.Save();

                if (kaydedildiMi == false)
                {
                    response.isSuccess = false;
                    response.ErrorMessage = "Hata oluştu!!  Lütfen tekrar deneyiniz..";
                    return response;
                }
                else
                {
                    var person = view.PersonList.Split(',');
                    foreach (var item in person)
                    {
                        var data = context.UsersRepository.Get().SingleOrDefault(q => q.Id == Convert.ToInt32(item));
                        TblFmOvertimeRequestStaff staff = new TblFmOvertimeRequestStaff();
                        staff.CreatedBy = users.UserAccount;
                        staff.CreatedDate = DateTime.Now;
                        staff.OvertimeRequestId = overtime.OvertimeRequestId;
                        staff.UsersEmployeeCode = data.EmployeeCode;
                        bool staffKaydedildiMi = context.TblFmOvertimeRequestStaffRepository.Insert(staff);
                        context.Save();
                        if (kaydedildiMi == false)
                        {
                            response.isSuccess = false;
                            response.ErrorMessage = "Hata oluştu!!  Lütfen tekrar deneyiniz..";
                        }
                        else {
                            response.isSuccess = true;
                            response.ErrorMessage = "";
                   

                        }

                    }

                    return response;
                }

            }
            catch (Exception)
            {

                response.isSuccess = false;
                response.ErrorMessage = "Hata oluştu!!  Lütfen tekrar deneyiniz..";
                return response;
            }


        }
    }
}
