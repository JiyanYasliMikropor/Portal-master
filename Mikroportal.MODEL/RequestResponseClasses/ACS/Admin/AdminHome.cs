using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.RequestResponseClasses.ACS.Admin
{
    class AdminHome
    {
    }

    public class RejectedRequest
    {
        public string machineryServiceId { get; set; }
        public string isRejectedComment { get; set; }
    }

    public class SearchRequest
    {
        public string search { get; set; }
    
    }
}
