using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.BalanceTransferLead
{
   public interface IBalanceTransService
    {
        Task<ApiServiceResponseModel<List<BTGoldLoanLeadListModel>>> BTGolddLoanLeadList(IndexModel model);
        Task<ApiServiceResponseModel<string>> AddUpdateAsync(BTGoldLoanLeadPostModel model);
    }
}
