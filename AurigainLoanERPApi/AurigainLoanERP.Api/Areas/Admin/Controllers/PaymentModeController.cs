using AurigainLoanERP.Services.PaymentMode;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AurigainLoanERP.Api.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentModeController : ControllerBase
    {
        private readonly IPaymentModeService _mode;
        public PaymentModeController(IPaymentModeService mode)
        {
            _mode = mode;
        }
        // GET api/PaymentMode/PaymentModes
        [HttpGet("[action]")]
        public async Task<ApiServiceResponseModel<List<DDLPaymentModeModel>>> PaymentModes()
        {
            return await _mode.GetPaymentModes();
        }

        // POST api/PaymentMode/SubmitPaymentMode
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<string>> SubmitPaymentMode(PaymentModeModel model)
        {
            if (ModelState.IsValid)
            {
                return await _mode.AddUpdateAsync(model);
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

        // DELETE api/PaymentMode/DeletePaymentMode/5
        [HttpDelete("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> DeletePaymentMode(int id)
        {
            return await _mode.UpdateDeleteStatus(id);
        }

        // GET api/PaymentMode/GetPaymentModeById/5
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<PaymentModeModel>> GetPaymentModeById(int id)
        {
            return await _mode.GetById(id);
        }
    }
}
