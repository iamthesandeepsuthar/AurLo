using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Shared.ContractModel
{
   public class OtpModel
    {
        public long Id { get; set; }
        public string Mobile { get; set; }
        public string Otp { get; set; }
        public long? UserId { get; set; }
        public bool IsVerify { get; set; }
        public DateTime SessionStartOn { get; set; }
        public DateTime? ExpireOn { get; set; }
    }
}
