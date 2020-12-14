using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.ServiceContracts.ACS.Admin
{
    public interface IAddGuaranteeService
    {
        public GetDashboardMenuResponse GetACSAdminMenu(int UserId);
        public GetInboxCountResponse GetInboxCount();

        public GetSkuBySerialResponse SerialNoCheckGuarantee(string serialNo);

        public TblSshMachineryServicesResponse AddGuarantee(GuaranteeView view);
        public TblSshMachineryServicesResponse AddBanGuarantee(GuaranteeView view);
    }
}
