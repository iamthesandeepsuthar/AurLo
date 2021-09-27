using System;
using System.Collections.Generic;

#nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class District
    {
        public District()
        {
            UserAgents = new HashSet<UserAgent>();
            UserDoorStepAgents = new HashSet<UserDoorStepAgent>();
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
        public virtual ICollection<UserAgent> UserAgents { get; set; }
        public virtual ICollection<UserDoorStepAgent> UserDoorStepAgents { get; set; }
    }
}
