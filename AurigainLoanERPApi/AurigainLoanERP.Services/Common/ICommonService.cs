using AurigainLoanERP.Shared.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.Common
{
    public interface ICommonService
    {

        Task<ApiServiceResponseModel<Dictionary<string, object>>> GetDropDown(string[] key);

    }
}
