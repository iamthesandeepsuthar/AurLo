using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.PaymentMode
{
    public interface IPaymentModeService
    {
        Task<ApiServiceResponseModel<List<PaymentModeModel>>> GetAllAsync(IndexModel model);
        Task<ApiServiceResponseModel<PaymentModeModel>> GetById(int id);
        Task<ApiServiceResponseModel<List<DDLPaymentModeModel>>> GetPaymentModes();
        Task<ApiServiceResponseModel<string>> AddUpdateAsync(PaymentModeModel model);
        // Task<ApiServiceResponseModel<object>> CheckRoleExist(string name, int? id = null);      
        Task<ApiServiceResponseModel<object>> UpateActiveStatus(int id);
        Task<ApiServiceResponseModel<object>> UpdateDeleteStatus(int id);
    }
}
