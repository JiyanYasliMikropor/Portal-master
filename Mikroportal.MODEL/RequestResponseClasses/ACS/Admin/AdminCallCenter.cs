using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.RequestResponseClasses.ACS.Admin
{
    class AdminCallCenter
    {
    }

    public class AddFileUploadCallCenterResponse : ResponseBase
    {
    }

    public class CallCenterFilter
    {
        public string searchSerialNo { get; set; }
        public string searchSkuCode { get; set; }
        public string searchTracingName { get; set; }
        public DateTime searchStartDate { get; set; }
        public DateTime searchEndDate { get; set; }
        public string searchStatus { get; set; }
    }
}
