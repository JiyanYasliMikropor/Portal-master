using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.ServiceContracts.ACS
{
    public interface IRepairAndMaintenanceService
    {
        public SerialNoCheckAndHistoryResponse SerialNoCheckAndHistory(string serialNo);
        public TblSshMachineryServicesResponse AddRepairAndMaintenance(RepairAndMaintenanceView view);
        public GetDashboardMenuResponse GetDashboardMenu(int UserId);
    }
}
