using AurigainLoanERP.Data.ContractModel;
using AurigainLoanERP.Shared.Common.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.StateAndDistrict
{
   public  interface IStateAndDistrictService
    {
        #region State Method
        Task<ApiServiceResponseModel<List<StateModel>>> GetAllStateAsync(IndexModel model);
        Task<ApiServiceResponseModel<StateModel>> GetStateById(int id);
        Task<ApiServiceResponseModel<List<DDLStateModel>>> GetStates();
        Task<ApiServiceResponseModel<string>> AddUpdateStateAsync(StateModel model);
        // Task<ApiServiceResponseModel<object>> CheckRoleExist(string name, int? id = null);      
        Task<ApiServiceResponseModel<object>> UpateStateActiveStatus(int id);
        Task<ApiServiceResponseModel<object>> UpdateStateDeleteStatus(int id);

        #endregion

        #region Distict Method
        Task<ApiServiceResponseModel<List<DistrictModel>>> GetAllDistrictAsync(IndexModel model);
        Task<ApiServiceResponseModel<DistrictModel>> GetDistrictById(int id);
        Task<ApiServiceResponseModel<List<DDLDistrictModel>>> GetDistricts(int id);
        Task<ApiServiceResponseModel<string>> AddUpdateDistrictAsync(DistrictModel model);
        // Task<ApiServiceResponseModel<object>> CheckRoleExist(string name, int? id = null);      
        Task<ApiServiceResponseModel<object>> UpateDistrictActiveStatus(int id);
        Task<ApiServiceResponseModel<object>> UpdateDistrictDeleteStatus(int id);
        #endregion
    }
}
