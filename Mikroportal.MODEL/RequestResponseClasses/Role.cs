using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.RequestResponseClasses
{
    class Role
    {
    }
 

    public class RoleTypesResponse : ResponseBase
    {
        public IEnumerable<RoleTypes> RoleTypeList { get; set; }
    }

    public class RoleTypes
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }


}
