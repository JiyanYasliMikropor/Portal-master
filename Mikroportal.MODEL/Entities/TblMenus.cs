using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    public partial class TblMenus
    {
        public int MenuId { get; set; }
        public int? RoleId { get; set; }
        public string MenuName { get; set; }
        public string MenuRoot { get; set; }
        public int? MasterMenuId { get; set; }
        public string MenuValueForProjects { get; set; }
    }
}
