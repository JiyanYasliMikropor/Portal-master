using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    public partial class TblHatalar
    {
        public long HataId { get; set; }
        public string HataTanimi { get; set; }
        public int? IlgiliUserHataDuzeltmeRoleId { get; set; }
        public long? ProductionOrderId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
