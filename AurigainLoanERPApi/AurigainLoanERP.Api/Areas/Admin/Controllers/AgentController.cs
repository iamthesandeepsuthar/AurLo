using AurigainLoanERP.Services.User;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AurigainLoanERP.Api.Areas.Admin.Controllers
{
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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AgentController>
        [HttpPost]
        public async Task<ApiServiceResponseModel<string>> Post([FromForm] AgentPostModel model)
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

        // PUT api/<AgentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AgentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
