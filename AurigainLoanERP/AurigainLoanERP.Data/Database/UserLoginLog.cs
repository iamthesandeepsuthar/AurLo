using System;
using System.Collections.Generic;

#nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class UserLoginLog
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public DateTime LoggedInTime { get; set; }
        public DateTime LoggedOutTime { get; set; }

        public virtual UserMaster User { get; set; }
    }
}
