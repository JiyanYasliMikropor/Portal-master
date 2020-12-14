using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    public partial class TblFmShift
    {
        public int ShiftId { get; set; }
        public string Name { get; set; }
        public int? ShiftTime { get; set; }
    }
}
