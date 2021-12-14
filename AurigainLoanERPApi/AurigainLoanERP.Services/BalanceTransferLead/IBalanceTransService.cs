using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.BalanceTransferLead
{
    public interface IBalanceTransService
    {
        Task<ApiServiceResponseModel<List<BTGoldLoanLeadListModel>>> BTGolddLoanLeadList(IndexModel model);
        Task<ApiServiceResponseModel<string>> AddUpdateBTGoldLoanExternalLeadAsync(BTGoldLoanLeadPostModel model);
        Task<ApiServiceResponseModel<BTGoldLoanLeadViewModel>> DetailbyIdAsync(long id);
        Task<ApiServiceResponseModel<string>> AddUpdateBTGoldLoanInternalLeadAsync(BTGoldLoanLeadPostModel model);
        Task<ApiServiceResponseModel<object>> UpdateLeadApprovalStageAsync(BtGoldLoanLeadApprovalStagePostModel model);
        Task<ApiServiceResponseModel<object>> UpdateLeadStatusAsync(LeadStatusModel model);
        Task<ApiServiceResponseModel<List<LeadStatusActionHistory>>> BTGoldLoanLeadStatusHistory(long leadId);
        Task<ApiServiceResponseModel<List<LeadStatusActionHistory>>> BTGoldLoanApprovalStatusHistory(long leadId);
    }
}
