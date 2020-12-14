using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    [Table("sp_SSH_GetTrackingNameByModelNo")]
    public class sp_SSH_GetTrackingNameByModelNo
    {
        [Key]
        public string Sku { get; set; }
        public int? SkuId { get; set; }
        public string TrackingName { get; set; }
        public int? TrackingId { get; set; }
        public string StockName { get; set; }
        public int? StockId { get; set; }
    }
}
