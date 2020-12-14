using System;
using System.Collections.Generic;

namespace Mikroportal.MODEL.Entities
{
    public partial class TblRoles
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
