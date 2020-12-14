using Mikroportal.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.RequestResponseClasses.ACS
{
    class ReplacementPart
    {
    }

    public class GetReplacementPartPriceByPartNoResponse : ResponseBase
    {
        public List<TblSshReplacementPartPrice> PartPriceList { get; set; }
    }

    public class SerialNoCheckReplacementPartResponse : ResponseBase
    {
        public int? PartNo { get; set; }
        public string Path { get; set; }
        public string idImage { get; set; }
        public string DocummentType { get; set; }
        public string DocummentRevisionno { get; set; }

    }
    public class ModelNoCheckReplacementPartResponse : ResponseBase
    {
        public int? PartNo { get; set; }
        public string Path { get; set; }
        public string idImage { get; set; }
        public string DocummentType { get; set; }
        public string DocummentRevisionno { get; set; }

    }
}
    
