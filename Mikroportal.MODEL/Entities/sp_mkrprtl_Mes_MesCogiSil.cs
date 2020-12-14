using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Mikroportal.MODEL.Entities
{

    [Table("sp_mkrprtl_Mes_MesCogiSil")]
    public class sp_mkrprtl_Mes_MesCogiSil
    {
        [Key]
        public string Sonuc { get; set; }


    }
}
