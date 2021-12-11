using System;

namespace AurigainLoanERP.Shared.ContractModel
{
    public class QualificationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
    /// <summary>
    /// Qualification Dropdown Model
    /// </summary>
    public class DDLQualificationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
