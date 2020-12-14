using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    [Table("sp_Mes_V2_HurdaMaliyetRaporuGetir")]
    public class sp_Mes_V2_HurdaMaliyetRaporuGetir
    {
        [Key]
        public Guid UniqId { get; set; }
        public string yorum { get; set; }
        public long? bilesenId { get; set; }
        public bool? isSeperateBomMaterial { get; set; }
        public int? MACHINE_ID { get; set; }
        public int? ScrapNettingId { get; set; }
        public long? REASON_ID { get; set; }
        public string REASON { get; set; }
        public int? STOCK_ID { get; set; }
        public int? TRACKING_ID { get; set; }
        public string STOCK_NAME { get; set; }
        public string TRACKING_NAME { get; set; }
        public decimal? VALIDATED_AMOUNT { get; set; }
        public decimal? amount { get; set; }

        public string UNIT { get; set; }

        public DateTime? SCRAP_DATE { get; set; }
        public string MACHINE_NAME { get; set; }
        public decimal? birimMaliyet { get; set; }
        public decimal? ToplamMaliyet { get; set; }
        public string MudurlukAdi { get; set; }
    }
}
