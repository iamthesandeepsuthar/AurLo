using AurigainLoanERP.Services.BalanceTransferLead;
using AurigainLoanERP.Shared.Common.API;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AurigainLoanERP.Api.Areas.Admin.Controllers
{
    [Route("api/[controller]/[action]")]
    public class GoldLoanBalanceTransferController : ApiControllerBase
    {
        private readonly IBalanceTransService _objBTLead;
        public GoldLoanBalanceTransferController(IBalanceTransService objBTLead)
        {
            _objBTLead = objBTLead;
        }
       // [AllowAnonymous]
        [HttpPost]
        public async Task<ApiServiceResponseModel<string>> AddUpdateBTGoldLoanExternalLead(BTGoldLoanLeadPostModel model)
        {
            if (ModelState.IsValid)
            {

                return await _objBTLead.AddUpdateBTGoldLoanExternalLeadAsync(model);
            }
            else
            {
                ApiServiceResponseModel<string> obj = new ApiServiceResponseModel<string>();
                obj.Data = null;
                obj.IsSuccess = false;
                obj.Message = ResponseMessage.InvalidData;
                obj.Exception = ModelState.ErrorCount.ToString();
                return obj;
            }
        }
        [HttpPost]
        public async Task<ApiServiceResponseModel<string>> AddUpdateBTGoldLoanInternalLead(BTGoldLoanLeadPostModel model)
        {
            if (ModelState.IsValid)
            {
                return await _objBTLead.AddUpdateBTGoldLoanInternalLeadAsync(model);
            }
            else
            {
                ApiServiceResponseModel<string> obj = new ApiServiceResponseModel<string>();
                obj.Data = null;
                obj.IsSuccess = false;
                obj.Message = ResponseMessage.InvalidData;
                obj.Exception = ModelState.ErrorCount.ToString();
                return obj;
            }
        }
        [HttpPost]
        public async Task<ApiServiceResponseModel<List<BTGoldLoanLeadListModel>>> BTGoldLoanLeadList(IndexModel model)
        {
            return await _objBTLead.BTGolddLoanLeadList(model);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ApiServiceResponseModel<BTGoldLoanLeadViewModel>> DetailById(long id)
        {
            return await _objBTLead.DetailbyIdAsync(id);
        }
        [HttpPost]
        public async Task<ApiServiceResponseModel<object>> UpdateLeadApprovalStage(BtGoldLoanLeadApprovalStagePostModel model)
        {
            return await _objBTLead.UpdateLeadApprovalStageAsync(model);
        }

        [HttpPost]
        public async Task<ApiServiceResponseModel<object>> UpdateLeadStatus(LeadStatusModel model)
        {
            return await _objBTLead.UpdateLeadStatusAsync(model);
        }
        [HttpGet("{leadId}")]
        public async Task<ApiServiceResponseModel<List<LeadStatusActionHistory>>> BTGoldLoanLeadStatusHistory(long leadId)
        {
            return await _objBTLead.BTGoldLoanLeadStatusHistory(leadId);
        }

        [HttpGet("{leadId}")]
        public async Task<ApiServiceResponseModel<List<LeadStatusActionHistory>>> BTGoldLoanApprovalStatusHistory(long leadId)
        {
            return await _objBTLead.BTGoldLoanApprovalStatusHistory(leadId);
        }
        //[AllowAnonymous]
        [HttpPost]
        public async Task<ApiServiceResponseModel<List<BTGoldLoanBalanceReturnLeadListModel>>> BTGoldLoanBalanceReturnLeadList(IndexModel model)
        {
            return await _objBTLead.BTGoldLoanBalanceReturnLeadList(model);
        }
        [HttpGet("{id}")]
        //[AllowAnonymous]
        public async Task<ApiServiceResponseModel<BalanceTransferReturnViewModel>> BTGoldLoanLeadDetailForBalanceReturn(long id)
        {
            return await _objBTLead.BTGoldLoanDetailByLeadId(id);
        }
       /// <summary>
        /// Save Balance Transfer Return 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// Success or Fail In Boolean Flag
        /// </returns>
        [HttpPost]
      //  [AllowAnonymous]
        public async Task<ApiServiceResponseModel<object>> AddUpdateBTGoldLoanLeadBalanceRetrun(BalanceTranferReturnPostModel model)
        {
            if (ModelState.IsValid)
            {
                return await _objBTLead.AddUpdateBTGoldLoanLeadBalanceReturn(model);    
            }
            else
            {
                ApiServiceResponseModel<object> obj = new ApiServiceResponseModel<object>();
                obj.Data = false;
                obj.IsSuccess = false;
                obj.Message = ResponseMessage.InvalidData;
                obj.Exception = ModelState.ErrorCount.ToString();
                return obj;
            }
        }

        [HttpGet("{id}")]
        //[AllowAnonymous]
        public async Task<ApiServiceResponseModel<BtGoldLoanLeadAppointmentViewModel>> AppointmentDetailById(long id)
        {
            return await _objBTLead.GetAppointmentDetail(id);
        }
        [HttpPost]
       // [AllowAnonymous]
        public async Task<ApiServiceResponseModel<object>> SaveAppointment(BtGoldLoanLeadAppointmentPostModel model)
        {
            return await _objBTLead.SaveAppointment(model);
        }
    }
}
