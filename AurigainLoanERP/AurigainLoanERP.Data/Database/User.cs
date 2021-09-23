using System;
using System.Collections.Generic;

#nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
