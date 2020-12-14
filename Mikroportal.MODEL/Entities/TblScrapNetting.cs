using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    public partial class TblScrapNetting
    {
        public int ScrapNettingId { get; set; }
        public int? StockId { get; set; }
        public int? TrackingId { get; set; }
        public string StockName { get; set; }
        public string TrackingName { get; set; }
        public string Unit { get; set; }
        public int? MachineId { get; set; }
        public decimal? ProcessAmount { get; set; }
        public decimal? TechnicalScrapAmount { get; set; }
        public decimal? NettingNagetiveAmount { get; set; }
        public decimal? FinalAmountAfterNetting { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
