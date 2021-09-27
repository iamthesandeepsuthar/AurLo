using System;
using System.Collections.Generic;

#nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class UserMaster
    {
        public UserMaster()
        {
            SecurityDepositDetails = new HashSet<SecurityDepositDetail>();
            UserAgents = new HashSet<UserAgent>();
            UserBanks = new HashSet<UserBank>();
            UserDocuments = new HashSet<UserDocument>();
            UserDoorStepAgents = new HashSet<UserDoorStepAgent>();
            UserKycs = new HashSet<UserKyc>();
            UserNominees = new HashSet<UserNominee>();
            UserOtps = new HashSet<UserOtp>();
            UserReportingPersonReportingUsers = new HashSet<UserReportingPerson>();
            UserReportingPersonUsers = new HashSet<UserReportingPerson>();
        }

        public long Id { get; set; }
        public int UserRoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public bool IsApproved { get; set; }
        public string DeviceToken { get; set; }
        public string Token { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }

        public virtual UserRole UserRole { get; set; }
        public virtual ICollection<SecurityDepositDetail> SecurityDepositDetails { get; set; }
        public virtual ICollection<UserAgent> UserAgents { get; set; }
        public virtual ICollection<UserBank> UserBanks { get; set; }
        public virtual ICollection<UserDocument> UserDocuments { get; set; }
        public virtual ICollection<UserDoorStepAgent> UserDoorStepAgents { get; set; }
        public virtual ICollection<UserKyc> UserKycs { get; set; }
        public virtual ICollection<UserNominee> UserNominees { get; set; }
        public virtual ICollection<UserOtp> UserOtps { get; set; }
        public virtual ICollection<UserReportingPerson> UserReportingPersonReportingUsers { get; set; }
        public virtual ICollection<UserReportingPerson> UserReportingPersonUsers { get; set; }
    }
}
