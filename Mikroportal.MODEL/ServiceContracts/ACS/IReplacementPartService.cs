using Mikroportal.MODEL.Entities;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.ServiceContracts.ACS
{
    public interface IReplacementPartService
    {
        public SerialNoCheckReplacementPartResponse SerialNoCheckReplacementPart(string serialNo);
        public ModelNoCheckReplacementPartResponse ModelNoCheckReplacementPart(string modelNo);
        public GetReplacementPartPriceByPartNoResponse GetReplacementPartPriceByPartNo(string partNo);

        public GetDashboardMenuResponse GetDashboardMenu(int UserId);
    }
}
