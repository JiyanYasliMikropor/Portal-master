using Mikroportal.MODEL.Entities;
using Mikroportal.MODEL.RequestResponseClasses.CustomerPortal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Mikroportal.MODEL.ServiceContracts.CustomerPortal
{
    public interface ISalesOrdersService
    {
        public Task<SalesOrdersDetailsResponse> GetSalesOrders(SalesOrdersRequestParams requestParams);

      
    }
}
