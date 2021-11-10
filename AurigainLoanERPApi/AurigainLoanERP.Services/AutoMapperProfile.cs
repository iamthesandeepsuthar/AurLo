using AurigainLoanERP.Data.Database;
using AurigainLoanERP.Shared.ContractModel;
using AutoMapper;

namespace AurigainLoanERP.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<State, StateModel>().ReverseMap();
            CreateMap<District, DistrictModel>().ReverseMap();
            CreateMap<DocumentType, DocumentTypeModel>().ReverseMap();
            CreateMap<QualificationMaster, QualificationModel>().ReverseMap();
            CreateMap<Data.Database.PaymentMode, PaymentModeModel>().ReverseMap();
            CreateMap<UserAgent, AgentPostModel>().ReverseMap();
            CreateMap<UserDoorStepAgent, DoorStepAgentPostModel>().ReverseMap();
            CreateMap<UserOtp, OtpModel>().ReverseMap();
            CreateMap<UserMaster, UserPostModel>().ReverseMap();
            CreateMap<UserReportingPerson, UserReportingPersonPostModel>().ReverseMap();
            CreateMap<UserBank, UserBankDetailPostModel>().ReverseMap();
            CreateMap<UserKyc, UserKycPostModel>().ReverseMap();
            CreateMap<UserNominee, UserNomineePostModel>().ReverseMap();
            CreateMap<UserSecurityDepositDetails, UserSecurityDepositPostModel>().ReverseMap();
            CreateMap<UserRole, UserRolePostModel>().ReverseMap();
            CreateMap<UserDoorStepAgent, DoorStepAgentViewModel>().ReverseMap();
            CreateMap<UserAgent, AgentViewModel>().ReverseMap();
            CreateMap<UserViewModel, UserMaster>().ReverseMap();
            CreateMap<UserDocumentViewModel, UserDocument>().ReverseMap();
            CreateMap<UserReportingPersonViewModel, UserReportingPerson>().ReverseMap();
            CreateMap<UserBankDetailViewModel, UserBank>().ReverseMap();
            CreateMap<UserKycViewModel, UserKyc>().ReverseMap();
            CreateMap<UserNomineeViewModel, UserNominee>().ReverseMap();
            CreateMap<UserDocumentFilesViewModel, UserDocumentFiles>().ReverseMap();
            CreateMap<UserSecurityDepositViewModel, UserSecurityDepositDetails>().ReverseMap();
            CreateMap<UserRole, UserRoleViewModel>().ReverseMap();
            CreateMap<Managers, UserManagerModel>().ReverseMap();
            CreateMap<AurigainLoanERP.Data.Database.ProductCategory, ProductCategoryModel>().ReverseMap();
            CreateMap<AurigainLoanERP.Data.Database.JewellaryType, JewellaryTypeModel>().ReverseMap();
            CreateMap<AurigainLoanERP.Data.Database.Product, ProductModel>().ReverseMap();
            CreateMap<BankMaster, BankModel>().ReverseMap();
            CreateMap<BankBranchMaster, BranchModel>().ReverseMap();
            CreateMap<GoldLoanFreshLead, GoldLoanFreshLeadModel>().ReverseMap();
            CreateMap<UserAvailability, UserAvailabilityViewModel>().ForMember(d => d.Area, opt => opt.MapFrom(src => src.PincodeArea.AreaName)).ReverseMap() ;
            CreateMap<FreshLeadHlplcl, FreshLeadHLPLCLModel>().ReverseMap();
        }
    }
}
