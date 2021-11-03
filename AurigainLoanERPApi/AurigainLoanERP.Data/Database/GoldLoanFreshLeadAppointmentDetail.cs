using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class GoldLoanFreshLeadAppointmentDetail
    {
        public int Id { get; set; }
        public int BankId { get; set; }
        public int BranchId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public TimeSpan? AppointmentTime { get; set; }
        public long GlfreshLeadId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual BankMaster Bank { get; set; }
        public virtual BankBranchMaster Branch { get; set; }
        public virtual GoldLoanFreshLead GlfreshLead { get; set; }
    }
}
