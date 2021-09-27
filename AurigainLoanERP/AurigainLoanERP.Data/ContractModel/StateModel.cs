using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Data.ContractModel
{
    public class StateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
    /// <summary>
    /// State Dropdown Model
    /// </summary>
    public class DDLStateModel
    {
        public int Id { get; set; }
        public int Name { get; set; }
    }
}
