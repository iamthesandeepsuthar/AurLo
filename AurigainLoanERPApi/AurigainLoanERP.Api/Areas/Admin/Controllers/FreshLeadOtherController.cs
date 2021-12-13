using AurigainLoanERP.Services.FreshLead;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AurigainLoanERP.Api.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreshLeadOtherController : ControllerBase
    {
        private readonly IFreshLeadService _freshLead;
        public FreshLeadOtherController(IFreshLeadService freshLead)
        {
            _freshLead = freshLead;
        }
        // POST api/FreshLeadOther/AddUpdateFreshLeadOther
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<string>> AddUpdateFreshLeadOther(FreshLeadHLPLCLModel model)
        {
            if (ModelState.IsValid)
            {
                return await _freshLead.SaveFreshLeadHLCLPLAsync(model);
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
        public async Task<ApiServiceResponseModel<object>> UpdateLeadStatus(LeadStatusModel model)
        {
            return await _freshLead.UpdateLeadStatusOtherLeadAsync(model);
        }
    }
}
