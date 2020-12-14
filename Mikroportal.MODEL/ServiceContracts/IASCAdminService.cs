using Mikroportal.MODEL.Entities;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.ServiceContracts
{
    public interface IACSAdminService
    {
        public AddAuthorizedServiceView AddAuthorizedService(AddAuthorizedServiceView view);
        public TblSshMachineryServicesResponse AddGuarantee(GuaranteeView view);
        public TblSshMachineryServicesResponse BanGuarantee(GuaranteeView view);
        public TblSshMachineryServicesResponse AddFileUploadCallCenter(FileView view);
        public TblSshMachineryServicesResponse AddFileUploadMachineHistory(FileView view);
        public GetCallCenterListResponse GetCallCenterList(string searchSerialNo, string searchSkuCode, string searchTracingName, DateTime searchStartDate, DateTime searchEndDate, string searchStatus);
        public GetInboxCountResponse GetInboxCount();
        public GetMachineHistoryListResponse GetMachineHistoryList(string searchSerialNo, string searchSkuCode, string searchTracingName, DateTime searchStartDate, DateTime searchEndDate, string searchStatus, string searchServicesType);
        public GetInboxListResponse GetInboxList(string search);
        public InboxShowByIdResponse InboxShowById(string machineryServiceId);
        public TblSshMachineryServicesResponse IsApproved(string machineryServiceId);
        public TblSshMachineryServicesResponse IsRejected(string machineryServiceId, string isRejectedComment);
        
    }
}
