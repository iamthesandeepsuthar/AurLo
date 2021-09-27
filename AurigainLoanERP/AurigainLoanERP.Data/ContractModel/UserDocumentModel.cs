using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Data.ContractModel
{
    class UserDocumentModel
    {
        public long Id { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentUrl { get; set; }
        public long UserId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DocumentTypeModel DocumentType { get; set; }
    }
}
