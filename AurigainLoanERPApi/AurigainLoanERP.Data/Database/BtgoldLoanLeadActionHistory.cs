using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class BtgoldLoanLeadActionHistory
    {
        public long Id { get; set; }
        public long LeadId { get; set; }
        public int? LeadStatus { get; set; }
        public long? AssignFromUserId { get; set; }
        public long? AssignToUserId { get; set; }
        public DateTime ActionDate { get; set; }

        public virtual UserMaster AssignFromUser { get; set; }
        public virtual UserMaster AssignToUser { get; set; }
        public virtual BtgoldLoanLead Lead { get; set; }
    }
}
