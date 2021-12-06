using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class Managers
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string FatherName { get; set; }
        public string Address { get; set; }
        public long UserId { get; set; }
        public long? DistrictId { get; set; }
        public int? StateId { get; set; }
        public string Pincode { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public string Setting { get; set; }
        public long? AreaPincodeId { get; set; }

        public virtual PincodeArea AreaPincode { get; set; }
        public virtual District District { get; set; }
        public virtual State State { get; set; }
        public virtual UserMaster User { get; set; }
    }
}
