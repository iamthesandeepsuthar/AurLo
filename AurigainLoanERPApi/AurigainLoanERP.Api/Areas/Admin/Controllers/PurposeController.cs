using AurigainLoanERP.Services.Purpose;
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
    public class PurposeController : ControllerBase
    {
        private readonly IPurposeService _purpose;
        public PurposeController(IPurposeService purpose)
        {
            _purpose = purpose;
        }
        // GET api/Purpose/PurposeList
        [HttpGet("[action]")]
        public async Task<ApiServiceResponseModel<List<ddlPurposeModel>>> PurposeList()
        {
            return await _purpose.PurposeList();
        }
        // Post api/Purpose/GetList
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<List<PurposeModel>>> GetList(IndexModel model)
        {
            return await _purpose.GetAllAsync(model);
        }
        // POST api/Purpose/AddUpdate
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<string>> AddUpdate(PurposeModel model)
        {
            if (ModelState.IsValid)
            {
                return await _purpose.AddUpdateAsync(model);
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

        // GET api/Purpose/GetById/5
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<PurposeModel>> GetById(int id)
        {
            return await _purpose.GetById(id);
        }
        // DELETE api/Purpose/DeletePurpose/5
        [HttpDelete("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> DeletePurpose(int id)
        {
            return await _purpose.UpdateDeleteStatus(id);
        }
        // GET api/Purpose/ChangeActiveStatus/5
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> ChangeActiveStatus(int id)
        {
            return await _purpose.UpateActiveStatus(id);
        }
    }
}
