using System;

namespace AurigainLoanERP.Shared.ContractModel
{
    public class UserSecurityDepositViewModel
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public int PaymentModeId { get; set; }
        public int? TransactionStatus { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreditDate { get; set; }
        public string ReferanceNumber { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public class UserSecurityDepositPostModel
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public int PaymentModeId { get; set; }
        //public int? TransactionStatus { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreditDate { get; set; }
        public string ReferanceNumber { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public bool? IsActive { get; set; }

    }
}
