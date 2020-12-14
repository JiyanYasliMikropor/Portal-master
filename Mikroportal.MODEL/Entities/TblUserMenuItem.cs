using System;
using System.Collections.Generic;

namespace Mikroportal.MODEL.Entities
{
    public partial class TblUserMenuItem
    {
        public int UserMenuItemId { get; set; }
        public int? UserId { get; set; }
        public int? MenuItemId { get; set; }
    }
}
