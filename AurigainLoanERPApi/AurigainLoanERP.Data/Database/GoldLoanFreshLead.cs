using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class GoldLoanFreshLead
    {
        public GoldLoanFreshLead()
        {
            GoldLoanFreshLeadAppointmentDetail = new HashSet<GoldLoanFreshLeadAppointmentDetail>();
            GoldLoanFreshLeadJewelleryDetail = new HashSet<GoldLoanFreshLeadJewelleryDetail>();
            GoldLoanFreshLeadKycDocument = new HashSet<GoldLoanFreshLeadKycDocument>();
            GoldLoanFreshLeadStatusActionHistory = new HashSet<GoldLoanFreshLeadStatusActionHistory>();
        }

        public long Id { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PrimaryMobileNumber { get; set; }
        public string Email { get; set; }
        public int ProductId { get; set; }
        public long LeadSourceByUserId { get; set; }
        public string SecondaryMobileNumber { get; set; }
        public double LoanAmountRequired { get; set; }
        public string Purpose { get; set; }
        public string LoanCaseNumber { get; set; }
        public int? PurposeId { get; set; }
        public int PreferredLoanTenure { get; set; }
        public long CustomerUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string LeadStatus { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }

        public virtual UserMaster CreatedByNavigation { get; set; }
        public virtual UserMaster CustomerUser { get; set; }
        public virtual UserMaster LeadSourceByUser { get; set; }
        public virtual UserMaster ModifiedByNavigation { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<GoldLoanFreshLeadAppointmentDetail> GoldLoanFreshLeadAppointmentDetail { get; set; }
        public virtual ICollection<GoldLoanFreshLeadJewelleryDetail> GoldLoanFreshLeadJewelleryDetail { get; set; }
        public virtual ICollection<GoldLoanFreshLeadKycDocument> GoldLoanFreshLeadKycDocument { get; set; }
        public virtual ICollection<GoldLoanFreshLeadStatusActionHistory> GoldLoanFreshLeadStatusActionHistory { get; set; }
    }
}
