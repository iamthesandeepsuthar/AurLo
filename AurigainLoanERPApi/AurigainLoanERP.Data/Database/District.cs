using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class District
    {
        public District()
        {
            UserAgent = new HashSet<UserAgent>();
            UserDoorStepAgent = new HashSet<UserDoorStepAgent>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Pincode { get; set; }
        public int StateId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual State State { get; set; }
        public virtual ICollection<UserAgent> UserAgent { get; set; }
        public virtual ICollection<UserDoorStepAgent> UserDoorStepAgent { get; set; }
    }
}
