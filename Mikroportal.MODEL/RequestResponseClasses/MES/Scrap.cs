using Mikroportal.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.RequestResponseClasses.MES
{
    class Scrap
    {
    }

    public class GetAllMachinesResponse : ResponseBase
    {
        public List<Machines> MachineList { get; set; }

    }

    public class GetAllMudurlukResponse : ResponseBase
    {
        public List<TblSapMudurlukler> MudurlukList { get; set; }
    }

    public class HurdaMaliyetRaporuResponse : ResponseBase
    {
        public List<sp_Mes_V2_HurdaMaliyetRaporuGetir> HurdaMaliyetRaporList { get; set; }
    }

    public class HurdaMaliyetSearchRequest
    {
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public string SelectedMakineListesi { get; set; }
        public string SelectedMudurlukListesi { get; set; }
    }

    public class HurdaNegatifRaporuResponse : ResponseBase
    {
        public List<sp_Mes_V2_HurdaNegatifeDusenler> HurdaNegatifRaporList { get; set; }
    }

    public class HurdaNegatifSearchRequest
    {
        public decimal NegatifValue { get; set; }
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public string SelectedMakineListesi { get; set; }
        public string SelectedMudurlukListesi { get; set; }
    }

    public class HurdaNegatifDetayResponse : ResponseBase
    {
        public List<sp_Mes_V2_HurdaNegatifDetayGetir> HurdaNegatifDetayList { get; set; }
    }

    public class HurdaNegatifDetayRequest
    {
        public int StockId { get; set; }
        public int? TrackingId { get; set; }
        public int? MachineId { get; set; }
    }

    public class HurdaNegatiftekiIsEmirleriResponse : ResponseBase
    {
        public List<sp_Mes_V2_HurdaNegatiftekiIsEmirleri> HurdaNegatiftekiIsEmirleriList { get; set; }
    }

    public class HurdaNegatiftekiIsEmirleriRequest
    {
        public int ScrapNettingId { get; set; }
   
    }
}
