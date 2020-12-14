using Mikroportal.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.RequestResponseClasses.PP.PP_OT
{
    public class GetAllOvertimeListResponse : ResponseBase
    {
        public List<vFMList> AllOvertimeList { get; set; }
        public int? OvertimeSum { get; set; }
    }
    public class SelectedItemApprovedResponse : ResponseBase
    {
    }
    public class AllApprovedResponse : ResponseBase
    {
    }
    public class SelectedItemDenialResponse : ResponseBase
    {
    }
    public class AllListDenialResponse : ResponseBase
    {
    }
    public class GetAllOvertimeTypeListResponse : ResponseBase
    {
        public List<TblFmOvertimeType> OvertimeType { get; set; }
    }
    public class GetAllPersonListResponse : ResponseBase
    {
        public List<sp_FM_GetPersonelList> PersonelList { get; set; }
    }
    public class GetAllShiftListResponse : ResponseBase
    {
        public List<TblFmShift> GetAllShiftList { get; set; }
    }

    public class GetAllDepartmentListResponse : ResponseBase
    {
        public List<Department> DepartmentList { get; set; }
        public string UserId { get; set; }
        public int? UstId { get; set; }
    }

    public class GetPersonelView
    {
        public DateTime OvertimeDate { get; set; }
        public int? DepartmentId { get; set; }
    }


    public class Department
    {
        public int? UstId { get; set; }
        public string GgyAdi { get; set; }
    }
    public class AddOvertimeRequestResponse : ResponseBase
    {

    }
    
    public class OvertimeRequestView
    {
        public int OvertimeRequestId { get; set; }
        public string OvertimeDate { get; set; }
        public string UserId { get; set; }
        public int? ShiftId { get; set; }
        public int? ShiftTime { get; set; }
        public int? OvertimeTypeId { get; set; }
        public int? DepartmentId { get; set; }
        public string OvertimeReason { get; set; }
        public string OvertimeProductionInformation { get; set; }
        public string PersonList { get; set; }

    }

    public class OvertimeFilter
    {
        public string UserId { get; set; }
        public string searchDepartmentName { get; set; }
        public string searchUsersEmployeeCodeNo { get; set; }
        public string searchFullname { get; set; }
        public DateTime searchStartDate { get; set; }
        public DateTime searchEndDate { get; set; }
        public string searchShiftName { get; set; }
        public string searchOvertimeTypeName { get; set; }
        public string searchConfirmation { get; set; }
    }
}
