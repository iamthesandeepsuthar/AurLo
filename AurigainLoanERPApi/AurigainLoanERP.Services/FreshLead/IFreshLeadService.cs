using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.FreshLead
{
    public interface IFreshLeadService
    {
        #region  <<Gold Loan>>
        Task<ApiServiceResponseModel<string>> SaveGoldLoanFreshLeadAsync(GoldLoanFreshLeadModel model);
        Task<ApiServiceResponseModel<List<GoldLoanFreshLeadListModel>>> GoldLoanFreshLeadListAsync(IndexModel model);
        Task<ApiServiceResponseModel<GoldLoanFreshLeadViewModel>> FreshGoldLoanLeadDetailAsync(long id);
        Task<ApiServiceResponseModel<object>> UpdateLeadStatusAsync(LeadStatusModel model);
        #endregion
        #region <<Other Lead>>
        Task<ApiServiceResponseModel<string>> SaveFreshLeadHLCLPLAsync(FreshLeadHLPLCLModel model);
        Task<ApiServiceResponseModel<List<FreshLeadHLPLCLModel>>> FreshLeadHLPLCLList(IndexModel model);
        Task<ApiServiceResponseModel<object>> UpdateLeadStatusOtherLeadAsync(LeadStatusModel model);
        #endregion
    }
}
