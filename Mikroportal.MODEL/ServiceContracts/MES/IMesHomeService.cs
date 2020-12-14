using System;
using System.Collections.Generic;
using System.Text;
using Mikroportal.MODEL.RequestResponseClasses;

namespace Mikroportal.MODEL.ServiceContracts.MES
{
    public interface IMesHomeService
    {
        public GetDashboardMenuResponse GetDashboardMenu(int UserId);
    }
}
