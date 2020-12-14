
using Mikroportal.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.RequestResponseClasses.MES
{

    class MesCogi
    {

    }
   public class MesCogiRequest
    {
        public int UserId { get; set; }
        
         public long? CogiId { get; set; }
        public string BildirimTanimi { get; set; }
        public string CogiType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string SiparisNumarasi { get; set; }
        public int CogiRoleId { get; set; }
       

        public int CogiTypeId { get; set; }
       



    }


    public class GetMesCogiResponse : ResponseBase
    {
        public List<sp_mkrprtl_Mes_MesCogiList> MesCogiList { get; set; }
    }


    public class CogiIslemeAlRequest
    {
        public long? CogiId { get; set; }
       
        public int CompletedBy { get; set; }
       
    }

    public class GetCogiIslemeAlResponse : ResponseBase
    {
        public List<sp_mkrprtl_Mes_MesCogiTamamlandi> CogiİslemeAlList { get; set; }
    }



    public class CogiSilRequest
    {
        public long? CogiId { get; set; }

        public int CanceledBy { get; set; }
        

    }


    public class GetCogiSilResponse : ResponseBase
    {
        public List<sp_mkrprtl_Mes_MesCogiSil> CogiSilList { get; set; }
    }






}


