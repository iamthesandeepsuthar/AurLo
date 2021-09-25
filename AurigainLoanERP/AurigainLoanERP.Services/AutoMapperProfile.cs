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

        }

    }
}
