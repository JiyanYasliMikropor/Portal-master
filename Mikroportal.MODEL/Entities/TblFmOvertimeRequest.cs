using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    public partial class TblFmOvertimeRequest
    {
        public int OvertimeRequestId { get; set; }
        public DateTime? OvertimeDate { get; set; }
        public int? ShiftId { get; set; }
        public int? ShiftTime { get; set; }
        public int? OvertimeTypeId { get; set; }
        public int? DepartmentId { get; set; }
        public string OvertimeReason { get; set; }
        public string OvertimeProductionInformation { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string DeletedBy { get; set; }
        public int? GustoRowId { get; set; }
    }
}
