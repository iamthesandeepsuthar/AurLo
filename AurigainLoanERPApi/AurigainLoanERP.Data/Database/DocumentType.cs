using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class DocumentType
    {
        public DocumentType()
        {
            BtgoldLoanLeadKycdetailPoadocumentType = new HashSet<BtgoldLoanLeadKycdetail>();
            BtgoldLoanLeadKycdetailPoidocumentType = new HashSet<BtgoldLoanLeadKycdetail>();
            GoldLoanFreshLeadKycDocument = new HashSet<GoldLoanFreshLeadKycDocument>();
            UserDocument = new HashSet<UserDocument>();
            UserKyc = new HashSet<UserKyc>();
        }

        public int Id { get; set; }
        public string DocumentName { get; set; }
        public bool? IsActive { get; set; }
        public bool IsNumeric { get; set; }
        public bool IsKyc { get; set; }
        public bool IsFreshLeadKyc { get; set; }
        public bool IsBtleadKyc { get; set; }
        public bool IsMandatory { get; set; }
        public int RequiredFileCount { get; set; }
        public int? DocumentNumberLength { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<BtgoldLoanLeadKycdetail> BtgoldLoanLeadKycdetailPoadocumentType { get; set; }
        public virtual ICollection<BtgoldLoanLeadKycdetail> BtgoldLoanLeadKycdetailPoidocumentType { get; set; }
        public virtual ICollection<GoldLoanFreshLeadKycDocument> GoldLoanFreshLeadKycDocument { get; set; }
        public virtual ICollection<UserDocument> UserDocument { get; set; }
        public virtual ICollection<UserKyc> UserKyc { get; set; }
    }
}
