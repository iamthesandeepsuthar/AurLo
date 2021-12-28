using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class Purpose
    {
        public Purpose()
        {
            BtgoldLoanLead = new HashSet<BtgoldLoanLead>();
            GoldLoanFreshLead = new HashSet<GoldLoanFreshLead>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<BtgoldLoanLead> BtgoldLoanLead { get; set; }
        public virtual ICollection<GoldLoanFreshLead> GoldLoanFreshLead { get; set; }
    }
}
