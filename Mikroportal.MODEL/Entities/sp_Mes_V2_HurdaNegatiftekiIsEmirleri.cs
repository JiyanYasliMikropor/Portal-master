using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    [Table("sp_Mes_V2_HurdaNegatiftekiIsEmirleri")]
    public class sp_Mes_V2_HurdaNegatiftekiIsEmirleri
    {
        [Key]
        public Guid UniqId { get; set; }
        public int WorkOrderNo { get; set; }
        public string UrunStokAd { get; set; }
        public string UrunIzlemeAd { get; set; }
        public int? UretilenMiktar { get; set; }
        public string STOCK_NAME { get; set; }
        public string TRACKING_NAME { get; set; }
        public decimal? AMOUNT { get; set; }
        public string UNIT { get; set; }
        public DateTime? SCRAP_DATE { get; set; }
        public string REASON { get; set; }

    }
}
