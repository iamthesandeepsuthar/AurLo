using System;
using System.Collections.Generic;
using System.Text;

namespace AurigainLoanERP.Shared.ContractModel
{
    public class ProductModel
    {
        public ProductModel() 
        {
            
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public int ProductCategoryId { get; set; }
        public int BankId { get; set;}
        public double? MinimumAmount { get; set; }
        public double? MaximumAmount { get; set; }
        public double? InterestRate { get; set; }
        public double? ProcessingFee { get; set; }
        public bool? InterestRateApplied { get; set; }
        public double? MinimumTenure { get; set; }
        public double? MaximumTenure { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedDate { get; set; }       
        public long? CreatedBy { get; set; } 
        public string ProductCategoryName { get; set;}
        public string BankName { get; set;}
        
    }
    public class DDLProductModel 
    {
        public int Id { get; set;}
        public string Name { get; set;}
    }
}
