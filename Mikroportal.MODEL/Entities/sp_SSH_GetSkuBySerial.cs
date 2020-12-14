using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mikroportal.MODEL.Entities
{
    [Table("sp_SSH_GetSkuBySerial")]
    public partial class sp_SSH_GetSkuBySerial
    {
        [Key]
        public int? SkuId { get; set; }
        public string Sku { get; set; }
        public int? TrackingId { get; set; }
        public string TrackingName { get; set; }
        public int? StockId { get; set; }
        public string StockName { get; set; }
    }
}
