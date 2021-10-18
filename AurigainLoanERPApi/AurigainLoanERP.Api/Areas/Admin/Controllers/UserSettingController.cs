using AurigainLoanERP.Services.User;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AurigainLoanERP.Shared.Enums.FixedValueEnums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AurigainLoanERP.Api.Areas.Admin.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserSettingController : ControllerBase
    {
        private readonly IUserService _userSerivce;
        public UserSettingController(IUserService userService)
        {
            _userSerivce = userService;
        }

        [HttpPost]
        public async Task<ApiServiceResponseModel<string>> UpdateProfile([FromBody] UserSettingPostModel Model)
        {
            if (ModelState.IsValid)
            {
                return await  _userSerivce.UpdateProfile(Model);
                
            }
            else
            {
                ApiServiceResponseModel<string> obj = new ApiServiceResponseModel<string>();
                obj.Data = null;
                obj.IsSuccess = false;
                obj.Message = ResponseMessage.InvalidData;
                obj.Exception = ModelState.ErrorCount.ToString();
                obj.StatusCode = ((int)ApiStatusCode.Ok);

                return obj;
            }


        }

    }
}
