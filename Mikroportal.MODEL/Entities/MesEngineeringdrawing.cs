using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.Entities
{
    public partial class MesEngineeringdrawing
    {
        public long Id { get; set; }
        public int? Engineeringdrawinghistoryfid { get; set; }
        public int Typefid { get; set; }
        public int? Subtypefid { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Material { get; set; }
        public string Revisionno { get; set; }
        public byte[] Filecontent { get; set; }
        public string Filename { get; set; }
        public string Contenttype { get; set; }
        public long? Filesize { get; set; }
        public string Filepath { get; set; }
        public bool? Issecret { get; set; }
        public int? Createdby { get; set; }
        public DateTime? Createddate { get; set; }
        public int? Modifiedby { get; set; }
        public DateTime? Modifieddate { get; set; }
        public int? Groupfid { get; set; }
        public string PhysicalPath { get; set; }
    }
}
