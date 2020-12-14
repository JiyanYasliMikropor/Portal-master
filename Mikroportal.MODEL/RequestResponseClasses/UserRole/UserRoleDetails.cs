using Mikroportal.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.RequestResponseClasses.UserRole
{
    class UserRoleDetails : ResponseBase
    {

        public List<TblUsers> UserList { get; set; }


    }
}
