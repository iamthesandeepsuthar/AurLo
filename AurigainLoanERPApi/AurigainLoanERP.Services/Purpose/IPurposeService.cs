using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.Purpose
{
    public interface IPurposeService
    {
        Task<ApiServiceResponseModel<List<PurposeModel>>> GetAllAsync(IndexModel model);
        Task<ApiServiceResponseModel<PurposeModel>> GetById(int id);
        Task<ApiServiceResponseModel<List<ddlPurposeModel>>> PurposeList();
        Task<ApiServiceResponseModel<string>> AddUpdateAsync(PurposeModel model);
        Task<ApiServiceResponseModel<object>> UpateActiveStatus(int id);
        Task<ApiServiceResponseModel<object>> UpdateDeleteStatus(int id);
    }
}
