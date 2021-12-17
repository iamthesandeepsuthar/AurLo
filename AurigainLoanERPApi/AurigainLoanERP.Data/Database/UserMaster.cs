using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class UserMaster
    {
        public UserMaster()
        {
            BtgoldLoanLeadActionHistoryActionTakenByUser = new HashSet<BtgoldLoanLeadActionHistory>();
            BtgoldLoanLeadActionHistoryAssignToUser = new HashSet<BtgoldLoanLeadActionHistory>();
            BtgoldLoanLeadApprovalActionHistory = new HashSet<BtgoldLoanLeadApprovalActionHistory>();
            BtgoldLoanLeadCreatedByNavigation = new HashSet<BtgoldLoanLead>();
            BtgoldLoanLeadCustomerUser = new HashSet<BtgoldLoanLead>();
            BtgoldLoanLeadLeadSourceByuser = new HashSet<BtgoldLoanLead>();
            BtgoldLoanLeadModifiedByNavigation = new HashSet<BtgoldLoanLead>();
            BtgoldLoanLeadStatusActionHistory = new HashSet<BtgoldLoanLeadStatusActionHistory>();
            FreshLeadHlplclCreatedByNavigation = new HashSet<FreshLeadHlplcl>();
            FreshLeadHlplclCustomerUser = new HashSet<FreshLeadHlplcl>();
            FreshLeadHlplclLeadSourceByUser = new HashSet<FreshLeadHlplcl>();
            FreshLeadHlplclModifiedByNavigation = new HashSet<FreshLeadHlplcl>();
            FreshLeadHlplclstatusActionHistory = new HashSet<FreshLeadHlplclstatusActionHistory>();
            GoldLoanFreshLeadActionHistoryAssignFromUser = new HashSet<GoldLoanFreshLeadActionHistory>();
            GoldLoanFreshLeadActionHistoryAssignToUser = new HashSet<GoldLoanFreshLeadActionHistory>();
            GoldLoanFreshLeadCreatedByNavigation = new HashSet<GoldLoanFreshLead>();
            GoldLoanFreshLeadCustomerUser = new HashSet<GoldLoanFreshLead>();
            GoldLoanFreshLeadLeadSourceByUser = new HashSet<GoldLoanFreshLead>();
            GoldLoanFreshLeadModifiedByNavigation = new HashSet<GoldLoanFreshLead>();
            GoldLoanFreshLeadStatusActionHistory = new HashSet<GoldLoanFreshLeadStatusActionHistory>();
            Managers = new HashSet<Managers>();
            UserAgent = new HashSet<UserAgent>();
            UserAvailability = new HashSet<UserAvailability>();
            UserBank = new HashSet<UserBank>();
            UserCustomer = new HashSet<UserCustomer>();
            UserDocument = new HashSet<UserDocument>();
            UserDoorStepAgent = new HashSet<UserDoorStepAgent>();
            UserKyc = new HashSet<UserKyc>();
            UserLoginLog = new HashSet<UserLoginLog>();
            UserNominee = new HashSet<UserNominee>();
            UserReportingPersonReportingUser = new HashSet<UserReportingPerson>();
            UserReportingPersonUser = new HashSet<UserReportingPerson>();
            UserSecurityDepositDetails = new HashSet<UserSecurityDepositDetails>();
        }

        public long Id { get; set; }
        public int UserRoleId { get; set; }
        public string UserName { get; set; }
        public string Mpin { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string ProfilePath { get; set; }
        public string DeviceToken { get; set; }
        public string Token { get; set; }
        public bool IsApproved { get; set; }
        public bool IsWhatsApp { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }

        public virtual UserRole UserRole { get; set; }
        public virtual ICollection<BtgoldLoanLeadActionHistory> BtgoldLoanLeadActionHistoryActionTakenByUser { get; set; }
        public virtual ICollection<BtgoldLoanLeadActionHistory> BtgoldLoanLeadActionHistoryAssignToUser { get; set; }
        public virtual ICollection<BtgoldLoanLeadApprovalActionHistory> BtgoldLoanLeadApprovalActionHistory { get; set; }
        public virtual ICollection<BtgoldLoanLead> BtgoldLoanLeadCreatedByNavigation { get; set; }
        public virtual ICollection<BtgoldLoanLead> BtgoldLoanLeadCustomerUser { get; set; }
        public virtual ICollection<BtgoldLoanLead> BtgoldLoanLeadLeadSourceByuser { get; set; }
        public virtual ICollection<BtgoldLoanLead> BtgoldLoanLeadModifiedByNavigation { get; set; }
        public virtual ICollection<BtgoldLoanLeadStatusActionHistory> BtgoldLoanLeadStatusActionHistory { get; set; }
        public virtual ICollection<FreshLeadHlplcl> FreshLeadHlplclCreatedByNavigation { get; set; }
        public virtual ICollection<FreshLeadHlplcl> FreshLeadHlplclCustomerUser { get; set; }
        public virtual ICollection<FreshLeadHlplcl> FreshLeadHlplclLeadSourceByUser { get; set; }
        public virtual ICollection<FreshLeadHlplcl> FreshLeadHlplclModifiedByNavigation { get; set; }
        public virtual ICollection<FreshLeadHlplclstatusActionHistory> FreshLeadHlplclstatusActionHistory { get; set; }
        public virtual ICollection<GoldLoanFreshLeadActionHistory> GoldLoanFreshLeadActionHistoryAssignFromUser { get; set; }
        public virtual ICollection<GoldLoanFreshLeadActionHistory> GoldLoanFreshLeadActionHistoryAssignToUser { get; set; }
        public virtual ICollection<GoldLoanFreshLead> GoldLoanFreshLeadCreatedByNavigation { get; set; }
        public virtual ICollection<GoldLoanFreshLead> GoldLoanFreshLeadCustomerUser { get; set; }
        public virtual ICollection<GoldLoanFreshLead> GoldLoanFreshLeadLeadSourceByUser { get; set; }
        public virtual ICollection<GoldLoanFreshLead> GoldLoanFreshLeadModifiedByNavigation { get; set; }
        public virtual ICollection<GoldLoanFreshLeadStatusActionHistory> GoldLoanFreshLeadStatusActionHistory { get; set; }
        public virtual ICollection<Managers> Managers { get; set; }
        public virtual ICollection<UserAgent> UserAgent { get; set; }
        public virtual ICollection<UserAvailability> UserAvailability { get; set; }
        public virtual ICollection<UserBank> UserBank { get; set; }
        public virtual ICollection<UserCustomer> UserCustomer { get; set; }
        public virtual ICollection<UserDocument> UserDocument { get; set; }
        public virtual ICollection<UserDoorStepAgent> UserDoorStepAgent { get; set; }
        public virtual ICollection<UserKyc> UserKyc { get; set; }
        public virtual ICollection<UserLoginLog> UserLoginLog { get; set; }
        public virtual ICollection<UserNominee> UserNominee { get; set; }
        public virtual ICollection<UserReportingPerson> UserReportingPersonReportingUser { get; set; }
        public virtual ICollection<UserReportingPerson> UserReportingPersonUser { get; set; }
        public virtual ICollection<UserSecurityDepositDetails> UserSecurityDepositDetails { get; set; }
    }
}
