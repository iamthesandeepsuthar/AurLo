using AurigainLoanERP.Services.User;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AurigainLoanERP.Api.Areas.Admin.Controllers
{
   // [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IUserService _userSerivce;
        public AgentController(IUserService userService)
        {
            _userSerivce = userService;
        }

        // GET: api/<AgentController>
        [HttpGet]
        public IEnumerable<string> Get(IndexModel model)
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AgentController>/5
        [HttpGet("{id}")]
        public async Task<ApiServiceResponseModel<AgentViewModel>> GetById(long id)
        {
            return  await _userSerivce.GetAgentDetailAsync(id);
        }

        // POST api/<AgentController>
        [HttpPost]
        public async Task<ApiServiceResponseModel<string>> AddUpdate([FromBody] AgentPostModel model)
        {
            if (ModelState.IsValid)
            {
                return await _userSerivce.AddUpdateAgentAsync(model);

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
            return await _userSerivce.UpdateDeleteStatus(id);
        }
    }
}
