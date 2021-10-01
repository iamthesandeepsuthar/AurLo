using AurigainLoanERP.Data.Database;
using AurigainLoanERP.Shared.ContractModel;
using AutoMapper;

namespace AurigainLoanERP.Services
{
    public  class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<UserRole, UserRoleModel>().ReverseMap();
            CreateMap<UserRole, UserRolePostModel>().ReverseMap();
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

        }

    }
}
