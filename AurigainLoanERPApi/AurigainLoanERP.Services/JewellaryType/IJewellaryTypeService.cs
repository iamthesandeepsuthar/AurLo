using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.JewellaryType
{
    public interface IJewellaryTypeService
    {
        Task<ApiServiceResponseModel<List<JewellaryTypeModel>>> GetAllAsync(IndexModel model);
        Task<ApiServiceResponseModel<JewellaryTypeModel>> GetById(int id);
        Task<ApiServiceResponseModel<List<DDLJewellaryType>>> JewellaryTypes();
        Task<ApiServiceResponseModel<string>> AddUpdateAsync(JewellaryTypeModel model);
        Task<ApiServiceResponseModel<object>> UpateActiveStatus(int id);
        Task<ApiServiceResponseModel<object>> UpdateDeleteStatus(int id);
    }
}
