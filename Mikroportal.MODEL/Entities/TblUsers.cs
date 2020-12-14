using System;
using System.Collections.Generic;

namespace Mikroportal.MODEL.Entities
{
    public partial class TblUsers
    {
        public int UserId { get; set; }
        public Guid? UserGuid { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserAccount { get; set; }
        public string UserPassword { get; set; }
        public string CompanyName { get; set; }
        public int? CountryId { get; set; }
        public int? CurrencyUnitId { get; set; }
        public int? DiscountId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool? IsActiveForCrosslist { get; set; }
        public bool? IsAdminForCrosslist { get; set; }
        public bool? CanSeePriceForCrosslist { get; set; }
        public bool? IsDefined { get; set; }
        public int? UserResourceId { get; set; }
    }
}
