using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    [Table("vFMList")]
    public class vFMList
    {
        [Key]
        public int OvertimeRequestStaffId { get; set; }
        public string OvertimeTypeName { get; set; }
        public string DepartmentName { get; set; }
        public string UsersEmployeeCode { get; set; }
        public string FULLNAME { get; set; }
        public int? ShiftTime { get; set; }
        public string shiftName { get; set; }
        public string Confirmation { get; set; }
        public string OvertimeProductionInformation { get; set; }
        public string OvertimeReason { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedBy { get; set; }
        public string OvertimeDate { get; set; }
        public DateTime? OvertimeDateTime { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedDateFormat { get; set; }
    }
}
