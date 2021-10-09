using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class UserSecurityDepositDetails
    {
        public UserSecurityDepositDetails()
        {
            UserDoorStepAgent = new HashSet<UserDoorStepAgent>();
        }

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

        public virtual PaymentMode PaymentMode { get; set; }
        public virtual UserMaster User { get; set; }
        public virtual ICollection<UserDoorStepAgent> UserDoorStepAgent { get; set; }
    }
}
