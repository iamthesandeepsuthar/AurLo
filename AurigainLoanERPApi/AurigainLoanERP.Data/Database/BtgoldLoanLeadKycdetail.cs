using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class BtgoldLoanLeadKycdetail
    {
        public long Id { get; set; }
        public long LeadId { get; set; }
        public int PoidocumentTypeId { get; set; }
        public string PoidocumentNumber { get; set; }
        public int PoadocumentTypeId { get; set; }
        public string PoadocumentNumber { get; set; }

        public virtual BtgoldLoanLead Lead { get; set; }
        public virtual DocumentType PoadocumentType { get; set; }
        public virtual DocumentType PoidocumentType { get; set; }
    }
}
