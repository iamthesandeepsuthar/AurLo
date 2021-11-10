using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class BtgoldLoanLeadAddressDetail
    {
        public long Id { get; set; }
        public long LeadId { get; set; }
        public string Address { get; set; }
        public long? AeraPincodeId { get; set; }
        public string CorrespondAddress { get; set; }
        public long? CorrespondAeraPincodeId { get; set; }

        public virtual PincodeArea AeraPincode { get; set; }
        public virtual PincodeArea CorrespondAeraPincode { get; set; }
        public virtual BtgoldLoanLead Lead { get; set; }
    }
}
