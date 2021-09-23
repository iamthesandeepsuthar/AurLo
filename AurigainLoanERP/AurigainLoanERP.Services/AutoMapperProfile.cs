using AurigainLoanERP.Data.ContractModel;
using AutoMapper;

namespace AurigainLoanERP.Services
{
  public  class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<StudentDTO, Student>().ReverseMap();
        }

    }
}
