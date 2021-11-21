using AurigainLoanERP.Services.BalanceTransferLead;
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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GoldLoanBalanceTransferController : ControllerBase
    {
        private readonly IBalanceTransService _objBTLead;
        public GoldLoanBalanceTransferController(IBalanceTransService objBTLead)
        {
            _objBTLead = objBTLead;

        }
        [HttpPost]
        public async Task<ApiServiceResponseModel<string>> AddUpdateGoldLoanFreshLead(BTGoldLoanLeadPostModel model)
        {
            if (ModelState.IsValid)
            {
                return await _objBTLead.AddUpdateAsync(model);
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
        public async Task<ApiServiceResponseModel<BTGoldLoanLeadViewModel>> DetailbyIdAsync(long id)
        {
            return await _objBTLead.DetailbyIdAsync(id);
    }
}
    
}
