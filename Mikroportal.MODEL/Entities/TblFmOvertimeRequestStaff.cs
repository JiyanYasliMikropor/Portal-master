using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    public partial class TblFmOvertimeRequestStaff
    {
        public int OvertimeRequestStaffId { get; set; }
        public int? OvertimeRequestId { get; set; }
        public string UsersEmployeeCode { get; set; }
        public bool? Confirmation { get; set; }
        public string ConfirmationModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string DeletedBy { get; set; }
        public int? GustoRowId { get; set; }
    }
}
