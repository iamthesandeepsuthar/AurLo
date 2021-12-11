using AurigainLoanERP.Shared.Common.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.Common
{
    public interface ICommonService
    {

        Task<ApiServiceResponseModel<Dictionary<string, object>>> GetDropDown(string[] key);
        Task<ApiServiceResponseModel<Dictionary<string, object>>> GetFilterDropDown(FilterDropDownPostModel[] model);
    }
}
