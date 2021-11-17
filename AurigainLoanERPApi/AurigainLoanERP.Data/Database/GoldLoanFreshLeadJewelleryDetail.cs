using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class GoldLoanFreshLeadJewelleryDetail
    {
        public int Id { get; set; }
        public int PreferredLoanTenure { get; set; }
        public int JewelleryTypeId { get; set; }
        public double Quantity { get; set; }
        public double? Weight { get; set; }
        public int? Karat { get; set; }
        public long GlfreshLeadId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual JewellaryType JewelleryType {get;set;}
        public virtual GoldLoanFreshLead GlfreshLead { get; set; }
    }
}
