using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    public partial class MesEngineeringdrawingsubtype
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int? DrawingTypeId { get; set; }
        public DateTime? Createddate { get; set; }
        public int? Createdby { get; set; }
        public int? Modifiedby { get; set; }
        public DateTime? Modifieddate { get; set; }
        public string Description { get; set; }
    }
}
