using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.ServiceContracts.ACS.Admin
{
    public interface IAdminReportService
    {
        public AddFileUploadMachineHistoryResponse AddFileUploadMachineHistory(FileView view);
        public GetMachineHistoryListResponse GetMachineHistoryList(ReportFilter request);
        public InboxShowByIdResponse InboxShowById(string machineryServiceId);
        public GetDashboardMenuResponse GetACSAdminMenu(int UserId);
    }
}
