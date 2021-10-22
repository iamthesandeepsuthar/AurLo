using System;
using System.Collections.Generic;
using System.Text;

namespace AurigainLoanERP.Shared.ContractModel
{
    public class UserManagerModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set;}
        public int RoleId { get; set;}
        public string EmailId { get; set;}
        public bool IsWhatsApp { get; set;}             
        public string FatherName { get; set; }
        public string Address { get; set; }
        public long UserId { get; set; }
        public long DistrictId { get; set; }
        public string Pincode { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public string Setting { get; set; }
        public UserPostModel User { get; set; }
    }   
}
