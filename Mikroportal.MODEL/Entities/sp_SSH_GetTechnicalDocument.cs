using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mikroportal.MODEL.Entities
{
    [Table("sp_SSH_GetTechnicalDocument")]
    public partial class sp_SSH_GetTechnicalDocument
    {
        [Key]
        public long ID { get; set; }
        public int? ENGINEERINGDRAWINGHISTORYFID { get; set; }
        public int TYPEFID { get; set; }
        public int? SUBTYPEFID { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string MATERIAL { get; set; }
        public string REVISIONNO { get; set; }
        public string FILENAME { get; set; }
        public string CONTENTTYPE { get; set; }
        public long? FILESIZE { get; set; }
        public string FILEPATH { get; set; }
        public bool? ISSECRET { get; set; }
        public int? CREATEDBY { get; set; }
        public DateTime? CREATEDDATE { get; set; }
        public int? MODIFIEDBY { get; set; }
        public DateTime? MODIFIEDDATE { get; set; }
        public int? GROUPFID { get; set; }
        public string PhysicalPath { get; set; }
    }
}
