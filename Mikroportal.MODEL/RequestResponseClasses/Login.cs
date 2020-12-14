using Mikroportal.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mikroportal.MODEL.RequestResponseClasses
{
    class Login
    {
    }


    public class LoginRequest
    {
        //[Required]
        public string UserAccount { get; set; }
        //[Required]
        public string UserPassword { get; set; }
    }

    public class LoginResponse : ResponseBase
    {
        public int UserId { get; set; }
        public Guid? UserGuid { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserAccount { get; set; }
        public string UserPassword { get; set; }
        public int? CountryId { get; set; }
        public int? CurrencyUnitId { get; set; }
        public int? DiscountId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsActiveForCrosslist { get; set; }
        public bool? IsAdminForCrosslist { get; set; }
        public bool? CanSeePriceForCrosslist { get; set; }
        public bool? IsDefined { get; set; }
        public int? UserResourceId { get; set; }
        public string Token { get; set; }
       public List<RoleTypes> UserRoles { get; set; }
    }

    public class AddAuthorizedServiceView : ResponseBase
    {
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }

    }


    public class SavePasswordResponse : ResponseBase
    {
        public string UserPassword { get; set; }
    }

}
