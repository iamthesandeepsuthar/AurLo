using AurigainLoanERP.Data.ContractModel;
using AurigainLoanERP.Data.Database;
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
            CreateMap<PaymentMode, PaymentModeModel>().ReverseMap();
            CreateMap<UserAgent, AgentModel>().ReverseMap();
            CreateMap<UserDoorStepAgent, DoorStepAgentModel>().ReverseMap();
            CreateMap<UserOtp, OtpModel>().ReverseMap();
            CreateMap<UserMaster, UserModel>().ReverseMap();
            CreateMap<UserReportingPerson, UserReportingPersonModel>().ReverseMap();
        }

    }
}
