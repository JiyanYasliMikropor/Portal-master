using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    public partial class Users
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public int? Createdby { get; set; }
        public DateTime? Createddate { get; set; }
        public int? Modifiedby { get; set; }
        public DateTime? Modifieddate { get; set; }
        public bool? Islogin { get; set; }
        public short? Terminalfid { get; set; }
        public int? Usergroupfid { get; set; }
        public DateTime? Logindatetime { get; set; }
        public DateTime? Logoutdatetime { get; set; }
        public int? PersonelKoduRowid { get; set; }
        public int? Machinefid { get; set; }
        public bool? Issms { get; set; }
        public int? Missionfid { get; set; }
        public string Cellphoneno { get; set; }
        public int? Departmentfid { get; set; }
        public int? Userstatus { get; set; }
        public int? Pillarfid { get; set; }
        public bool? Isactive { get; set; }
        public decimal? Vacationbalance { get; set; }
        public Guid? UserGuid { get; set; }
        public bool? CanWriteOccupancy { get; set; }
        public int? CurrencyUnitId { get; set; }
        public int? DiscountId { get; set; }
        public string PasswordHash { get; set; }
        public int? ErpDepartmanId { get; set; }
        public string ErpDepartman { get; set; }
        public int? GustoDepartmanId { get; set; }
        public string GustoDepartman { get; set; }
        public string UserService { get; set; }
    }
}
