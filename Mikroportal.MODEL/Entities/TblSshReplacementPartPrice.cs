using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    public partial class TblSshReplacementPartPrice
    {
        public int ReplacementPartPriceId { get; set; }
        public int? PartNo { get; set; }
        public int? Qty { get; set; }
        public decimal? Price { get; set; }
        public string DryerModel { get; set; }
        public string Voltage { get; set; }
        public int? EdDrawingItemNo { get; set; }
        public string Currency { get; set; }
    }
}
