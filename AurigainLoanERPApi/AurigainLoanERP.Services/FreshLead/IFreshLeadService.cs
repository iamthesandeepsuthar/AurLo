using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.FreshLead
{
    public interface IFreshLeadService
    {
        #region  <<Gold Loan>>
        Task<ApiServiceResponseModel<string>> SaveGoldLoanFreshLeadAsync(GoldLoanFreshLeadModel model);
        Task<ApiServiceResponseModel<List<GoldLoanFreshLeadListModel>>> GoldLoanFreshLeadListAsync(IndexModel model);
        #endregion
        #region <<Other Lead>>
        Task<ApiServiceResponseModel<string>> SaveFreshLeadHLCLPLAsync(FreshLeadHLPLCLModel model);
        #endregion
    }
}
