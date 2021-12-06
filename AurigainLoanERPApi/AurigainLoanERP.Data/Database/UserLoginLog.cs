using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class UserLoginLog
    {
        public long Id { get; set; }
        public DateTime LoggedInTime { get; set; }
        public DateTime LoggedOutTime { get; set; }
        public string Mobile { get; set; }
        public long UserId { get; set; }

        public virtual UserMaster User { get; set; }
    }
}
