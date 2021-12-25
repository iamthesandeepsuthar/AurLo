namespace AurigainLoanERP.Shared.ContractModel
{
    public class OtpRequestModel
    {
        public string MobileNumber { get; set; }
        public bool IsResendOtp { get; set; }        
    }
    public class OtpVerifiedModel
    {
        public string MobileNumber { get; set; }
        public string Otp { get; set; }
        public long UserId { get; set;}

    }
    
}
