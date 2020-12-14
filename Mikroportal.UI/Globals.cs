using Microsoft.Extensions.Configuration;
using Mikroportal.MODEL.Entities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mikroportal.UI
{
    public static class Globals
    {
        public static void SetGlobals(IConfiguration configuration)
        {
            ApiClient = new RestClient(configuration["Mikroportal.API:Host"]);
            CONTENT_FOLDER_ROOT_PATH = "..";
        }
        public static RestClient ApiClient { get; private set; }
        public static string CONTENT_FOLDER_ROOT_PATH { get; private set; }
   

        public static string Session_USER_NAME = "SessionUserName";
        public static string Session_USER_ID = "SessionUserId";

        public static string Session_USER_MACHINE_NAME = "SessionMachineName";
        public static string Session_USER_MACHINE_ID = "SessionMachineId";
        public static string Session_USER_WEBPAGE_ID = "SessionWebPageId";


        public static readonly string LOGGED_IN_USER_SESSION_KEY = "LoggedInUser";

        public static readonly string LOGGED_IN_USER_TOKEN_COOKIE_KEY = "USER_TOKEN";

        public static readonly List<TblUserRoles> Tbl_User_Roles = null;
    }
}
