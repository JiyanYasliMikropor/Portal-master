using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.RequestResponseClasses.ACS.Admin
{
    class AdminReplacement
    {
    }

    public class SearchPartNo
    {
        public string partNo { get; set; }

    }

    public class GetReplacementListResponse : ResponseBase
    {
        public List<AdminReplacementPartView> ReplacementList { get; set; }
    }

    public class UpdateReplacementPrice
    {
        public int? PartNo { get; set; }
        public int? Qty { get; set; }
        public decimal? Price { get; set; }
        public string DryerModel { get; set; }
        public string Voltage { get; set; }
        public int? EdDrawingItemNo { get; set; }
        public string Currency { get; set; }
    }
    public class AdminReplacementPartView
    {
        public int ReplacementPartId { get; set; }
        public int? PartNo { get; set; }
        public string Sad { get; set; }
        public int? StokKoduId { get; set; }
        public int? IzlemeKoduId { get; set; }
        public int ReplacementPartPriceId { get; set; }
        public int? Qty { get; set; }
        public decimal? Price { get; set; }
        public string DryerModel { get; set; }
        public string Voltage { get; set; }
        public int? EdDrawingItemNo { get; set; }
        public string Currency { get; set; }
    }
}
