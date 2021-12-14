namespace AurigainLoanERP.Shared.ContractModel
{
    public class LoginModel
    {
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string Plateform { get; set; }
    }
    public class LoginResponseModel
    {
        public long UserId { get; set; }
        public int RoleId { get; set; }
        public int? RoleLevel { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set;}
    }
}
