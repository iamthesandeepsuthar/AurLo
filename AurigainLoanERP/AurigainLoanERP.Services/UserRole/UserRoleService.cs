﻿using AurigainLoanERP.Data.ContractModel;
using AurigainLoanERP.Shared.Common.Method;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.UserRole
{
   public class UserRoleService : BaseService, IUserRoleService
    {


        public readonly IMapper _mapper;

        public UserRoleService(IMapper mapper)
        {
            this._mapper = mapper;
        }

        public Student get()
        {

            StudentDTO studentDTO = new StudentDTO()
            {
                Name = "Student 1",
                Age = 25,
                City = "New York"
            };
        
         return _mapper.Map<Student>(studentDTO);
        }
    }
}
