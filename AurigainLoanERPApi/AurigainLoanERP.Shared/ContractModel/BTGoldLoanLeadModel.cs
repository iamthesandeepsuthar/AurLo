using System;
using System.Collections.Generic;

namespace AurigainLoanERP.Shared.ContractModel
{
    public class BTGoldLoanLeadViewModel
    {
        public long Id { get; set; }
        public int ProductId { get; set; }
        public string ProductCategoryName { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Profession { get; set; }
        public string Mobile { get; set; }
        public string EmailId { get; set; }
        public long CustomerUserId { get; set; }
        public string SecondaryMobile { get; set; }
        public string Purpose { get; set; }
        public int? PurposeId { get; set; }
        public string PurposeName { get; set; }
        public decimal LoanAmount { get; set; }
        public string LoanAccountNumber { get; set; }
        public string LoanCaseNumber { get; set; }
        public long LeadSourceByuserId { get; set; }
        public BTGoldLoanLeadViewModel()
        {
            DetailAddress = new BtGoldLoanLeadAddressViewModel();
            AppointmentDetail = new BtGoldLoanLeadAppointmentViewModel();
            DocumentDetail = new BtGoldLoanLeadDocumentViewModel();
            JewelleryDetail = new List<BtGoldLoanLeadJewelleryDetailViewModel>();
            KYCDetail = new BtGoldLoanLeadKYCDetailViewModel();
            ExistingLoanDetail = new BtGoldLoanLeadExistingLoanViewModel();
        }
        public string LeadSourceByuserName { get; set; }
        public string ProductName { get; set; }
        public string CustomerUserName { get; set; }
        public bool IsInternalLead { get; set; }
        public BtGoldLoanLeadAddressViewModel DetailAddress { get; set; }
        public BtGoldLoanLeadAppointmentViewModel AppointmentDetail { get; set; }
        public BtGoldLoanLeadDocumentViewModel DocumentDetail { get; set; }
        public BtGoldLoanLeadExistingLoanViewModel ExistingLoanDetail { get; set; }
        public List<BtGoldLoanLeadJewelleryDetailViewModel> JewelleryDetail { get; set; }
        public BtGoldLoanLeadKYCDetailViewModel KYCDetail { get; set; }
    }
    public class BTGoldLoanLeadPostModel
    {
        public long Id { get; set; }
        public int ProductId { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Profession { get; set; }
        public string Mobile { get; set; }
        public string EmailId { get; set; }
        public long CustomerUserId { get; set; }
        public string SecondaryMobile { get; set; }
        public string Purpose { get; set; }
        public int? PurposeId { get; set; }
        public decimal LoanAmount { get; set; }
        public string LoanAccountNumber { get; set; }
        public long LeadSourceByuserId { get; set; }

        public BtGoldLoanLeadAddressPostModel AddressDetail { get; set; }
        public BtGoldLoanLeadAppointmentPostModel AppointmentDetail { get; set; }
        public BtGoldLoanLeadDocumentPostModel DocumentDetail { get; set; }
        public BtGoldLoanLeadExistingLoanPostModel ExistingLoanDetail { get; set; }
        public List<BtGoldLoanLeadJewelleryDetailPostModel> JewelleryDetail { get; set; }
        public BtGoldLoanLeadKYCDetailPostModel KYCDetail { get; set; }

    }
    public class BtGoldLoanLeadAddressPostModel
    {
        public long Id { get; set; }
        public string Address { get; set; }
        public string AddressLine2 { get; set; }
        public long? AeraPincodeId { get; set; }
        public string CorrespondAddress { get; set; }
        public string CorrespondAddressLine2 { get; set; }
        public long? CorrespondAeraPincodeId { get; set; }
    }
    public class BtGoldLoanLeadAppointmentPostModel
    {
        public long Id { get; set; }
        public int BranchId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public long LeadId { get; set; }
    }
    public class BTGoldLoanDeletePostModel
    {
        public long LeadId { get; set; }
        public long documentType { get; set; }
        public bool IsPOIPOADOC { get; set; }
        public string FileName { get; set; }


    }
    public class BtGoldLoanLeadDocumentPostModel
    {
        public long Id { get; set; }
        public FileModel? CustomerPhoto { get; set; }
        public List<FileModel>? KycDocumentPoi { get; set; }
        public List<FileModel>? KycDocumentPoa { get; set; }
        public FileModel? BlankCheque1 { get; set; }
        public FileModel? BlankCheque2 { get; set; }
        public FileModel? LoanDocument { get; set; }
        public FileModel? AggrementLastPage { get; set; }
        public FileModel? PromissoryNote { get; set; }
        public FileModel? AtmwithdrawalSlip { get; set; }
        public FileModel? ForeClosureLetter { get; set; }
    }
    public class BtGoldLoanLeadExistingLoanPostModel
    {
        public long Id { get; set; }
        public string BankName { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
        public decimal? JewelleryValuation { get; set; }
        public decimal? OutstandingAmount { get; set; }
        public decimal? BalanceTransferAmount { get; set; }
        public decimal? RequiredAmount { get; set; }
        public int? Tenure { get; set; }
    }
    public class BtGoldLoanLeadJewelleryDetailPostModel
    {
        public long Id { get; set; }

