using System;
using System.Collections.Generic;

#nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class UserReportingPerson
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ReportingUserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }

        public virtual UserMaster ReportingUser { get; set; }
        public virtual UserMaster User { get; set; }
    }
}
