using AurigainLoanERP.Services.JewellaryType;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AurigainLoanERP.Api.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JewellaryTypeController : ControllerBase
    {
        private readonly IJewellaryTypeService _type;
        public JewellaryTypeController(IJewellaryTypeService type)
        {
            _type = type;
        }
        // GET api/JewellaryType/JewellaryTypes
        [HttpGet("[action]")]
        public async Task<ApiServiceResponseModel<List<DDLJewellaryType>>> JewellaryTypes()
        {
            return await _type.JewellaryTypes();
        }
        // Post api/JewellaryType/GetList
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<List<JewellaryTypeModel>>> GetList(IndexModel model)
        {
            return await _type.GetAllAsync(model);
        }
        // POST api/JewellaryType/AddUpdate
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<string>> AddUpdate(JewellaryTypeModel model)
        {
            if (ModelState.IsValid)
            {
                return await _type.AddUpdateAsync(model);
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

        // GET api/JewellaryType/GetById/5
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<JewellaryTypeModel>> GetById(int id)
        {
            return await _type.GetById(id);

        }

        // DELETE api/JewellaryType/DeleteJewellaryType/5
        [HttpDelete("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> DeleteJewellaryType(int id)
        {
            return await _type.UpdateDeleteStatus(id);
        }
        // GET api/JewellaryType/ChangeActiveStatus/5
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> ChangeActiveStatus(int id)
        {
            return await _type.UpateActiveStatus(id);
        }
    }
}
