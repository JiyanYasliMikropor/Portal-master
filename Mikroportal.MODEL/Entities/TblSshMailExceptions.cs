using System;
using System.Collections.Generic;

namespace Mikroportal.MODEL.Entities
{
    public partial class TblSshMailExceptions
    {
        public int MailId { get; set; }
        public string ExceptionDetail { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int? MachineId { get; set; }
    }
}
