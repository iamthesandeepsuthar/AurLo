using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Shared.ContractModel
{

    #region << User View Model >>

    public class UserViewModel
    {
        public long Id { get; set; }
        public int UserRoleId { get; set; }
        public string UserRoleName { get; set; }
        public string ProfilePath { get; set; }
        public string UserName { get; set; }
        public string MPin { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public bool? IsWhatsApp { get; set; }
        public bool IsApproved { get; set; }
        public string DeviceToken { get; set; }
        public string Token { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
    }

    public class UserBankDetailViewModel
    {
        public long Id { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string Ifsccode { get; set; }
        public string Address { get; set; }
        public long UserId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
    }

    public class UserKycViewModel
    {
        public long Id { get; set; }
        public string Kycnumber { get; set; }
        public int KycdocumentTypeId { get; set; }
        public string KycdocumentTypeName { get; set; }

        public long UserId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
    }

    public class UserNomineeViewModel
    {
        public long Id { get; set; }
        public string NamineeName { get; set; }
        public string RelationshipWithNominee { get; set; }
        public long UserId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }

    }

    public class UserReportingPersonViewModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ReportingUserId { get; set; }


        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public virtual UserViewModel ReportingUser { get; set; }
    }

    public class UserDocumentViewModel
    {

        public long Id { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }

        public long UserId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }


        public virtual List<UserDocumentFilesViewModel> UserDocumentFiles { get; set; }
    }

    public class AgentViewModel
    {
        public AgentViewModel()
        {
            User = new UserViewModel();
            Documents = new List<UserDocumentViewModel>();
            ReportingPerson = new UserReportingPersonViewModel();
            BankDetails = new UserBankDetailViewModel();
            UserKYC = new List<UserKycViewModel>();
            UserNominee = new UserNomineeViewModel();
        }
        public long Id { get; set; }

        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string UniqueId { get; set; }
        public string Gender { get; set; }
        public int QualificationId { get; set; }
        public string QualificationName { get; set; }

        public string Address { get; set; }
        public int? DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string StateName { get; set; }
        public string PinCode { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ProfilePictureUrl { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public List<UserDocumentViewModel>? Documents { get; set; }
        public UserViewModel User { get; set; }

        public UserBankDetailViewModel? BankDetails { get; set; }
        public List<UserKycViewModel>? UserKYC { get; set; }
        public UserNomineeViewModel? UserNominee { get; set; }
        public UserReportingPersonViewModel? ReportingPerson { get; set; }


        //public virtual Object District { get; set; }
        //public virtual Object Qualification { get; set; }

    }
    public class AgentListViewModel
    {

        public long Id { get; set; }
        public long UserId { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string UniqueId { get; set; }
        public string Gender { get; set; }
        public string QualificationName { get; set; }

        public string Address { get; set; }
        public string DistrictName { get; set; }
        public string StateName { get; set; }
        public string PinCode { get; set; }
        public string Mpin { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ProfilePictureUrl { get; set; }
        public bool IsApproved { get; set; }

        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }

    }

    public class DoorStepAgentListModel
    {

        public long Id { get; set; }
        public long UserId { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string UniqueId { get; set; }
        public string Gender { get; set; }
        public string QualificationName { get; set; }

        public string Address { get; set; }
        public string DistrictName { get; set; }
        public string StateName { get; set; }
        public string PinCode { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ProfilePictureUrl { get; set; }
        public bool IsApproved { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }

    }

    public class DoorStepAgentViewModel
    {
        public DoorStepAgentViewModel()
        {
            User = new UserViewModel();
            BankDetails = new UserBankDetailViewModel();
            ReportingPerson = new UserReportingPersonViewModel();
            UserKYC = new List<UserKycViewModel>();
            UserNominee = new UserNomineeViewModel();
            Documents = new List<UserDocumentViewModel>();
            SecurityDeposit = new UserSecurityDepositViewModel();
        }

        public int Id { get; set; }
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string UniqueId { get; set; }
        public bool SelfFunded { get; set; }
        public string Gender { get; set; }
        public int QualificationId { get; set; }
        public string QualificationName { get; set; }

        public string Address { get; set; }
        public string PinCode { get; set; }
        public int? DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string StateName { get; set; }
        public int? StateId { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public string ProfilePictureUrl { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public int? SecurityDepositId { get; set; }

        public UserViewModel User { get; set; }

        public UserBankDetailViewModel BankDetails { get; set; }
        public List<UserKycViewModel> UserKYC { get; set; }
        public UserNomineeViewModel UserNominee { get; set; }
        public UserReportingPersonViewModel ReportingPerson { get; set; }
        public List<UserDocumentViewModel> Documents { get; set; }
        public UserSecurityDepositViewModel SecurityDeposit { get; set; }
    }


    #endregion

    #region <<User Post Model>>
    public class UserPostModel
    {
        public long? Id { get; set; }
        public int? UserRoleId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public bool? IsWhatsApp { get; set; }

        public bool? IsApproved { get; set; }
        public string DeviceToken { get; set; }

    }
    public class UserBankDetailPostModel
    {
        public long Id { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string Ifsccode { get; set; }
        public string Address { get; set; }
    }
    public class UserNomineePostModel
    {
        public long Id { get; set; }
        public string NamineeName { get; set; }
        public string RelationshipWithNominee { get; set; }
        public bool IsSelfDeclaration { get; set; }

    }
    public class UserKycPostModel
    {
        public long Id { get; set; }
        public string Kycnumber { get; set; }
        public int KycdocumentTypeId { get; set; }
    }
    public class UserReportingPersonPostModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ReportingUserId { get; set; }

    }
    public class UserDocumentPostModel
    {
        public UserDocumentPostModel()
        {
            Files = new List<FilePostModel>();
        }

        public long Id { get; set; }
        public int DocumentTypeId { get; set; }

        public List<FilePostModel> Files { get; set; }

    }
    public class FilePostModel
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public string File { get; set; }
        public string FileType { get; set; }
        /// <summary>
        /// If Is Edit =true then Update file in database Document not blank, if ie edit = true and Document is blank then remove file
        /// </summary>
        public bool IsEditMode { get; set; }

    }
    public class AgentPostModel
    {
        public AgentPostModel()
        {
            User = new UserPostModel();
            BankDetails = new UserBankDetailPostModel();
            Documents = new List<UserDocumentPostModel>();
            UserKYC = new List<UserKycPostModel>();
            UserNominee = new UserNomineePostModel();
            // ReportingPerson = new UserReportingPersonPostModel();
        }

        public long Id { get; set; }

        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public int QualificationId { get; set; }
        public string Address { get; set; }
        public int? DistrictId { get; set; }
        public string PinCode { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsActive { get; set; }

        public UserPostModel User { get; set; }

        public List<UserDocumentPostModel> Documents { get; set; }

        public UserBankDetailPostModel BankDetails { get; set; }
        public List<UserKycPostModel> UserKYC { get; set; }
        public UserNomineePostModel UserNominee { get; set; }
        public UserReportingPersonPostModel? ReportingPerson { get; set; }
    }
    public class DoorStepAgentPostModel
    {
        public DoorStepAgentPostModel()
        {
            User = new UserPostModel();
            BankDetails = new UserBankDetailPostModel();
            UserKYC = new List<UserKycPostModel>();
            UserNominee = new UserNomineePostModel();
            //   ReportingPerson = new UserReportingPersonPostModel();
            Documents = new List<UserDocumentPostModel>();
            SecurityDeposit = new UserSecurityDepositPostModel();


        }

        public long Id { get; set; }

        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public int QualificationId { get; set; }
        public string Address { get; set; }
        public int? DistrictId { get; set; }
        public int? StateId { get; set; }
        public string PinCode { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool SelfFunded { get; set; }

        public UserPostModel User { get; set; }
        public UserBankDetailPostModel? BankDetails { get; set; }
        public List<UserKycPostModel>? UserKYC { get; set; }
        public UserNomineePostModel? UserNominee { get; set; }


        public UserReportingPersonPostModel? ReportingPerson { get; set; }
        public List<UserDocumentPostModel>? Documents { get; set; }
        public UserSecurityDepositPostModel? SecurityDeposit { get; set; }

    }

    public class UserSettingPostModel
    {
        public long UserId { get; set; }
        public string ProfileBase64 { get; set; }
        public string FileName { get; set; }


    }

    public class UserAvailibilityPostModel
    {
        public long UserId { get; set; }
        public string MondayST { get; set; }
        public string MondayET { get; set; }
        public string TuesdayST { get; set; }
        public string TuesdayET { get; set; }
        public string WednesdayST { get; set; }
        public string WednesdayET { get; set; }
        public string ThursdayST { get; set; }
        public string ThursdayET { get; set; }
        public string FridayST { get; set; }
        public string FridayET { get; set; }
        public string SaturdayST { get; set; }
        public string SaturdayET { get; set; }
        public string SundayST { get; set; }
        public string SundayET { get; set; }
        public int? Capacity { get; set; }
        public long? PinCode { get; set; }
        public int? DistrictId { get; set; }
    }
    #endregion

}
