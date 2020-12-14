using Mikroportal.MODEL.RequestResponseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.ServiceContracts.ACS
{
    public interface IHomeService
    {
        public LoginResponse SavePassword(string password, string UserId);
        public GetDashboardMenuResponse GetDashboardMenu(int UserId);
    }
}
