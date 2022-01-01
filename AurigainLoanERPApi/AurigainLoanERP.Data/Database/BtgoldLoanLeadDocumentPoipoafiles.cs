using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class BtgoldLoanLeadDocumentPoipoafiles
    {
        public long Id { get; set; }
        public long LeadId { get; set; }
        public bool IsPoi { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Path { get; set; }

        public virtual BtgoldLoanLead Lead { get; set; }
    }
}
