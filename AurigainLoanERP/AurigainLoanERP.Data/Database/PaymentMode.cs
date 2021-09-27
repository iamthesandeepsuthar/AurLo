using System;
using System.Collections.Generic;

#nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class PaymentMode
    {
        public PaymentMode()
        {
            SecurityDepositDetails = new HashSet<SecurityDepositDetail>();
        }

        public int Id { get; set; }
        public string Mode { get; set; }
        public long? MinimumValue { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<SecurityDepositDetail> SecurityDepositDetails { get; set; }
    }
}
