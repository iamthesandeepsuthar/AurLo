using AurigainLoanERP.Services.UserRoles;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AurigainLoanERP.Api.Areas.Admin.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRole;
        public UserRoleController(IUserRoleService userRole)
        {
            _userRole = userRole;
        }

        // GET: api/<UserRoleController>
        [HttpPost]
        public async Task<ApiServiceResponseModel<List<UserRoleViewModel>>> Get(IndexModel model)
        {
            return await _userRole.GetAsync(model);
        }

        // GET api/<UserRoleController>/5
        [HttpGet("{id}")]
        public async Task<ApiServiceResponseModel<UserRoleViewModel>> Get(int id)
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
        [HttpGet("{id}")]
        public async Task<ApiServiceResponseModel<object>> ChangeActiveStatus(int id)
        {
            return await _userRole.UpateActiveStatus(id);
        }

        // DELETE api/<UserRoleController>/5
        [HttpDelete("{id}")]
        public async Task<ApiServiceResponseModel<object>> Delete(int id)
        {
            return await _userRole.UpdateDeleteStatus(id);
        }
    }
}
