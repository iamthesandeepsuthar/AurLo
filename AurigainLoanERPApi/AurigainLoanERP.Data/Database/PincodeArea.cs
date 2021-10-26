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
            UserAvailability = new HashSet<UserAvailability>();
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
        public virtual ICollection<UserAvailability> UserAvailability { get; set; }
    }
}
