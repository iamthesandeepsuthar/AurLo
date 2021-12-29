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
        Task<ApiServiceResponseModel<List<LeadStatusActionHistory>>> FreshGoldLoanLeadStatusHistory(long leadId);
        #endregion
        #region <<Other Lead>>
        Task<ApiServiceResponseModel<string>> SaveFreshLeadHLCLPLAsync(FreshLeadHLPLCLModel model);
        Task<ApiServiceResponseModel<List<FreshLeadHLPLCLModel>>> FreshLeadHLPLCLList(LeadQueryModel model);
        Task<ApiServiceResponseModel<object>> UpdateLeadStatusOtherLeadAsync(LeadStatusModel model);
        Task<ApiServiceResponseModel<List<LeadStatusActionHistory>>> OtherLoanLeadStatusHistory(long leadId);
        Task<ApiServiceResponseModel<FreshLeadHLPLCLModel>> FreshHLPLCLDetail(long Id);
        Task<ApiServiceResponseModel<GoldLoanFreshLeadAppointmentDetailViewModel>> GetAppointmentDetailByLeadId(long Id);
        Task<ApiServiceResponseModel<object>> SaveAppointment(GoldLoanFreshLeadAppointmentDetailModel model);
        #endregion
    }
}
