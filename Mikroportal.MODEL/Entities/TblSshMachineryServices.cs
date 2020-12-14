using System;
using System.Collections.Generic;

namespace Mikroportal.MODEL.Entities
{
    public partial class TblSshMachineryServices
    {
        public int MachineryServiceId { get; set; }
        public int? MachineId { get; set; }
        public string SkuCode { get; set; }
        public string TracingName { get; set; }
        public DateTime? Date { get; set; }
        public string CompressorInformation { get; set; }
        public string StaffName { get; set; }
        public string StaffLastName { get; set; }
        public string MachineComment { get; set; }
        public string ChangedPartsComment { get; set; }
        public string CustomerComplaint { get; set; }
        public string CaringCompanyName { get; set; }
        public string CompanyName { get; set; }
        public string RelatedPersonName { get; set; }
        public string RelatedPersonLastName { get; set; }
        public string RelatedPersonEmail { get; set; }
        public string RelatedPersonTitle { get; set; }
        public string RelatedPersonPhone { get; set; }
        public string CompanyAdress { get; set; }
        public string CustomerComment { get; set; }
        public bool? ReadFlags { get; set; }
        public int? ServiceTypeId { get; set; }
        public string FileNames { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ModifiedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int DeletedBy { get; set; }
        public string SerialNo { get; set; }
        public string CallCenterComment { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsRejected { get; set; }
        public string IsRejectedComment { get; set; }
        public DateTime? GuaranteeStartDate { get; set; }
        public DateTime? GuaranteeEndDate { get; set; }
        public DateTime? GuaranteeBanDate { get; set; }
        public string GuaranteeComment { get; set; }
    }
}
