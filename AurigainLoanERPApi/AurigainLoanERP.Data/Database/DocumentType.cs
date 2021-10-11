using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class DocumentType
    {
        public DocumentType()
        {
            UserDocument = new HashSet<UserDocument>();
            UserKyc = new HashSet<UserKyc>();
        }

        public int Id { get; set; }
        public string DocumentName { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<UserDocument> UserDocument { get; set; }
        public virtual ICollection<UserKyc> UserKyc { get; set; }
    }
}
