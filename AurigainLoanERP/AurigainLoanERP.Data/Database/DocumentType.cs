using System;
using System.Collections.Generic;

#nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class DocumentType
    {
        public DocumentType()
        {
            UserDocuments = new HashSet<UserDocument>();
            UserKycs = new HashSet<UserKyc>();
        }

        public int Id { get; set; }
        public string DocumentName { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<UserDocument> UserDocuments { get; set; }
        public virtual ICollection<UserKyc> UserKycs { get; set; }
    }
}
