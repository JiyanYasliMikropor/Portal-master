using Microsoft.Extensions.Configuration;
using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.ServiceContracts;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.UI.Services
{
    public class LoginService: ILoginService
    {
        IConfiguration _config;
        public LoginService(IConfiguration config)
        {
            _config = config;
        }
        public LoginResponse Login(LoginRequest loginRequest)
        {
            var request = new RestRequest("api/Login", Method.POST, DataFormat.Json)
                 .AddJsonBody(loginRequest);

            var response = Globals.ApiClient.Execute<LoginResponse>(request);
            return response.Data;
        }

    }
}
