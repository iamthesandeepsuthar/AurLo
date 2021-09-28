using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Data.ContractModel
{
    public class UserNomineeModel
    {       
            public long Id { get; set; }
            public string NamineeName { get; set; }
            public string RelationshipWithNominee { get; set; }
            public long UserId { get; set; }
            public bool? IsActive { get; set; }
            public bool IsDelete { get; set; }
            public DateTime CreatedOn { get; set; }
            public DateTime? ModifiedOn { get; set; }
            public long? CreatedBy { get; set; }
            public long? ModifiedBy { get; set; }           
        
    }
}
