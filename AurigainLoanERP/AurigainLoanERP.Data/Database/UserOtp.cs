using System;
using System.Collections.Generic;

#nullable disable

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
