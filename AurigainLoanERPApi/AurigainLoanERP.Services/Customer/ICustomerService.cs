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
        Task<ApiServiceResponseModel<string>> TestEmail();
        Task<ApiServiceResponseModel<SentSMSResponseModel>> TestSMS(string msg, string mobile);

        Task<ApiServiceResponseModel<SMSStatusResponseModel>> CheckSMSStatus(long smsId);
        Task<ApiServiceResponseModel<object>> UpateActiveStatus(long id);
        Task<ApiServiceResponseModel<object>> UpdateDeleteStatus(long id);
        Task<ApiServiceResponseModel<object>> UpdateApproveStatus(long id);
        Task<ApiServiceResponseModel<List<CustomerListModel>>> GetListAsync(IndexModel model);
        Task<ApiServiceResponseModel<string>> AddUpdateAsync(CustomerRegistrationModel model);
        Task<ApiServiceResponseModel<CustomerRegistrationViewModel>> GetCustomerProfile(long id);
        Task<ApiServiceResponseModel<CustomerRegistrationModel>> GetCustomerDetail(long id);

    }
}
