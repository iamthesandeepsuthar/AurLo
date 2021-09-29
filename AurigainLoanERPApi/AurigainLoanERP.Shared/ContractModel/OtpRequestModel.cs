using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Shared.ContractModel
{
    public class OtpRequestModel
    {
        public long Id { get; set;}
        public string MobileNumber { get; set; }
        public bool IsResendOtp { get; set; }
    }
    public class OtpVerifiedModel
    {
        public string MobileNumber { get; set; }
        public string Otp { get; set; }
    }
}
