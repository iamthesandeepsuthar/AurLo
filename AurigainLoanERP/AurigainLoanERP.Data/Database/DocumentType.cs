using System;
using System.Collections.Generic;

#nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class DocumentType
    {
        public int Id { get; set; }
        public string DocumentName { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
