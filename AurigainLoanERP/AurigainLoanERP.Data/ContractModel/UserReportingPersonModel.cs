using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Data.ContractModel
{
    public class UserReportingPersonModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ReportingUserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public virtual UserModel ReportingUser { get; set; }
    }
}
