using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class UserOtp
    {
        public long Id { get; set; }
        public string Mobile { get; set; }
        public string Otp { get; set; }
        public long? UserId { get; set; }
        public bool IsVerify { get; set; }
        public DateTime SessionStartOn { get; set; }
        public DateTime? ExpireOn { get; set; }

        public virtual UserMaster User { get; set; }
    }
}
