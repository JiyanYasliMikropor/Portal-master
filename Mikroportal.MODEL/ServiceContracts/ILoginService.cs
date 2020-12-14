using Mikroportal.MODEL.RequestResponseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.ServiceContracts
{
    public interface ILoginService
    {
        public LoginResponse Login(LoginRequest loginRequest);

    }
}
