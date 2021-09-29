using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

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
