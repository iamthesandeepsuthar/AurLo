using System;
using System.Collections.Generic;

#nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class UserBank
    {
        public long Id { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string Ifsccode { get; set; }
        public string Address { get; set; }
        public long UserId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }

        public virtual UserMaster User { get; set; }
    }
}
