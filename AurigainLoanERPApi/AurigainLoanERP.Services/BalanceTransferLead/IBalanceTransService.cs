﻿using AurigainLoanERP.Shared.Common.Model;
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
        Task<ApiServiceResponseModel<List<BTGoldLoanBalanceReturnLeadListModel>>> BTGoldLoanBalanceReturnLeadList(IndexModel model);
        Task<ApiServiceResponseModel<BalanceTransferReturnViewModel>> BTGoldLoanDetailByLeadId(long id);
        Task<ApiServiceResponseModel<object>>AddUpdateBTGoldLoanLeadBalanceReturn(BalanceTranferReturnPostModel model);
        Task<ApiServiceResponseModel<BtGoldLoanLeadAppointmentViewModel>> GetAppointmentDetail(long leadId);
        Task<ApiServiceResponseModel<object>> SaveAppointment(BtGoldLoanLeadAppointmentPostModel model);
        Task<ApiServiceResponseModel<object>> DeleteBTGoldLoanLeadDocumentFile(long LeadId, long documentType, bool IsPOIPOADOC, string FileName = null);

    }
}
