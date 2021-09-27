using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Data.ContractModel
{
    public class DocumentTypeModel
    {
        public int Id { get; set; }
        public string DocumentName { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
    /// <summary>
    /// Document Type Dropdown Model
    /// </summary>
    public class DDLDocumentTypeModel
    {
        public int Id { get; set; }
        public int Name { get; set; }
    }
}
