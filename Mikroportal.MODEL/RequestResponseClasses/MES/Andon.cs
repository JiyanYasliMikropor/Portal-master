using Mikroportal.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.RequestResponseClasses.MES
{
    public  class GetAndonListResponse : ResponseBase
    {
        public List<MSP_ANDON_GETPRODUCTIONLINE_SERIAL> andonList { get; set; }
    }
          
    
}
