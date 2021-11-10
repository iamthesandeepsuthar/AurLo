using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class JewellaryType
    {
        public JewellaryType()
        {
            BtgoldLoanLeadJewelleryDetail = new HashSet<BtgoldLoanLeadJewelleryDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }

        public virtual ICollection<BtgoldLoanLeadJewelleryDetail> BtgoldLoanLeadJewelleryDetail { get; set; }
    }
}
