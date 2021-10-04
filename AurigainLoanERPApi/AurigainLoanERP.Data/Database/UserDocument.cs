﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class UserDocument
    {
        public UserDocument()
        {
            UserDocumentFiles = new HashSet<UserDocumentFiles>();
        }

        public long Id { get; set; }
        public int DocumentTypeId { get; set; }
        public long UserId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }

        public virtual DocumentType DocumentType { get; set; }
        public virtual UserMaster User { get; set; }
        public virtual ICollection<UserDocumentFiles> UserDocumentFiles { get; set; }
    }
}