using Mikroportal.MODEL.RequestResponseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.ServiceContracts
{
    public interface IDashboardService
    {
        public GetDashboardMenuResponse GetDashboardMenu(int UserId);
    }
}
