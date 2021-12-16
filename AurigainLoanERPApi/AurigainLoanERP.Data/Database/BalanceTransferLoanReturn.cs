using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class BalanceTransferLoanReturn
    {
        public BalanceTransferLoanReturn()
        {
            BalanceTransferReturnBankChequeDetail = new HashSet<BalanceTransferReturnBankChequeDetail>();
        }

        public long Id { get; set; }
        public long LeadId { get; set; }
        public bool? AmountPaidToExistingBank { get; set; }
        public bool? GoldReceived { get; set; }
        public bool? GoldSubmittedToBank { get; set; }
        public bool? LoanDisbursement { get; set; }
        public string LoanAccountNumber { get; set; }
        public string BankName { get; set; }
        public string CustomerName { get; set; }
        public int? PaymentMethod { get; set; }
        public string UtrNumber { get; set; }
        public string Remarks { get; set; }
        public decimal? AmountReturn { get; set; }
        public decimal? PaymentAmount { get; set; }

        public virtual BtgoldLoanLead Lead { get; set; }
        public virtual ICollection<BalanceTransferReturnBankChequeDetail> BalanceTransferReturnBankChequeDetail { get; set; }
    }
}
