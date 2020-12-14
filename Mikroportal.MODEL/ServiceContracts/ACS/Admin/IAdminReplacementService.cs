using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using Mikroportal.MODEL.RequestResponseClasses.ACS.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.ServiceContracts.ACS.Admin
{
    public interface IAdminReplacementService
    {
        public GetReplacementListResponse GetReplacementList(SearchPartNo searchPartNo);
        public GetInboxCountResponse GetInboxCount();
        public GetDashboardMenuResponse GetACSAdminMenu(int UserId);
        public GetReplacementListResponse SaveReplacementPrice(UpdateReplacementPrice updateReplacementPrice);
    }


}
