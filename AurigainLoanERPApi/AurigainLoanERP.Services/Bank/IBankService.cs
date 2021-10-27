using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.Bank
{
    public interface IBankService
    {
        Task<ApiServiceResponseModel<List<BankModel>>> GetAllAsync(IndexModel model);
        Task<ApiServiceResponseModel<BankModel>> GetById(int id);
        Task<ApiServiceResponseModel<List<DDLBankModel>>> Banks();
        Task<ApiServiceResponseModel<string>> AddUpdateAsync(BankModel model);
        Task<ApiServiceResponseModel<object>> UpateActiveStatus(int id);
        Task<ApiServiceResponseModel<object>> UpdateDeleteStatus(int id);
        Task<ApiServiceResponseModel<List<AvailableBranchModel>>> BranchByPincode(string pincode);
        Task<ApiServiceResponseModel<string>> UpdateLogo(BankLogoPostModel model);
    }
}
