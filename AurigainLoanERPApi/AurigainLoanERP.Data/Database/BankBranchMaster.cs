using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class BankBranchMaster
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string Ifsc { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string BranchEmailId { get; set; }
        public int BankId { get; set; }
        public string ConfigurationSettingJson { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual BankMaster Bank { get; set; }
    }
}
