using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.ServiceContracts.ACS
{
    public interface IReplacementService
    {
        public SerialNoCheckAndHistoryResponse SerialNoCheckAndHistory(string serialNo);
        public TblSshMachineryServicesResponse AddReplacement(ReplacementView view);
        public GetDashboardMenuResponse GetDashboardMenu(int UserId);
    }
}
