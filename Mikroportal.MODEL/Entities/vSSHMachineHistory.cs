using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    [Table("vSSHMachineHistory")]
    public class vSSHMachineHistory
    {
        [Key]
        public int MachineryServiceId { get; set; }
        public int? MachineId { get; set; }
        public string UserName { get; set; }
        public string SerialNo { get; set; }
        public string SkuCode { get; set; }
        public string TracingName { get; set; }
        public string MachineStartedDate { get; set; }
        public string CompressorInformation { get; set; }
        public string StafName { get; set; }
        public string MachineComment { get; set; }
        public string ChangedPartsComment { get; set; }
        public string CustomerComplaint { get; set; }
        public string CaringCompanyName { get; set; }
        public string CompanyName { get; set; }
        public string RelatedPerson { get; set; }
        public string RelatedPersonEmail { get; set; }
        public string RelatedPersonTitle { get; set; }
        public string RelatedPersonPhone { get; set; }
        public string CompanyAdress { get; set; }
        public string CustomerComment { get; set; }
        public string ServiceName { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
        public string IsRejectedComment { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedDateFormat { get; set; }
        public string ServiceDate { get; set; }
        public string CallCenterComment { get; set; }
        public string UserEmail { get; set; }
        public string FileNames { get; set; }
        public string ServiceStatus { get; set; }
        public string GuaranteeStartDate { get; set; }
        public string GuaranteeEndDate { get; set; }
        public string GuaranteeBanDate { get; set; }
        public string GuaranteeComment { get; set; }

   
    }
}
