using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.PP.PP_OT;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.ServiceContracts.PP.PP_OT
{
    //Fazla Mesai
    public interface IPP_OTService
    {
        public GetDashboardMenuResponse GetPPMenu(int UserId);
        public GetAllOvertimeListResponse GetAllOvertimeList(OvertimeFilter request);
        public GetAllOvertimeListResponse GetAllOvertimeListFilter(int UserId,string data);
        public SelectedItemApprovedResponse SelectedItemApproved(int UserId,string FMList);
        public AllApprovedResponse AllApproved(int UserId);
        public SelectedItemDenialResponse SelectedItemDenial(int UserId, string FMList);
        public AllListDenialResponse AllListDenial(int UserId);
        public GetAllPersonListResponse GetAllPersonList(GetPersonelView view);
        public GetAllOvertimeTypeListResponse GetAllOvertimeTypeList();
        public GetAllShiftListResponse GetAllShiftList();
        public GetAllDepartmentListResponse GetAllDepartmentList(int UserId);
        public AddOvertimeRequestResponse AddOvertimeRequest(OvertimeRequestView view);
    }
}
