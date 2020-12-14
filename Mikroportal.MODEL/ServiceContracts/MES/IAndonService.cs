using Mikroportal.MODEL.RequestResponseClasses.MES;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.ServiceContracts.MES
{
    public interface IAndonService
    {
        public GetAndonListResponse GetAndonList();
    }
}
