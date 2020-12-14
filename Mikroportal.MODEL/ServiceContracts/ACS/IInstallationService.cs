using Microsoft.AspNetCore.Http;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.ServiceContracts.ACS
{
    public interface IInstallationService
    {
        public GetSkuBySerialResponse SerialNoCheck(string serialNo);
        public TblSshMachineryServicesResponse AddMachine(InstallationView view);

        public GetDashboardMenuResponse GetDashboardMenu(int UserId);
    }
}
