using System;

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
        public bool LeadType { get; set; }
        public double? AnnualIncome { get; set; }
        public long LeadSourceByUserId { get; set; }
        public string LeadSourceByUserName { get; set; }      
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCategoryName { get; set; }
        public string EmployeeType { get; set; }
        public int? NoOfItr { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string LeadStatus { get; set;}
        public string Pincode { get; set; }
        public long? AreaPincodeId { get; set;}
        public long? CustomerUserId { get; set;}
        public string AreaLocation { get; set;}
        
    }

}
