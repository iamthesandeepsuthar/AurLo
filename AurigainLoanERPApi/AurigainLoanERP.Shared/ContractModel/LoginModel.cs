using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Shared.ContractModel
{
    public class LoginModel
    {
        public string MobileNumber { get; set;}
        public string Password { get; set;}
        public string Plateform { get; set; }
    }
    public class LoginResponseModel
    {
        public long UserId { get; set;}
        public int RoleId { get; set;}
        public string Token { get; set;}
        public string UserName { get; set; }
    }
}
