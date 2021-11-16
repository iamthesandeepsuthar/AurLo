using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class FreshLeadHlplcl
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public bool LeadType { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public double LoanAmount { get; set; }
        public double? AnnualIncome { get; set; }
        public long LeadSourceByUserId { get; set; }
        public long CustomerUserId { get; set; }
        public int ProductId { get; set; }
        public string EmployeeType { get; set; }
        public int? NoOfItr { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual UserMaster CustomerUser { get; set; }
        public virtual UserMaster LeadSourceByUser { get; set; }
        public virtual Product Product { get; set; }
    }
}
