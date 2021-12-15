using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class UserDoorStepAgent
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string UniqueId { get; set; }
        public bool SelfFunded { get; set; }
        public string Gender { get; set; }
        public int QualificationId { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public long? DistrictId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public int? SecurityDepositId { get; set; }
        public long? AreaPincodeId { get; set; }

        public virtual PincodeArea AreaPincode { get; set; }
        public virtual District District { get; set; }
        public virtual QualificationMaster Qualification { get; set; }
        public virtual UserSecurityDepositDetails SecurityDeposit { get; set; }
        public virtual UserMaster User { get; set; }
        public virtual UserReportingPerson ReportingPerson {get;set;}
    }
}
