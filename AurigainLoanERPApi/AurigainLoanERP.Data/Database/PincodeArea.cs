using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class PincodeArea
    {
        public PincodeArea()
        {
            BtgoldLoanLeadAddressDetailAeraPincode = new HashSet<BtgoldLoanLeadAddressDetail>();
            BtgoldLoanLeadAddressDetailCorrespondAeraPincode = new HashSet<BtgoldLoanLeadAddressDetail>();
            FreshLeadHlplcl = new HashSet<FreshLeadHlplcl>();
            GoldLoanFreshLeadKycDocument = new HashSet<GoldLoanFreshLeadKycDocument>();
            Managers = new HashSet<Managers>();
            UserAgent = new HashSet<UserAgent>();
            UserAvailability = new HashSet<UserAvailability>();
            UserCustomer = new HashSet<UserCustomer>();
            UserDoorStepAgent = new HashSet<UserDoorStepAgent>();
        }

        public long Id { get; set; }
        public string Pincode { get; set; }
        public string AreaName { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public long DistrictId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? Modifieddate { get; set; }

        public virtual District District { get; set; }
        public virtual ICollection<BtgoldLoanLeadAddressDetail> BtgoldLoanLeadAddressDetailAeraPincode { get; set; }
        public virtual ICollection<BtgoldLoanLeadAddressDetail> BtgoldLoanLeadAddressDetailCorrespondAeraPincode { get; set; }
        public virtual ICollection<FreshLeadHlplcl> FreshLeadHlplcl { get; set; }
        public virtual ICollection<GoldLoanFreshLeadKycDocument> GoldLoanFreshLeadKycDocument { get; set; }
        public virtual ICollection<Managers> Managers { get; set; }
        public virtual ICollection<UserAgent> UserAgent { get; set; }
        public virtual ICollection<UserAvailability> UserAvailability { get; set; }
        public virtual ICollection<UserCustomer> UserCustomer { get; set; }
        public virtual ICollection<UserDoorStepAgent> UserDoorStepAgent { get; set; }
    }
}
