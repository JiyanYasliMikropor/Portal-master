using System;
using System.Collections.Generic;

namespace Mikroportal.MODEL.Entities
{
    public partial class TblSshMachines
    {
        public int MachineId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? MachineStartedDate { get; set; }
        public string SerialNo { get; set; }
        public string ModelNo { get; set; }
        public string AirDryerType { get; set; }
        public string CompressorInformation { get; set; }
        public string StaffName { get; set; }
        public string StaffLastName { get; set; }
        public string Comment { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ModifiedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int DeletedBy { get; set; }
        public bool? ReadFlags { get; set; }
        public int? TypeId { get; set; }
    }
}
