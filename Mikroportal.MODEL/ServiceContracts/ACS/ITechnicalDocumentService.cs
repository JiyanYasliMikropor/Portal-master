using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.ServiceContracts.ACS
{
    public interface ITechnicalDocumentService
    {
        public TechnicalDocumentResponse SerialNoCheckTechnicalDocument(string serialNo, string UserId);
        public GetDashboardMenuResponse GetDashboardMenu(int UserId);

    }
}
