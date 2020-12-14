using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.RequestResponseClasses.ACS
{
    class Guarantee
    {
    }

    public class GuaranteeView
    {
        public string UserId { get; set; }
        public string SkuCode { get; set; }
        public string TracingName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string BanDate { get; set; }
        public string Comment { get; set; }
        public string SerialNo { get; set; }
        public string FileNames { get; set; }
        public List<IFormFile> files { get; set; }

    }
}
