using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using Mikroportal.MODEL.RequestResponseClasses.ACS.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.ServiceContracts.ACS.Admin
{
    public interface IAdminHomeService
    {
        public GetInboxCountResponse GetInboxCount();
        public GetInboxListResponse GetInboxList(SearchRequest searchRequest);
        public InboxShowByIdResponse InboxShowById(string machineryServiceId);
        public TblSshMachineryServicesResponse IsApproved(string machineryServiceId);
        public TblSshMachineryServicesResponse IsRejected(RejectedRequest rejectedRequest);
        public GetDashboardMenuResponse GetACSAdminMenu(int UserId);
    }
}
