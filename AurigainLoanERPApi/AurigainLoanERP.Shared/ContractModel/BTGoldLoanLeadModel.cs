using System;
using System.Collections.Generic;
using System.Text;

namespace AurigainLoanERP.Shared.ContractModel
{
    public class BTGoldLoanLeadViewModel  : BTGoldLoanLeadPostModel
    {
        public int LeadSourceByuserName { get; set; }
    }
    public class BTGoldLoanLeadPostModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Profession { get; set; }
        public string Mobile { get; set; }
        public string EmailId { get; set; }
        public string SecondaryMobile { get; set; }
        public string Purpose { get; set; }
        public long LeadSourceByuserId { get; set; }

    }
   
}
