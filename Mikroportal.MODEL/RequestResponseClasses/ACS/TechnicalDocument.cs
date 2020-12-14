using Microsoft.AspNetCore.Http;
using Mikroportal.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.RequestResponseClasses.ACS
{
    class TechnicalDocument
    {
    }

    public class TechnicalDocumentResponse : ResponseBase
    {
        public IEnumerable<sp_SSH_GetSkuBySerial> MachineDetail { get; set; }
        public IEnumerable<sp_SSH_GetTechnicalDocument> DocumentListesi { get; set; }
        public bool isModal { get; set; }
        public string Path { get; set; }

    }
}
