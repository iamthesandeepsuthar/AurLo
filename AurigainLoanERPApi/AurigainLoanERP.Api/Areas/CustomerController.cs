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
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<List<CustomerListModel>>> GetListAsync(IndexModel model) 
        {
            return await _customer.GetListAsync(model);
        }

        [HttpGet("[action]")]
        public async Task<ApiServiceResponseModel<string>> test() 
        {
            return await _customer.TestEmail();
        }
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> UpdateApproveStatus(long id)
        {
            return await _customer.UpdateApproveStatus(id);
        }
        // DELETE api/<AgentController>/5
        [HttpDelete("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> Delete(long id)
        {
            return await _customer.UpdateDeleteStatus(id);
        }       
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> UpdatActiveStatus(long id)
        {
            return await _customer.UpateActiveStatus(id);
        }
        [HttpGet("[action]/{userId}")]  // In parameter pass customer UserId 
        public async Task<ApiServiceResponseModel<CustomerRegistrationViewModel>> GetCustomerProfile(long userId) 
        {
            return await _customer.GetCustomerProfile(userId);
        }
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<CustomerRegistrationModel>> GetCustomerDetail(long id)
        {
            return await _customer.GetCustomerDetail(id);
        }
    }
}
