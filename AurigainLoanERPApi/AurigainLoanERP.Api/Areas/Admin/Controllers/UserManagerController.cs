using AurigainLoanERP.Services.User;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static AurigainLoanERP.Shared.Enums.FixedValueEnums;

namespace AurigainLoanERP.Api.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        private readonly IUserService _userSerivce;
        public UserManagerController(IUserService userService)
        {
            _userSerivce = userService;
        }
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<List<UserManagerModel>>> ManagerList(IndexModel model)
        {
            return await _userSerivce.ManagersList(model);
        }
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<string>> AddUpdate([FromBody] UserManagerModel model)
        {
            if (ModelState.IsValid)
            {
                return await _userSerivce.AddUpdateManagerAsync(model);

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

        [HttpGet("[action]/{Id}")]
        public async Task<ApiServiceResponseModel<UserManagerModel>> GetById(long Id)
        {
            return await _userSerivce.UserManagerDetailAsync(Id);
        }
        // GET api/<AgentController>/5
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> UpdateActiveStatus(long id)
        {
            return await _userSerivce.UpateActiveStatus(id);
        }

        [HttpDelete("[action]/{Id}")]
        public async Task<ApiServiceResponseModel<object>> DeleteManager(long id)
        {
            return await _userSerivce.UpdateDeleteStatus(id);
        }
        [HttpGet("[action]")]
        public async Task<ApiServiceResponseModel<List<ReportingUser>>> GetReportingPersons()
        {
            return await _userSerivce.ReportingUsersAsync();
        }
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<object>> AssignReportingPerson(UserReportingPersonPostModel model)
        {
            if (ModelState.IsValid)
            {
                return await _userSerivce.AssignReportingPersonAsync(model);
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
