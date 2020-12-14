using Mikroportal.MODEL.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mikroportal.API.Services
{
    public class User :IUser
    {
        public static ClaimsIdentity Identity { get; internal set; }

        public int GetID()
        {
            return 1;
        }

    }
}
