using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    public partial class TblSshReplacementPart
    {
        public int ReplacementPartId { get; set; }
        public int? PartNo { get; set; }
        public string Sad { get; set; }
        public int? StokKoduId { get; set; }
        public int? IzlemeKoduId { get; set; }
    }
}
