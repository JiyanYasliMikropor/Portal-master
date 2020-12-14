using Microsoft.AspNetCore.Http;
using Mikroportal.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.RequestResponseClasses.ACS
{
    class AdminReport
    {
    }
    public class FileView
    {
        public int MachineryServiceId { get; set; }
        public string FileNames { get; set; }
        public List<IFormFile> files { get; set; }

    }

    public class ReportFilter
    {
        public string searchSerialNo { get; set; }
        public string searchSkuCode { get; set; }
        public string searchTracingName { get; set; }
        public DateTime searchStartDate { get; set; }
        public DateTime searchEndDate { get; set; }
        public string searchStatus { get; set; }
        public string searchServicesType { get; set; }
    }


    public class GetMachineHistoryListResponse : ResponseBase
    {
        public List<vSSHMachineHistory> getInstallationList { get; set; }
        public int ReadFlagsNothingCount { get; set; }
    }

    public class GetInboxCountResponse : ResponseBase
    {
        public int ReadFlagsNothingCount { get; set; }
    }

    public class GetInboxListResponse : ResponseBase
    {
        public List<vSSHMachineHistory> inboxList { get; set; }
        public int ReadFlagsNothingCount { get; set; }
    } 
    
    public class InboxShowByIdResponse : ResponseBase
    {
        public List<vSSHMachineHistory> ServiceData { get; set; }
    }

    public class AddFileUploadMachineHistoryResponse : ResponseBase
    {
    }


}
