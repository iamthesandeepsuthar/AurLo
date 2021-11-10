using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class BtgoldLoanLeadExistingLoanDetail
    {
        public long Id { get; set; }
        public long LeadId { get; set; }
        public string BankName { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
        public decimal? JewelleryValuation { get; set; }
        public decimal? OutstandingAmount { get; set; }
        public decimal? BalanceTransferAmount { get; set; }
        public decimal? RequiredAmount { get; set; }
        public int? Tenure { get; set; }

        public virtual BtgoldLoanLead Lead { get; set; }
    }
}
