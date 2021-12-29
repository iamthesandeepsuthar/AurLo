﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class GoldLoanFreshLeadStatusActionHistory
    {
        public GoldLoanFreshLeadStatusActionHistory()
        {
            BtgoldLoanLead = new HashSet<BtgoldLoanLead>();
            FreshLeadHlplcl = new HashSet<FreshLeadHlplcl>();
            GoldLoanFreshLead = new HashSet<GoldLoanFreshLead>();
        }

        public long Id { get; set; }
        public long LeadId { get; set; }
        public int? LeadStatus { get; set; }
        public long? ActionTakenByUserId { get; set; }
        public DateTime ActionDate { get; set; }
        public string Remarks { get; set; }

        public virtual UserMaster ActionTakenByUser { get; set; }
        public virtual GoldLoanFreshLead Lead { get; set; }
        public virtual ICollection<BtgoldLoanLead> BtgoldLoanLead { get; set; }
        public virtual ICollection<FreshLeadHlplcl> FreshLeadHlplcl { get; set; }
        public virtual ICollection<GoldLoanFreshLead> GoldLoanFreshLead { get; set; }
    }
}
