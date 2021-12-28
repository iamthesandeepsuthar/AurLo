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
            BalanceTransferLoanReturn = new HashSet<BalanceTransferLoanReturn>();
            BtgoldLoanLeadAddressDetail = new HashSet<BtgoldLoanLeadAddressDetail>();
            BtgoldLoanLeadAppointmentDetail = new HashSet<BtgoldLoanLeadAppointmentDetail>();
            BtgoldLoanLeadApprovalActionHistory = new HashSet<BtgoldLoanLeadApprovalActionHistory>();
            BtgoldLoanLeadDocumentDetail = new HashSet<BtgoldLoanLeadDocumentDetail>();
            BtgoldLoanLeadExistingLoanDetail = new HashSet<BtgoldLoanLeadExistingLoanDetail>();
            BtgoldLoanLeadJewelleryDetail = new HashSet<BtgoldLoanLeadJewelleryDetail>();
            BtgoldLoanLeadKycdetail = new HashSet<BtgoldLoanLeadKycdetail>();
            BtgoldLoanLeadStatusActionHistory = new HashSet<BtgoldLoanLeadStatusActionHistory>();
        }

        public long Id { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Profession { get; set; }
        public string Mobile { get; set; }
        public decimal LoanAmount { get; set; }
        public int ProductId { get; set; }
        public string EmailId { get; set; }
        public string SecondaryMobile { get; set; }
        public string Purpose { get; set; }
        public long LeadSourceByuserId { get; set; }
        public long CustomerUserId { get; set; }
        public bool IsInternalLead { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public string LoanAccountNumber { get; set; }
        public string LeadStatus { get; set; }
        public string ApprovalStatus { get; set; }
        public string LoanCaseNumber { get; set; }
        public int? PurposeId { get; set; }

        public virtual UserMaster CreatedByNavigation { get; set; }
        public virtual UserMaster CustomerUser { get; set; }
        public virtual UserMaster LeadSourceByuser { get; set; }
        public virtual UserMaster ModifiedByNavigation { get; set; }
        public virtual Product Product { get; set; }
        public virtual Purpose PurposeNavigation { get; set; }
        public virtual ICollection<BalanceTransferLoanReturn> BalanceTransferLoanReturn { get; set; }
        public virtual ICollection<BtgoldLoanLeadAddressDetail> BtgoldLoanLeadAddressDetail { get; set; }
        public virtual ICollection<BtgoldLoanLeadAppointmentDetail> BtgoldLoanLeadAppointmentDetail { get; set; }
        public virtual ICollection<BtgoldLoanLeadApprovalActionHistory> BtgoldLoanLeadApprovalActionHistory { get; set; }
        public virtual ICollection<BtgoldLoanLeadDocumentDetail> BtgoldLoanLeadDocumentDetail { get; set; }
        public virtual ICollection<BtgoldLoanLeadExistingLoanDetail> BtgoldLoanLeadExistingLoanDetail { get; set; }
        public virtual ICollection<BtgoldLoanLeadJewelleryDetail> BtgoldLoanLeadJewelleryDetail { get; set; }
        public virtual ICollection<BtgoldLoanLeadKycdetail> BtgoldLoanLeadKycdetail { get; set; }
        public virtual ICollection<BtgoldLoanLeadStatusActionHistory> BtgoldLoanLeadStatusActionHistory { get; set; }
    }
}
