using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    [Table("sp_FM_GetPersonelList")]
    public class sp_FM_GetPersonelList
    {

        [Key]
        public int ID { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public string FULLNAME { get; set; }
        public string PASSWORD { get; set; }
        public string MAIL { get; set; }
        public int? CREATEDBY { get; set; }
        public DateTime? CREATEDDATE { get; set; }
        public int? MODIFIEDBY { get; set; }
        public DateTime? MODIFIEDDATE { get; set; }
        public bool? ISLOGIN { get; set; }
        public short? TERMINALFID { get; set; }
        public int? USERGROUPFID { get; set; }
        public DateTime? LOGINDATETIME { get; set; }
        public DateTime? LOGOUTDATETIME { get; set; }
        public int? PersonelKodu_rowid { get; set; }
        public int? MACHINEFID { get; set; }
        public bool? ISSMS { get; set; }
        public int? MISSIONFID { get; set; }
        public string CELLPHONENO { get; set; }
        public int? DEPARTMENTFID { get; set; }
        public int? USERSTATUS { get; set; }
        public int? PILLARFID { get; set; }
        public bool? ISACTIVE { get; set; }
        public decimal? VACATIONBALANCE { get; set; }
        public Guid? UserGuid { get; set; }
        public bool? CanWriteOccupancy { get; set; }
        public int? CurrencyUnitId { get; set; }
        public int? DiscountId { get; set; }
        public string PasswordHash { get; set; }
        public int? Radar { get; set; }
        public bool? IsOvertime { get; set; }
        public string DepartmentName { get; set; }
        public int TotalShiftTime { get; set; }


    }
}
