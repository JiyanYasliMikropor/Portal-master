using Mikroportal.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.RequestResponseClasses.ACS.Admin
{
    class AdminAddAuthorized
    {
    }


    public class AddAuthorizedServiceResponse : ResponseBase
    {
     

    }

    public class AddAuthorizedService
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
    }

    public class GetPersonelAndRoleListResponse : ResponseBase
    {

        public List<PersonelAndRoleList> PersonelAndRoleList { get; set; }

    }

    public class PersonelAndRoleList
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string CompanyName { get; set; }
        public bool? UserIsActive { get; set; }
        public bool? TeknikDokuman { get; set; }
        public bool? YedekParca { get; set; }

    }

    public class UserRoleChangeView
    {
        public int UserId { get; set; }
        public int Durum { get; set; }
        public string Type { get; set; }
        

    }

}
