using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.Bank
{
    public interface IBranchService
    {
        Task<ApiServiceResponseModel<BranchModel>> GetById(int id);
        Task<ApiServiceResponseModel<List<DDLBranchModel>>> Branches(int bankId);
        Task<ApiServiceResponseModel<string>> AddUpdateAsync(BranchModel model);
        Task<ApiServiceResponseModel<object>> UpateActiveStatus(int id);
        Task<ApiServiceResponseModel<object>> UpdateDeleteStatus(int id);
        Task<ApiServiceResponseModel<List<DDLBranchModel>>> BranchesbyPinCode(string PinCode);
    }
}
