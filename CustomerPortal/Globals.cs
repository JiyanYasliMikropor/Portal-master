using RestSharp;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerPortal
{
    public static class Globals
    {
        public static void SetGlobals(IConfiguration configuration)
        {
            ApiClient = new RestClient(configuration["Mikroportal.API:Host"]);
            CONTENT_FOLDER_ROOT_PATH = "..";
        }
        public static string CONTENT_FOLDER_ROOT_PATH { get; private set; }
        public static RestClient ApiClient { get; private set; }

        public static string URLPath = "";
    }
}
