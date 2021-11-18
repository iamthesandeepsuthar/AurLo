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

    public class CustomerListModel 
    {
        public long Id { get; set;}
        public string FullName { get; set;}
        public string FatherName { get; set;}
        public string EmailId { get; set;}
        public string Mobile { get; set;}
        public bool? IsActive { get; set;}
        public string Pincode { get; set;}
        public bool IsApproved { get; set;}
        public long UserId { get; set;}
        public string Mpin { get; set;}
        public string Password { get; set;}
        public string ProfileImageUrl { get; set;}
        public string Gender { get; set;}
    }

    public class CustomerRegistrationViewModel 
    {
        public CustomerRegistrationViewModel() 
        {
            KycDocuments = new List<CustomerKycViewModel>();
        }
        public long Id { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public long UserId { get; set; }
        public string EmailId { get; set;}
        public string Mobile { get; set;}
        public string Pincode { get; set;}
        public string State { get; set;}
        public string District { get; set; }
        public string AreaName { get; set;}
        public string Address { get; set; }         
        public DateTime? DateOfBirth { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<CustomerKycViewModel> KycDocuments { get; set; }
    }
    public class CustomerKycViewModel
    {
        public long Id { get; set; }
        public string Kycnumber { get; set; }
        public int KycdocumentTypeId { get; set; }
        public string KycdocumentTypeName { get; set; }
        public long UserId { get; set; }       
    }

}
