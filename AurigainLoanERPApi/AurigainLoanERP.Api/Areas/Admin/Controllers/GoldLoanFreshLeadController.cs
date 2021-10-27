using AurigainLoanERP.Services.FreshLead;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AurigainLoanERP.Api.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoldLoanFreshLeadController : ControllerBase
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
    }
}
