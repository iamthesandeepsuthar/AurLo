using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class BtgoldLoanLeadJewelleryDetail
    {
        public long Id { get; set; }
        public long LeadId { get; set; }
        public int? JewelleryTypeId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Weight { get; set; }
        public int? Karats { get; set; }

        public virtual JewellaryType JewelleryType { get; set; }
        public virtual BtgoldLoanLead Lead { get; set; }
    }
}
