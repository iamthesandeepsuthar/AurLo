using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class BtgoldLoanLead
    {
        public BtgoldLoanLead()
        {
            BtgoldLoanLeadAddressDetail = new HashSet<BtgoldLoanLeadAddressDetail>();
            BtgoldLoanLeadAppointmentDetail = new HashSet<BtgoldLoanLeadAppointmentDetail>();
            BtgoldLoanLeadDocumentDetail = new HashSet<BtgoldLoanLeadDocumentDetail>();
            BtgoldLoanLeadExistingLoanDetail = new HashSet<BtgoldLoanLeadExistingLoanDetail>();
            BtgoldLoanLeadJewelleryDetail = new HashSet<BtgoldLoanLeadJewelleryDetail>();
            BtgoldLoanLeadKycdetail = new HashSet<BtgoldLoanLeadKycdetail>();
        }

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
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }

        public virtual UserMaster LeadSourceByuser { get; set; }
        public virtual ICollection<BtgoldLoanLeadAddressDetail> BtgoldLoanLeadAddressDetail { get; set; }
        public virtual ICollection<BtgoldLoanLeadAppointmentDetail> BtgoldLoanLeadAppointmentDetail { get; set; }
        public virtual ICollection<BtgoldLoanLeadDocumentDetail> BtgoldLoanLeadDocumentDetail { get; set; }
        public virtual ICollection<BtgoldLoanLeadExistingLoanDetail> BtgoldLoanLeadExistingLoanDetail { get; set; }
        public virtual ICollection<BtgoldLoanLeadJewelleryDetail> BtgoldLoanLeadJewelleryDetail { get; set; }
        public virtual ICollection<BtgoldLoanLeadKycdetail> BtgoldLoanLeadKycdetail { get; set; }
    }
}
