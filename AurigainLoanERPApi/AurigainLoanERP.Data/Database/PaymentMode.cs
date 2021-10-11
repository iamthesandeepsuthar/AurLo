using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class PaymentMode
    {
        public PaymentMode()
        {
            UserSecurityDepositDetails = new HashSet<UserSecurityDepositDetails>();
        }

        public int Id { get; set; }
        public string Mode { get; set; }
        public long? MinimumValue { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<UserSecurityDepositDetails> UserSecurityDepositDetails { get; set; }
    }
}
