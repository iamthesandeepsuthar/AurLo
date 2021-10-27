using AurigainLoanERP.Services.Bank;
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
    public class BankController : ControllerBase
    {
        private readonly IBankService _bank;
        public BankController(IBankService bank)
        {
            _bank = bank;
        }
        // GET api/Bank/Banks
        [HttpGet("[action]")]
        public async Task<ApiServiceResponseModel<List<DDLBankModel>>> Banks()
        {
            return await _bank.Banks();
        }

        // Post api/Bank/GetList
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<List<BankModel>>> GetList(IndexModel model)
        {
            return await _bank.GetAllAsync(model);
        }

        // POST api/Bank/AddUpdate
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<string>> AddUpdate(BankModel model)
        {
            if (ModelState.IsValid)
            {
                return await _bank.AddUpdateAsync(model);
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

        // GET api/Bank/GetById/5
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<BankModel>> GetById(int id)
        {
            return await _bank.GetById(id);
        }

        // DELETE api/Bank/DeleteBank/5
        [HttpDelete("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> DeleteBank(int id)
        {
            return await _bank.UpdateDeleteStatus(id);
        }

        // GET api/Bank/ChangeActiveStatus/5
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> ChangeActiveStatus(int id)
        {
            return await _bank.UpateActiveStatus(id);
        }

        // GET api/Bank/BranchByPincode/'311001'
        [HttpGet("[action]/{pincode}")]
        public async Task<ApiServiceResponseModel<List<AvailableBranchModel>>> BranchByPincode(string pincode)
        {
            return await _bank.BranchByPincode(pincode);
        }

        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<string>> UpdateBankLogo(BankLogoPostModel model)
        {
            return await _bank.UpdateLogo(model);
        }
    }
}
