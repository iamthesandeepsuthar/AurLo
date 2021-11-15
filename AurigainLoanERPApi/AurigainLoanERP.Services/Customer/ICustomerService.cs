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
        Task<ApiServiceResponseModel<object>> UpateActiveStatus(long id);
        Task<ApiServiceResponseModel<object>> UpdateDeleteStatus(long id);
        Task<ApiServiceResponseModel<object>> UpdateApproveStatus(long id);
        Task<ApiServiceResponseModel<List<CustomerListModel>>> GetListAsync(IndexModel model);
        public Task<ApiServiceResponseModel<string>> AddUpdateAsync(CustomerRegistrationModel model);
        public Task<ApiServiceResponseModel<CustomerRegistrationViewModel>>GetCustomerProfile(long id);
        public Task<ApiServiceResponseModel<CustomerRegistrationModel>> GetCustomerDetail(long id);
       
    }
}
