using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Shared.ContractModel
{
    public class ChangePasswordModel
    {
        public long UserId  { get; set; }
        public string Password { get; set;}
    }
}
