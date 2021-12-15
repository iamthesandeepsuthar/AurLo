using AurigainLoanERP.Services.User;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static AurigainLoanERP.Shared.Enums.FixedValueEnums;

namespace AurigainLoanERP.Api.Areas.Admin.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoorStepAgentController : ControllerBase
    {
        private readonly IUserService _userSerivce;
        public DoorStepAgentController(IUserService userService)
        {
            _userSerivce = userService;
        }

        [HttpPost]
        public async Task<ApiServiceResponseModel<List<DoorStepAgentListModel>>> Get(IndexModel model)
        {
            return await _userSerivce.GetDoorStepAgentAsync(model);
        }

        [HttpPost]
        public async Task<ApiServiceResponseModel<string>> AddUpdate([FromBody] DoorStepAgentPostModel model)
        {
            if (ModelState.IsValid)
            {
                return await _userSerivce.AddUpdateDoorStepAgentAsync(model);

            }
            else
            {
                ApiServiceResponseModel<string> obj = new ApiServiceResponseModel<string>();
                obj.Data = null;
                obj.IsSuccess = false;
                obj.Message = ResponseMessage.InvalidData;
                obj.Exception = ModelState.ErrorCount.ToString();
                obj.StatusCode = (int)ApiStatusCode.InvaildModel;
                return obj;
            }
        }

        [HttpGet("{id}")]
        public async Task<ApiServiceResponseModel<DoorStepAgentViewModel>> GetById(long id)
        {
            return await _userSerivce.GetDoorStepAgentDetailAsync(id);
        }

        // DELETE api/<AgentController>/5
        [HttpDelete("{id}")]
        public async Task<ApiServiceResponseModel<object>> Delete(long id)
        {
            return await _userSerivce.UpdateDeleteStatus(id);
        }
        // GET api/<AgentController>/5
        [HttpGet("{id}")]
        public async Task<ApiServiceResponseModel<object>> UpdatActiveStatus(long id)
        {
            return await _userSerivce.UpateActiveStatus(id);
        }

        [HttpDelete("{id}/{documentId}")]
        public async Task<ApiServiceResponseModel<object>> DeleteDocumentFile(long id, long documentId)
        {
            return await _userSerivce.DeleteDocumentFile(id, documentId);
        }

        [HttpPost]
        public async Task<ApiServiceResponseModel<object>> AgentSecurityDepositAppUpdate(UserSecurityDepositPostModel model)
        {
            if (ModelState.IsValid)
            {
                return await _userSerivce.SaveDoorstepAgentSecurityDepositAsync(model);

            }
            else
            {
                ApiServiceResponseModel<object> obj = new ApiServiceResponseModel<object>();
                obj.Data = false;
                obj.IsSuccess = false;
                obj.Message = ResponseMessage.InvalidData;
                obj.Exception = ModelState.ErrorCount.ToString();
                obj.StatusCode = (int)ApiStatusCode.InvaildModel;
                return obj;
            }
        }
    }
}
