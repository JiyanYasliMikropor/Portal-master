using Microsoft.AspNetCore.Http;
using Mikroportal.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.RequestResponseClasses.ACS
{
    class Installation
    {
    }
    public class GetSkuBySerialResponse : ResponseBase
    {
        public List<sp_SSH_GetSkuBySerial> MachineDetail { get; set; }
        public bool isModal { get; set; }

    }

    public class InstallationView
    {
        public string UserId { get; set; }
        public string MachineStartedDate { get; set; }
        public string SerialNo { get; set; }
        public string SkuCode { get; set; }
        public string TracingName { get; set; }
        public string ModelNo { get; set; }
        public string AirDryerType { get; set; }
        public string CompressorInformation { get; set; }
        public string StaffName { get; set; }
        public string StaffLastName { get; set; }
        public string MachineComment { get; set; }

        public string CompanyName { get; set; }
        public string CompanyActivityName { get; set; }
        public string RelatedPersonName { get; set; }
        public string RelatedPersonLastName { get; set; }
        public string RelatedPersonEmail { get; set; }
        public string RelatedPersonTitle { get; set; }
        public string RelatedPersonPhone { get; set; }
        public string CompanyAdress { get; set; }
        public string Comment { get; set; }
        public string FileNames { get; set; }
        public string ChangedPartsComment { get; set; }
        public string CustomerComplaint { get; set; }
        public string CaringCompanyName { get; set; }
        public List<IFormFile> files { get; set; }

    }
}
