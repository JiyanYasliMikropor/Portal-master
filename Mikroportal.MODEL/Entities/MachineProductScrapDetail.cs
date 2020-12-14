using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    public partial class MachineProductScrapDetail
    {
        public long Id { get; set; }
        public long? ReasonId { get; set; }
        public long? MppdId { get; set; }
        public long? Mpsid { get; set; }
        public int? StockId { get; set; }
        public int? TrackingId { get; set; }
        public bool? ProductType { get; set; }
        public int? MachineId { get; set; }
        public string Reason { get; set; }
        public decimal? Amount { get; set; }
        public string Unit { get; set; }
        public decimal? Wastage { get; set; }
        public decimal? Twastage { get; set; }
        public decimal? Scrap { get; set; }
        public string Cunit { get; set; }
        public string Description { get; set; }
        public string Productserial { get; set; }
        public string Paletid { get; set; }
        public string EmployeeCode { get; set; }
        public long? ScrapTicketId { get; set; }
        public DateTime? ScrapDate { get; set; }
        public string StockName { get; set; }
        public string TrackingName { get; set; }
        public decimal? ValidatedAmount { get; set; }
        public DateTime? ValidationDate { get; set; }
        public string ValidationEmployeeCode { get; set; }
        public int? Onerpid { get; set; }
        public decimal? Amount2 { get; set; }
        public string Unit2 { get; set; }
        public decimal? ValidatedAmount2 { get; set; }
        public string ValidationUnit2 { get; set; }
        public decimal? TotalAmountFromOperator { get; set; }
        public bool? IsDone { get; set; }
        public string TotalAmountFromOperatorUnit { get; set; }
        public bool? IsUsed { get; set; }
        public bool? IsUseableForErp { get; set; }
        public bool? IsSeperateBomMaterial { get; set; }
        public long? MpsdIdForSeperateBom { get; set; }
        public int? ScrapNettingId { get; set; }
        public bool? Sapflag { get; set; }
    }
}
