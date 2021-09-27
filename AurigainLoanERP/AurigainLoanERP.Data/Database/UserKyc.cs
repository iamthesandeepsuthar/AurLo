using System;
using System.Collections.Generic;

#nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class UserKyc
    {
        public long Id { get; set; }
        public string Kycnumber { get; set; }
        public int KycdocumentTypeId { get; set; }
        public long UserId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }

        public virtual DocumentType KycdocumentType { get; set; }
        public virtual UserMaster User { get; set; }
    }
}
