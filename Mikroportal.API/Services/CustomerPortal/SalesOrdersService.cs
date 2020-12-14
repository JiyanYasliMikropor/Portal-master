using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mikroportal.MODEL.RequestResponseClasses.CustomerPortal;
using Mikroportal.MODEL.ServiceContracts.CustomerPortal;
using Mikroportal.API.Services;
using AutoMapper;
using System.Net.Http;
using Mikroportal.MODEL.Entities;
using System.Configuration;
using System.ServiceModel;
using Microsoft.IdentityModel.Protocols;
using System.Reflection;

namespace Mikroportal.API.Services.CustomerPortal
{
    public class SalesOrdersService : ISalesOrdersService
    {
        public WsSalesOrderDetails_OBService.SI_SalesOrderDetails_OBClient client;
        //public WsSalesOrderDetails_OBService.SI_SalesOrderDetails_OBRequest requset;
        public WsSalesOrderDetails_OBService.SI_SalesOrderDetails_OBResponse response;
        public SalesOrdersDetailsRequest salesOrderRequest;

        public SalesOrdersService()
        {
            salesOrderRequest = new SalesOrdersDetailsRequest();
            client = new WsSalesOrderDetails_OBService.SI_SalesOrderDetails_OBClient(0);
            //requset = new WsSalesOrderDetails_OBService.SI_SalesOrderDetails_OBRequest();
            response = new WsSalesOrderDetails_OBService.SI_SalesOrderDetails_OBResponse();
           
        }

        public async Task<SalesOrdersDetailsResponse>  GetSalesOrders (SalesOrdersRequestParams requestParams)
        {
            try {
                BasicHttpBinding binding = new BasicHttpBinding();
                binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
                EndpointAddress endPoint = new EndpointAddress(" http://mkrppod01.mikroportal.com:50000/XISOAPAdapter/MessageServlet?senderParty=&senderService=BC_MIKROPORTAL&receiverParty=&receiverService=&interface=SI_SalesOrderDetails_OB&interfaceNamespace=http://mikroportalintegration/services");
                client.Endpoint.Address = endPoint;
                client.Endpoint.Binding = binding;

                client.ClientCredentials.UserName.UserName = "portal-comm";
                client.ClientCredentials.UserName.Password = "Mm!12345";



                WsSalesOrderDetails_OBService.SI_SalesOrderDetails_OBRequest request = new WsSalesOrderDetails_OBService.SI_SalesOrderDetails_OBRequest();

            request.IV_BASL_TRH = requestParams.IV_BASL_TRH;
            request.IV_BITS_TRH = requestParams.IV_BITS_TRH;
            request.IV_MUSTERI = requestParams.IV_MUSTERI;

            var serviceResponse = await client.SI_SalesOrderDetails_OBAsync(request);
              
            return ConvertToOrderDetailsResponse(serviceResponse);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());

                throw; }

        }
    
        public SalesOrdersDetailsResponse ConvertToOrderDetailsResponse(WsSalesOrderDetails_OBService.SI_SalesOrderDetails_OBResponse response)
        {
            SalesOrdersDetailsResponse convertedresponse = new SalesOrdersDetailsResponse();

            //SalesOrdersDetailsResponseItem salesOrdersDetailsResponseItem = new SalesOrdersDetailsResponseItem();
            SalesOrdersDetailsResponseObject salesOrdersDetailsResponseObject;

            convertedresponse.ES_MSG.MESSAGE = response.ES_MSG.MESSAGE;
            convertedresponse.ES_MSG.TYPE = response.ES_MSG.TYPE;
            foreach (var idata in response.ET_DATA) {
                salesOrdersDetailsResponseObject = new SalesOrdersDetailsResponseObject();
                salesOrdersDetailsResponseObject.ZSD_S_ORDER_STAT.cONF_TARIHIField = idata.CONF_TARIHI;
                salesOrdersDetailsResponseObject.ZSD_S_ORDER_STAT.fIYATField = idata.FIYAT;
                salesOrdersDetailsResponseObject.ZSD_S_ORDER_STAT.fIYATFieldSpecified = idata.FIYATSpecified;
                salesOrdersDetailsResponseObject.ZSD_S_ORDER_STAT.mALZEMEField = idata.MALZEME;
                salesOrdersDetailsResponseObject.ZSD_S_ORDER_STAT.mIKTARField = idata.MIKTAR;
                salesOrdersDetailsResponseObject.ZSD_S_ORDER_STAT.mIKTARFieldSpecified = idata.MIKTARSpecified;
                salesOrdersDetailsResponseObject.ZSD_S_ORDER_STAT.mUSTERIField = idata.MUSTERI;
                salesOrdersDetailsResponseObject.ZSD_S_ORDER_STAT.pbField = idata.PB;
                salesOrdersDetailsResponseObject.ZSD_S_ORDER_STAT.sAS_NOField = idata.SAS_NO;
                salesOrdersDetailsResponseObject.ZSD_S_ORDER_STAT.sEVK_DURUMUField = idata.SEVK_DURUMU;
                salesOrdersDetailsResponseObject.ZSD_S_ORDER_STAT.sEVK_MIKTARIField = idata.SEVK_MIKTARI;
                salesOrdersDetailsResponseObject.ZSD_S_ORDER_STAT.sEVK_MIKTARIFieldSpecified = idata.SEVK_MIKTARISpecified;
                salesOrdersDetailsResponseObject.ZSD_S_ORDER_STAT.sEVK_TARIHIField = idata.SEVK_TARIHI;
                salesOrdersDetailsResponseObject.ZSD_S_ORDER_STAT.sIP_DURUMField = idata.SIP_DURUM;
                salesOrdersDetailsResponseObject.ZSD_S_ORDER_STAT.sIP_KALEMField = idata.SIP_KALEM;
                salesOrdersDetailsResponseObject.ZSD_S_ORDER_STAT.sIP_NOField = idata.SIP_NO;
                salesOrdersDetailsResponseObject.ZSD_S_ORDER_STAT.sIP_ONAY_DURUMUField = idata.SIP_ONAY_DURUMU;
                salesOrdersDetailsResponseObject.ZSD_S_ORDER_STAT.sIRA_NOField = idata.SIRA_NO;
                salesOrdersDetailsResponseObject.ZSD_S_ORDER_STAT.uPKField = idata.UPK;
                convertedresponse.ET_DATA.Add(salesOrdersDetailsResponseObject);
            }
            return convertedresponse;
        }
    }

    //public class AutoMapping:Profile
    //{
    //    public AutoMapping()
    //    {
    //        //buradan devam edilecek..TODO:
    //        //https://www.youtube.com/watch?v=GCbmUpyzkQw
    //        CreateMap<WsSalesOrderDetails_OBService.SI_SalesOrderDetails_OBRequest, SalesOrdersDetailsRequest>();
    //    }

    //}

}
