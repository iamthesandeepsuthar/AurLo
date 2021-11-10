using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class BtgoldLoanLeadAppointmentDetail
    {
        public long Id { get; set; }
        public long LeadId { get; set; }
        public int? BranchId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public TimeSpan? AppointmentTime { get; set; }

        public virtual BankBranchMaster Branch { get; set; }
        public virtual BtgoldLoanLead Lead { get; set; }
    }
}
