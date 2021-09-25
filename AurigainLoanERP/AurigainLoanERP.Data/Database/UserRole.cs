using System;
using System.Collections.Generic;

#nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class UserRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
