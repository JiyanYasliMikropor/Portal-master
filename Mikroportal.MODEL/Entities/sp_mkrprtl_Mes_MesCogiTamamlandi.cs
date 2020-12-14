using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    [Table("sp_mkrprtl_Mes_MesCogiTamamlandi")]
    public class sp_mkrprtl_Mes_MesCogiTamamlandi
    {
        [Key]
        public string Sonuc { get; set; }
     


    }
}
