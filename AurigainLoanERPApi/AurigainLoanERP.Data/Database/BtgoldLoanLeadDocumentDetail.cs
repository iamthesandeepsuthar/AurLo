using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class BtgoldLoanLeadDocumentDetail
    {
        public long Id { get; set; }
        public long LeadId { get; set; }
        public string CustomerPhoto { get; set; }
        public string KycdocumentPoi { get; set; }
        public string KycdocumentPoa { get; set; }
        public string BlankCheque1 { get; set; }
        public string BlankCheque2 { get; set; }
        public string LoanDocument { get; set; }
        public string AggrementLastPage { get; set; }
        public string PromissoryNote { get; set; }
        public string AtmwithdrawalSlip { get; set; }
        public string ForeClosureLetter { get; set; }

        public virtual BtgoldLoanLead Lead { get; set; }
    }
}
