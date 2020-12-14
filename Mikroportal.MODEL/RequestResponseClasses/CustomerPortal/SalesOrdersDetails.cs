using System;
using System.Collections.Generic;
using System.Text;
using Mikroportal.MODEL.Entities;
namespace Mikroportal.MODEL.RequestResponseClasses.CustomerPortal
{
     class SalesOrdersDetails
    {
    }

    public class SalesOrdersDetailsRequest
    {
        public string IV_BASL_TRH { get; set; }

        public string IV_BITS_TRH { get; set; }

        public string IV_MUSTERI { get; set; }
    }

    public class SalesOrdersDetailsResponse
    {
        public SalesOrdersDetailsResponse() {
            ES_MSG = new ZSD_S_MSG();
            ET_DATA = new List<SalesOrdersDetailsResponseObject>();
        }

        public ZSD_S_MSG ES_MSG { get; set; }
        public List<SalesOrdersDetailsResponseObject> ET_DATA { get; set; }
    }

    public class SalesOrdersDetailsResponseObject
    {
        public SalesOrdersDetailsResponseObject() {
            ZSD_S_ORDER_STAT = new SalesOrdersDetailsResponseItem();
        }
        public SalesOrdersDetailsResponseItem ZSD_S_ORDER_STAT { get; set; }
    }

    public class SalesOrdersDetailsResponseItem {
        //ET_DATA EtData { get; set; }
        public string sIRA_NOField { get; set; }
        public string sIP_NOField { get; set; }
        public string mUSTERIField { get; set; }
        public string sIP_ONAY_DURUMUField { get; set; }
        public string sIP_KALEMField { get; set; }
        public string mALZEMEField { get; set; }
        public string sAS_NOField { get; set; }
        public string uPKField { get; set; }
        public decimal mIKTARField { get; set; }
        public bool mIKTARFieldSpecified { get; set; }
        public decimal sEVK_MIKTARIField { get; set; }
        public bool sEVK_MIKTARIFieldSpecified { get; set; }
        public decimal fIYATField { get; set; }
        public bool fIYATFieldSpecified { get; set; }
        public string pbField { get; set; }
        public string sIP_DURUMField { get; set; }
        public string cONF_TARIHIField { get; set; }
        public string sEVK_TARIHIField { get; set; }
        public string sEVK_DURUMUField { get; set; }

    }

    public class ZSD_S_MSG
    {
        //  ES_MSG EsMsg { get; set; }
        public string MESSAGE { get; set; }
        public string TYPE { get; set; }

    }

}
