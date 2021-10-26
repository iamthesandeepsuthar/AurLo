using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class UserAvailability
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public TimeSpan? MondaySt { get; set; }
        public TimeSpan? MondayEt { get; set; }
        public TimeSpan? TuesdaySt { get; set; }
        public TimeSpan? TuesdayEt { get; set; }
        public TimeSpan? WednesdaySt { get; set; }
        public TimeSpan? WednesdayEt { get; set; }
        public TimeSpan? ThursdaySt { get; set; }
        public TimeSpan? ThursdayEt { get; set; }
        public TimeSpan? FridaySt { get; set; }
        public TimeSpan? FridayEt { get; set; }
        public TimeSpan? SaturdaySt { get; set; }
        public TimeSpan? SaturdayEt { get; set; }
        public TimeSpan? SundaySt { get; set; }
        public TimeSpan? SundayEt { get; set; }
        public int? Capacity { get; set; }
        public long? PincodeAreaId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual PincodeArea PincodeArea { get; set; }
        public virtual UserMaster User { get; set; }
    }
}
