using Mikroportal.MODEL.RequestResponseClasses.MES;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.ServiceContracts.MES
{
    public interface IMesCogiService
    {
        //public GetMesCogiResponse BildirimleriGoster(MesCogiRequest request);

        public GetMesCogiResponse BildirimleriGoster(int UserId);
        public GetCogiIslemeAlResponse CogiIslemeAl(CogiIslemeAlRequest request);
        public GetCogiSilResponse CogiIslemSil(CogiSilRequest request);
        
    }


 
}
