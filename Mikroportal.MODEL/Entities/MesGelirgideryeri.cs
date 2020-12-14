using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    public partial class MesGelirgideryeri
    {
        public int Id { get; set; }
        public int? GustoId { get; set; }
        public int? UstId { get; set; }
        public string GgyKodu { get; set; }
        public string GgyAdi { get; set; }
    }
}
