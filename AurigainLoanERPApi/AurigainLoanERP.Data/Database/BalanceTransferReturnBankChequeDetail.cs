using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class BalanceTransferReturnBankChequeDetail
    {
        public long Id { get; set; }
        public long BtreturnId { get; set; }
        public string ChequeNumber { get; set; }
        public string ChequeImage { get; set; }

        public virtual BalanceTransferLoanReturn Btreturn { get; set; }
    }
}
