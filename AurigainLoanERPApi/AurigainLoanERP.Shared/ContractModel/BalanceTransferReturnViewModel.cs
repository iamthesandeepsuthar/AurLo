using System;
using System.Collections.Generic;
using System.Text;

namespace AurigainLoanERP.Shared.ContractModel
{
   public  class BalanceTransferReturnViewModel
    {
        public BalanceTransferReturnViewModel()
        {            
            Jewelleries = new List<BtGoldLoanLeadJewelleryDetailViewModel>();           
            ExistingLoanDetail = new BtGoldLoanLeadExistingLoanViewModel();
            BalanceTransferReturn = new BalanceTranferReturnViewModel();
        }
        public long Id { get; set; }
        public int ProductId { get; set; }
        public string ProductCategoryName { get; set; }
        public string FullName { get; set; }       
        public string Mobile { get; set; }   
        public decimal LoanAmount { get; set; }
        public string LoanCaseNumber { get; set; }
        public long LeadSourceByuserId { get; set; }      
        public string LeadSourceByuserName { get; set; }
        public string ProductName { get; set; }        
        public bool IsInternalLead { get; set; }             
        public BtGoldLoanLeadExistingLoanViewModel ExistingLoanDetail { get; set; }
        public List<BtGoldLoanLeadJewelleryDetailViewModel> Jewelleries { get; set; }   
        public BalanceTranferReturnViewModel BalanceTransferReturn { get; set;}

    }
    public class BalanceTranferReturnViewModel 
    {
        public long Id { get; set; }
        public long LeadId { get; set; }
        public bool? AmountPainToExistingBank { get; set; }
        public bool? GoldReceived { get; set; }
        public bool? GoldSubmittedToBank { get; set; }
        
    }
    public class BalanceTranferReturnPostModel
    {
        public long BtReturnId { get; set; }
        public long LeadId { get; set; }
        public bool? AmountPainToExistingBank { get; set; }
        public bool? GoldReceived { get; set; }
        public bool? GoldSubmittedToBank { get; set; }
        public bool? LoanDisbursment { get; set; }
        public string LoanAccountNumber { get; set; }
        public string BankName { get; set; }
        public int? PaymentMethod { get; set; }
        public string CustomerName { get; set; }
        public string Remarks { get; set; }
        public string UtrNumber { get; set; }
        public decimal? AmountReturn { get; set; }
        public string FinalPaymentDate { get; set; }
        public decimal LoanAmount {get;set;}
        public BalanceTransferReturnBankChequeDetail ChequeDetail { get; set; }
    }
}
