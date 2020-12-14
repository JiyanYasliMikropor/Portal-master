using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    [Table("sp_mkrprtl_Mes_MesCogiList")]

    public class sp_mkrprtl_Mes_MesCogiList
    {
        [Key]
      
        public long? CogiId { get; set; }
        public string BildirimTanimi { get; set; }
        public string CogiType { get; set; }
        public DateTime? CreatedDate { get; set; }
        //public string SiparisNumarasi { get; set; }
        //public string bilgi { get; set; }

        //public int HataDuzeltmeRoleId { get; set; }
        
    }
}
