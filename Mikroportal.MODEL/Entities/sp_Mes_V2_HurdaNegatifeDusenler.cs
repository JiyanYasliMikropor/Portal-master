using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    [Table("sp_Mes_V2_HurdaNegatifeDusenler")]
    public class sp_Mes_V2_HurdaNegatifeDusenler
    {
        [Key]
        public Guid UniqId { get; set; }
        public int? MachineId { get; set; }
        public string MACHINE_CODE { get; set; }
        public string MACHINE_NAME { get; set; }
        public int? StockId { get; set; }
        public string StockName { get; set; }
        public int? TrackingId { get; set; }
        public string TrackingName { get; set; }
        public decimal? NegatifDeger { get; set; }
        public string Unit { get; set; }
        public string MudurlukAdi { get; set; }

    }
}
