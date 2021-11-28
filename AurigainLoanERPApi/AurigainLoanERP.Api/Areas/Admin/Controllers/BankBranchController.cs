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
    public class BankBranchController : ControllerBase
    {
        private readonly IBranchService _branch;
        public BankBranchController(IBranchService branch)
        {
            _branch = branch;
        }
        // GET api/BankBranch/Branches
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<List<DDLBranchModel>>> Branches(int id) // Bank Id
        {
            return await _branch.Branches(id);
        }

        // POST api/BankBranch/AddUpdate
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<string>> AddUpdate(BranchModel model)
        {
            if (ModelState.IsValid)
            {
                return await _branch.AddUpdateAsync(model);
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

        // GET api/BankBranch/GetById/5
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<BranchModel>> GetById(int id)
        {
            return await _branch.GetById(id);
        }

        // DELETE api/BankBranch/DeleteBranch/5
        [HttpDelete("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> DeleteBranch(int id)
        {
            return await _branch.UpdateDeleteStatus(id);
        }

        // GET api/Bank/ChangeActiveStatus/5
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> ChangeActiveStatus(int id)
        {
            return await _branch.UpateActiveStatus(id);
        }

        // GET api/BankBranch/GetById/5
        [HttpGet("[action]/{PinCode}")]
        public async Task<ApiServiceResponseModel<List<DDLBranchModel>>> BranchesbyPinCode(string PinCode)
        {
            return await _branch.BranchesbyPinCode(PinCode);
        }
         
    }
}
