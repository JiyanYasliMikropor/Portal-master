using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.ServiceContracts.ACS
{
    public interface ICallCenterService
    {
        public TblSshMachineryServicesResponse AddCallCenter(CallCenterView view);
        public GetDashboardMenuResponse GetDashboardMenu(int UserId);
    }
}
