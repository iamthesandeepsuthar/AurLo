using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Shared.ContractModel
{
    public class DistrictModel
    {

       public  DistrictModel() 
        {
            Areas = new List<PincodeAreaModel>();
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        public string Pincode { get; set; }
        public string StateName { get; set;}
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public List<PincodeAreaModel> Areas { get; set;}
    }
    /// <summary>
    /// District Dropdown Model
    /// </summary>
    public class DDLDistrictModel 
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Pincode { get; set;}
    }
}
