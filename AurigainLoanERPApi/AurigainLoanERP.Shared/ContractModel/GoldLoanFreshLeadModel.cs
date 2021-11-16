using System;
using System.Collections.Generic;
using System.Text;

namespace AurigainLoanERP.Shared.ContractModel
{
    public class GoldLoanFreshLeadModel
    {
        public GoldLoanFreshLeadModel() 
        {
            JewelleryDetail = new GoldLoanFreshLeadJewelleryDetailModel();
            AppointmentDetail = new GoldLoanFreshLeadAppointmentDetailModel();
            KycDocument = new GoldLoanFreshLeadKycDocumentModel();
        }
        public long Id { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public long? CustomerUserId { get; set;}
        public string PrimaryMobileNumber { get; set; }
        public string SecondaryMobileNumber { get; set; }
        public double LoanAmountRequired { get; set; }
        public string Purpose { get; set; }
        public long LeadSourceByUserId { get; set; }
        public int ProductId { get; set;}
        public DateTime CreatedDate { get; set; }       
        public bool? IsActive { get; set; }      
        public GoldLoanFreshLeadKycDocumentModel KycDocument { get; set;}
        public GoldLoanFreshLeadAppointmentDetailModel AppointmentDetail { get; set; }
        public GoldLoanFreshLeadJewelleryDetailModel JewelleryDetail { get; set;}

    }
    public class GoldLoanFreshLeadAppointmentDetailModel 
    {
        public int Id { get; set; }
        public int BankId { get; set; }
        public int BranchId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public long GlfreshLeadId { get; set; }
        public bool? IsActive { get; set; }        
        public DateTime CreatedDate { get; set; }
    }
    public class GoldLoanFreshLeadJewelleryDetailModel
    {
        public int Id { get; set; }
        public int PreferredLoanTenure { get; set; }
        public int JewelleryTypeId { get; set; }
        public double Quantity { get; set; }
        public double? Weight { get; set; }
        public int? Karat { get; set; }
        public long GlfreshLeadId { get; set; }
        public bool? IsActive { get; set; }      
        public DateTime CreatedDate { get; set; }        
    }
    public class GoldLoanFreshLeadKycDocumentModel 
    {
        public int Id { get; set; }
        public int KycDocumentTypeId { get; set; }
        public string DocumentNumber { get; set; }
        public string PanNumber { get; set; }
        public long PincodeAreaId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }       
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public long GlfreshLeadId { get; set; }
    }

    public class GoldLoanFreshLeadListModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PrimaryMobileNumber { get; set; }
        public long LeadSourceByUserId { get; set; }
        public string LeadSourceByUserName { get; set; }
        public double LoanAmountRequired {get;set;}
        public int ProductId { get; set; }
        public string ProductName { get; set;}
        public DateTime CreatedDate { get; set; }
        public string Pincode { get; set;}
        public bool IsActive { get; set;}
    }
}
