using System;

namespace AurigainLoanERP.Shared.ContractModel
{
    public class JewellaryTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
    }
    public class DDLJewellaryType
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
