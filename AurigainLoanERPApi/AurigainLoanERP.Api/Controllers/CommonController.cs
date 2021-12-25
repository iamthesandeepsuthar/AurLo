using AurigainLoanERP.Services.Common;
using AurigainLoanERP.Shared.Common.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AurigainLoanERP.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService _common;
        public CommonController(ICommonService common)
        {
            _common = common;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiServiceResponseModel<Dictionary<string, object>>> GetDropDown(string[] key)
        {
            return await _common.GetDropDown(key);
        }

        [HttpPost]
        public async Task<ApiServiceResponseModel<Dictionary<string, object>>> GetMultipleFilterDropDown(FilterDropDownPostModel[] model)
        {
            return await _common.GetFilterDropDown(model);
        }
        [HttpPost]
        public async Task<ApiServiceResponseModel<Dictionary<string, object>>> GetFilterDropDown(FilterDropDownPostModel model)
        {
            FilterDropDownPostModel[] obj = { model != null ? model : new FilterDropDownPostModel() };
            return await _common.GetFilterDropDown(obj);
        }
        [HttpGet("{id}")]
        public async Task<ApiServiceResponseModel<CommonModel>> GetCommonDataByUserId(long id)
        {
            return await _common.GetCommonDataByUserId(id);
        }

    }
}
