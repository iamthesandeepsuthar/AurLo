using AurigainLoanERP.Services.StateAndDistrict;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AurigainLoanERP.Api.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateAndDistrictController : ControllerBase
    {
        private readonly IStateAndDistrictService _serivce;
        public StateAndDistrictController(IStateAndDistrictService service)
        {
            _serivce =service;
        }
        // GET api/StateAndDistrict/States
        [HttpGet("[action]")]
        public async Task<ApiServiceResponseModel<List<DDLStateModel>>> States()
        {
            return await _serivce.GetStates();
        }
        // GET api/StateAndDistrict/Districts
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<List<DDLDistrictModel>>> Districts(int id)
        {
            return await _serivce.GetDistricts(id);
        }

        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<List<StateModel>>> GetStateList(IndexModel model)
        {
            return await _serivce.GetAllStateAsync(model);
        }
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<List<DistrictModel>>> GetDistrictList(IndexModel model)
        {
            return await _serivce.GetAllDistrictAsync(model);
        }
        // POST api/StateAndDistrict/SubmitState
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<string>> SubmitState(StateModel model)
        {
            if (ModelState.IsValid)
            {
                return await _serivce.AddUpdateStateAsync(model);
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

        // POST api/StateAndDistrict/SubmitDistrict
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<string>> SubmitDistrict(DistrictModel model)
        {
            if (ModelState.IsValid)
            {
                return await _serivce.AddUpdateDistrictAsync(model);
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

        // DELETE api/StateAndDistrict/DeleteState/5
        [HttpDelete("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> DeleteState(int id)
        {
            return await _serivce.UpdateStateDeleteStatus(id);
        }
        // DELETE api/StateAndDistrict/DeleteDistrict/5
        [HttpDelete("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> DeleteDistrict(long id)
        {
            return await _serivce.UpdateDistrictDeleteStatus(id);
        }
        // GET api/StateAndDistrict/GetStateById/5
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<StateModel>> GetStateById(int id)
        {
            return await _serivce.GetStateById(id);

        }
        // GET api/StateAndDistrict/GetDistrictById/5
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<DistrictModel>> GetDistrictById(long id)
        {
            return await _serivce.GetDistrictById(id);

        }
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> ChangeStateActiveStatus(int id)
        {
            return await _serivce.UpateStateActiveStatus(id);
        }
        [HttpGet("[action]/{id}")]
        public async Task<ApiServiceResponseModel<object>> ChangeDistrictActiveStatus(int id)
        {
            return await _serivce.UpateDistrictActiveStatus(id);
        }
        [HttpGet("[action]/{pincode}")]
        public async Task<ApiServiceResponseModel<List<AvailableAreaModel>>> AreaByPincode(string pincode)
        {
            return await _serivce.GetAreaByPincode(pincode);
        }     

    }
}
