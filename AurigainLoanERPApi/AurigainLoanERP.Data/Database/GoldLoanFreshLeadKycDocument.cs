using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class GoldLoanFreshLeadKycDocument
    {
        public int Id { get; set; }
        public int KycDocumentTypeId { get; set; }
        public string DocumentNumber { get; set; }
        public string PanNumber { get; set; }
        public long PincodeAreaId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public long GlfreshLeadId { get; set; }

        public virtual GoldLoanFreshLead GlfreshLead { get; set; }
        public virtual DocumentType KycDocumentType { get; set; }
        public virtual PincodeArea PincodeArea { get; set; }
    }
}
