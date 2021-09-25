using AurigainLoanERP.Data.ContractModel;
using AurigainLoanERP.Services.UserRoles;
using AurigainLoanERP.Shared.Common.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AurigainLoanERP.Api.Areas.Admin.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRole;
        public UserRoleController(IUserRoleService userRole)
        {
            _userRole = userRole;
        }

        // GET: api/<UserRoleController>
        [HttpPost]
        public async Task<ApiServiceResponseModel<List<UserRoleModel>>> Get([FromBody] IndexModel model)
        {
            return await _userRole.GetAsync(model);
        }

        // GET api/<UserRoleController>/5
        [HttpGet("{id}")]
        public async Task<ApiServiceResponseModel<UserRoleModel>> Get(int id)
        {
            return await _userRole.GetAsync(id);

        }

        // POST api/<UserRoleController>
        [HttpPost]
        public async Task<ApiServiceResponseModel<string>> Post(UserRolePostModel model)
        {
            if (ModelState.IsValid)
            {


                return await _userRole.AddUpdateAsync(model);
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

        // GET api/<UserRoleController>/5
        [HttpGet("{name}/{id}")]
        public async Task<ApiServiceResponseModel<object>> CheckRoleExist(string name, int? id = null)
        {
            return await _userRole.CheckRoleExist(name,id);
        }

        // GET api/<UserRoleController>/5
        [HttpGet("{id}/{status}")]
        public async Task<ApiServiceResponseModel<object>> ChangeActiveStatus(int id, bool? status = null)
        {
            return await _userRole.ChangeActiveStatus(id, status);
        }

        // DELETE api/<UserRoleController>/5
        [HttpDelete("{id}/{status}")]
        public async Task<ApiServiceResponseModel<object>> Delete(int id, bool? status = null)
        {
            return await _userRole.ChangeActiveStatus(id, status);
        }
    }
}
