using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.Customer
{
    public interface ICustomerService
    {
        public Task<ApiServiceResponseModel<string>> TestEmail();
        public Task<ApiServiceResponseModel<string>> AddUpdateAsync(CustomerRegistrationModel model);
    }
}
