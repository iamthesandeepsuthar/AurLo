using System;
using System.Collections.Generic;
using System.Text;

namespace AurigainLoanERP.Shared.ContractModel
{
    public class CustomerRegistrationModel
    {
        public CustomerRegistrationModel() 
        {
            User = new UserPostModel();
            this.KycDocuments = new List<UserKycPostModel>();
        }
        public long Id { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public long UserId { get; set; }
        public long? PincodeAreaId { get; set; }
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public UserPostModel User { get; set;}
        public List<UserKycPostModel> KycDocuments { get; set;}
    }
}
