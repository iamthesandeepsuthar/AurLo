using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class BankMaster
    {
        public BankMaster()
        {
            BankBranchMaster = new HashSet<BankBranchMaster>();
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string BankLogoUrl { get; set; }
        public string ContactNumber { get; set; }
        public string WebsiteUrl { get; set; }
        public string FaxNumber { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<BankBranchMaster> BankBranchMaster { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
