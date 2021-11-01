﻿using AurigainLoanERP.Services.Customer;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AurigainLoanERP.Api.Areas
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customer;
        public CustomerController(ICustomerService customer)
        {
            _customer = customer;
        }
        // GET api/Customer/AddUpdateAsync
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<string>> RegisterCustomer(CustomerRegistrationModel model)
        {
            return await _customer.AddUpdateAsync(model);
        }
    }
}
