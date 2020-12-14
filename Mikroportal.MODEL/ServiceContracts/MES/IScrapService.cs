using Mikroportal.MODEL.RequestResponseClasses;
using Mikroportal.MODEL.RequestResponseClasses.MES;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.ServiceContracts.MES
{
    public interface IScrapService
    {

        public HurdaMaliyetRaporuResponse HurdaMaliyetRaporuGetir(HurdaMaliyetSearchRequest request);
        public HurdaNegatifRaporuResponse HurdaNegatifeDusenlerRaporuGetir(HurdaNegatifSearchRequest request);
        public HurdaNegatifDetayResponse HurdaNegatifDetayGetir(HurdaNegatifDetayRequest request);
        public HurdaNegatiftekiIsEmirleriResponse HurdaNegatiftekiIsEmirleriGetir(HurdaNegatiftekiIsEmirleriRequest request);
      

        public GetAllMachinesResponse GetAllMachines();
        public GetAllMudurlukResponse GetAllMudurluk();
    }
}
