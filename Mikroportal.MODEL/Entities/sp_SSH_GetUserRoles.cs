using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    [Table("sp_SSH_GetUserRoles")]
    public class sp_SSH_GetUserRoles
    {
        [Key]
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string CompanyName { get; set; }
        public int? ACSService { get; set; }
        public int? ACSServiceTekinkDokuman { get; set; }
        public int? ACSServiceYedekParca { get; set; }
        public bool? UserIsActive { get; set; }
    }
}
