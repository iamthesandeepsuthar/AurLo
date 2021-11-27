using AurigainLoanERP.Services.StateAndDistrict;
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
        private readonly IStateAndDistrictService _areaSerivce;

        public UserSettingController(IUserService userService, IStateAndDistrictService areaSerivce)
        {
            _userSerivce = userService;
            _areaSerivce = areaSerivce;
        }

        [HttpGet("{id}")]
        public async Task<ApiServiceResponseModel<UserViewModel>> GetUserProfile(long id)
        {
            return await _userSerivce.GetUserProfile(id);
        }


        [HttpPost]
        public async Task<ApiServiceResponseModel<string>> UpdateProfile([FromBody] UserSettingPostModel model)
        {
            if (ModelState.IsValid)
            {
                return await _userSerivce.UpdateProfile(model);

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

        [HttpGet("{id}")]
        public async Task<ApiServiceResponseModel<object>> UpdateApproveStatus(long id)
        {
            return await _userSerivce.UpdateApproveStatus(id);
        }


        [HttpPost]
        public async Task<ApiServiceResponseModel<string>> SetUserAvailibilty([FromBody] UserAvailibilityPostModel model)
        {
            return await _userSerivce.SetUserAvailibilty(model);

        }

        [HttpGet("{pinCode}/{roleId}/{id}")]
        public async Task<ApiServiceResponseModel<List<AvailableAreaModel>>> GetUserAvailableAreaForRolebyPinCode(string pinCode, int roleId,long id=0)
        {
            return await _areaSerivce.GetUserAvailableAreaAsync(pinCode, roleId,id);
        }

        [HttpGet("{id}")]
        public async Task<ApiServiceResponseModel<List<UserAvailabilityViewModel>>> GetUserAvailibiltyList(long id)
        {
            return await _userSerivce.GetUserAvailibilty(id);
        }

        [HttpDelete("{id}/{documentId}")]
        public async Task<ApiServiceResponseModel<object>> DeleteDocumentFile(long id,long documentId)
        {
            return await _userSerivce.DeleteDocumentFile(id, documentId);
        }
        [HttpGet("{id}")]
        public async Task<ApiServiceResponseModel<UserProfileModel>> GetProfile(long id) 
        {
            return await _userSerivce.GetProfile(id);
        }
        [HttpPost]
        public async Task<ApiServiceResponseModel<string>> UpdateProfileMobile(UserProfileModel model)
        {
            if (ModelState.IsValid)
            {
                return await _userSerivce.UpdateProfile(model);
            }
            else
            {
                ApiServiceResponseModel<string> obj = new ApiServiceResponseModel<string>();
                obj.Data = null;
                obj.IsSuccess = false;
                obj.Message = ResponseMessage.InvalidData;
                obj.Exception = ModelState.ErrorCount.ToString();
                obj.StatusCode = ((int)ApiStatusCode.BadRequest);
                return obj;
            }
        }
    }
}
