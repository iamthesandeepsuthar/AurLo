using System;

namespace AurigainLoanERP.Shared.ContractModel
{
    public class ProductCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Note { get; set; }
    }
    public class DllProductCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
