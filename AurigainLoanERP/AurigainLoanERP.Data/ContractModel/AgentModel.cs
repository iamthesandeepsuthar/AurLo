using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Data.ContractModel
{
    public class AgentModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string UniqueId { get; set; }
        public string Gender { get; set; }
        public int QualificationId { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public int? DistrictId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ProfilePictureUrl { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public virtual DistrictModel District { get; set; }
        public virtual QualificationModel Qualification { get; set; }
        public virtual UserBankDetailModel BankDetails { get; set; }
    }
}