        public int? JewelleryTypeId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Weight { get; set; }
        public int? Karats { get; set; }
    }
    public class BtGoldLoanLeadKYCDetailPostModel
    {
        public long Id { get; set; }

        public int PoidocumentTypeId { get; set; }
        public string PoidocumentNumber { get; set; }
        public int PoadocumentTypeId { get; set; }
        public string PoadocumentNumber { get; set; }
        public string PANNumber { get; set; }
    }
    public class BtGoldLoanLeadApprovalStagePostModel
    {
        public long LeadId { get; set; }
        public int? ApprovalStatus { get; set; }
        public string Remarks { get; set; }
    }
    public class BtGoldLoanLeadApprovalStageViewModel
    {
        public long Id { get; set; }
        public string LeadApproveByUser { get; set; }
        public long LeadApproveByUserId { get; set; }
        public long LeadId { get; set; }
        public int? ApprovalStatus { get; set; }
        public string Remarks { get; set; }
    }
    public class BtGoldLoanLeadAddressViewModel
    {
        public long Id { get; set; }
        public string Address { get; set; }
        public string AddressLine2 { get; set;}
        public long? AeraPincodeId { get; set; }
        public string AreaName { get; set; }
        public string PinCode { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string CorrespondAddress { get; set; }
        public string CorrespondingAddressLine2 { get; set;}
        public long? CorrespondAeraPincodeId { get; set; }
        public string CorrespondAreaName { get; set; }
        public string CorrespondPinCode { get; set; }
        public string CorrespondState { get; set; }
        public string CorrespondDistrict { get; set; }
    }
    public class BtGoldLoanLeadAppointmentViewModel
    {
        public long Id { get; set; }
        public int? BranchId { get; set; }
        public string BranchName { get; set; }
        public string Ifsc { get; set; }
        public string Pincode { get; set; }
        public int? BankId { get; set; }

        public string BankName { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public DateTime ActionDate { get; set; }

    }
    public class BtGoldLoanLeadDocumentViewModel
    {
        public long Id { get; set; }
        public string CustomerPhoto { get; set; }
        public List<string> KycDocumentPoi { get; set; }
        public List<string> KycDocumentPoa { get; set; }
        public string BlankCheque1 { get; set; }
        public string BlankCheque2 { get; set; }
        public string LoanDocument { get; set; }
        public string AggrementLastPage { get; set; }
        public string PromissoryNote { get; set; }
        public string AtmwithdrawalSlip { get; set; }
        public string ForeClosureLetter { get; set; }
    }
    public class BtGoldLoanLeadExistingLoanViewModel
    {
        public long Id { get; set; }
        public string BankName { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
        public decimal? JewelleryValuation { get; set; }
        public decimal? OutstandingAmount { get; set; }
        public decimal? BalanceTransferAmount { get; set; }
        public decimal? RequiredAmount { get; set; }
        public int? Tenure { get; set; }
    }
    public class BtGoldLoanLeadJewelleryDetailViewModel
    {
        public long Id { get; set; }

        public int? JewelleryTypeId { get; set; }
        public string JewelleryType { get; set; }
        public int? Quantity { get; set; }
        public decimal? Weight { get; set; }
        public int? Karats { get; set; }
    }
    public class BtGoldLoanLeadKYCDetailViewModel
    {
        public long Id { get; set; }
        public int PoidocumentTypeId { get; set; }
        public string PoidocumentType { get; set; }
        public string PoidocumentNumber { get; set; }
        public int PoadocumentTypeId { get; set; }
        public string PoadocumentNumber { get; set; }
        public string PoadocumentType { get; set; }
        public string PANNumber { get; set; }

    }
    public class FileModel
    {

        public string FileName { get; set; }
        public string File { get; set; }
        public string FileType { get; set; }
        /// <summary>
        /// If Is Edit =true then Update file in database Document not blank, if ie edit = true and Document is blank then remove file
        /// </summary>
        public bool IsEditMode { get; set; }

    }
    public class BTGoldLoanLeadListModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PrimaryMobileNumber { get; set; }
        public long LeadSourceByUserId { get; set; }
        public bool IsInternalLead { get; set; }
        public string LeadSourceByUserName { get; set; }
        public double LoanAmountRequired { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Pincode { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string LeadStatus { get; set; }
        public string ApprovalStatus { get; set; }
        public bool IsStatusCompleted { get; set; }
        public int? ApprovedStage { get; set; }
        public string LoanCaseNumber { get; set; }
        public int LeadStatusId { get; set; }
        public int ApprovalStatusId { get; set; }
    }
    public class BTGoldLoanBalanceReturnLeadListModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string PrimaryMobileNumber { get; set; }
        public string LeadSourceByUserName { get; set; }
        public bool IsInternalLead { get; set; }
        public string Pincode { get; set; }
        public string ApprovalStatus { get; set; }
        public bool IsStatusCompleted { get; set; }
        public string LeadStatus { get; set; }
        public string LoanCaseNumber { get; set; }
        public double LoanAmountRequired { get; set; }
        public string LeadType { get; set; }
        public string ProductName { get; set; }
    }
    public class BalanceTransferReturnBankChequeDetail
    {
        public long Id { get; set; }
        public string ChequeNumber { get; set; }
        public FilePostModel ChequeImageUrl { get; set; }
    }
}
