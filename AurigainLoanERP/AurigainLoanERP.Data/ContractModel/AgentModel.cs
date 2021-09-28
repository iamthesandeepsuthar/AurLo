using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Data.ContractModel
{
    public class AgentModel
    {
        public AgentModel() {           
            BankDetails = new UserBankDetailModel1();
            UserKYC = new UserKycModel1();
            UserNominee = new UserNomineeModel1();
        }
        public long Id { get; set; }
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string UniqueId { get; set; }
        public string Gender { get; set; }
        public int QualificationId { get; set; }
        public string Address { get; set; }      
        public int? DistrictId { get; set; }
        public string PinCode { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ProfilePictureUrl { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }       
        public  UserBankDetailModel1 BankDetails { get; set; }
        public  UserKycModel1 UserKYC { get; set;}
        public  UserNomineeModel1  UserNominee { get; set; }
    }
    public class UserBankDetailModel1
    {
        public long Id { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string Ifsccode { get; set; }
        public string Address { get; set; }
        public long UserId { get; set; }
        public bool? IsActive { get; set; }        
    }
    public class UserNomineeModel1
    {
        public long Id { get; set; }
        public string NamineeName { get; set; }
        public string RelationshipWithNominee { get; set; }
        public long UserId { get; set; }
        public bool? IsActive { get; set; }       
    }
    public class UserKycModel1
    {
        public long Id { get; set; }
        public string Kycnumber { get; set; }
        public int KycdocumentTypeId { get; set; }
        public long UserId { get; set; }
        public bool? IsActive { get; set; }        
    }
}
