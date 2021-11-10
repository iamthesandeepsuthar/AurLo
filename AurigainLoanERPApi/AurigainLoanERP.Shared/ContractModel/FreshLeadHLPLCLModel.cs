using System;
using System.Collections.Generic;
using System.Text;

namespace AurigainLoanERP.Shared.ContractModel
{
   public class FreshLeadHLPLCLModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public double LoanAmount { get; set; }
        public bool LeadType { get; set;}
        public double? AnnualIncome { get; set; }
        public long LeadSourceByUserId { get; set; }
        public int ProductId { get; set; }
        public string EmployeeType { get; set; }
        public int? NoOfItr { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        }
}
