using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using Mikroportal.MODEL.RequestResponseClasses.ACS.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.ServiceContracts.ACS.Admin
{
    public interface IAdminCallCenterService
    {
        public InboxShowByIdResponse InboxShowById(string machineryServiceId);
        public GetCallCenterListResponse GetCallCenterList(CallCenterFilter CallCenterRequest);
        public GetDashboardMenuResponse GetACSAdminMenu(int UserId);
        public AddFileUploadCallCenterResponse AddFileUploadCallCenter(FileView view);

    }
}
