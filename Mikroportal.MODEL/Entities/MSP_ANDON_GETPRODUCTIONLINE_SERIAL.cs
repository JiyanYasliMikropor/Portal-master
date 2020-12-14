using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    [Table("MSP_ANDON_GETPRODUCTIONLINE_SERIAL")]
    public class MSP_ANDON_GETPRODUCTIONLINE_SERIAL
    {
        [Key]
        public string MACHINECODE { get; set; }
        public string MACHINENAME { get; set; }
        public int? ACTUALAMOUNT { get; set; }
        public int? THEORICAMOUNT { get; set; }
        public int? BALANCEAMOUNT { get; set; }
        public int? RISK { get; set; }
        public int? EMPTYPALETTEAMOUNT { get; set; }
        public int? PLANAMOUNT { get; set; }
        public decimal? INDEKS { get; set; }

    }
}
