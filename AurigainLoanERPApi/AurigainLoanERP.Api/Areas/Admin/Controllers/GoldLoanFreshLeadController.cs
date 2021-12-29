using AurigainLoanERP.Services.FreshLead;
using AurigainLoanERP.Shared.Common.API;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AurigainLoanERP.Api.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    
    public class GoldLoanFreshLeadController : ApiControllerBase
    {
        private readonly IFreshLeadService _freshLead;
        public GoldLoanFreshLeadController(IFreshLeadService freshLead)
        {
            _freshLead = freshLead;
        }
        // POST api/GoldLoanFreshLead/AddUpdateGoldLoanFreshLead
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<string>> AddUpdateGoldLoanFreshLead(GoldLoanFreshLeadModel model)
        {
            if (ModelState.IsValid)
            {
                return await _freshLead.SaveGoldLoanFreshLeadAsync(model);
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

        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<List<GoldLoanFreshLeadListModel>>> GoldLoanFreshLeadList(IndexModel model)
        {
            return await _freshLead.GoldLoanFreshLeadListAsync(model);
        }
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<GoldLoanFreshLeadViewModel>> GoldLoanFreshLeadDetail(long id)
        {
            return await _freshLead.FreshGoldLoanLeadDetailAsync(id);
        }

        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<List<FreshLeadHLPLCLModel>>> PersonalHomeCarLoanList(LeadQueryModel model)
        {
            return await _freshLead.FreshLeadHLPLCLList(model);

        }

        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<object>> UpdateLeadStatus(LeadStatusModel model) 
        {
            return await _freshLead.UpdateLeadStatusAsync(model);
        }
        [HttpGet("[action]/{leadId}")]
        public async Task<ApiServiceResponseModel<List<LeadStatusActionHistory>>> FreshGoldLoanLeadStatusHistory(long leadId)
        {
            return await _freshLead.FreshGoldLoanLeadStatusHistory(leadId);
        }
        [HttpGet("[action]/{id}")]
        //[AllowAnonymous]
        public async Task<ApiServiceResponseModel<GoldLoanFreshLeadAppointmentDetailViewModel>> AppointmentDetailById(long id)
        {
            return await _freshLead.GetAppointmentDetailByLeadId(id);
        }
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<object>> SaveAppointment(GoldLoanFreshLeadAppointmentDetailModel model)
        {
            return await _freshLead.SaveAppointment(model);
        }

    }
}
