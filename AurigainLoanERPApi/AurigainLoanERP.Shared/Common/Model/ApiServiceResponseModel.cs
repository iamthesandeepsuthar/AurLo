
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Shared.Common.Model
{
    public class ApiServiceResponseModel<T> where T : class
    {

        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public string Exception { get; set; }
        public int? TotalRecord { get; set; }
    }

    public class FilterDropDownPostModel {
        public string Key { get; set; }
        public string FileterFromKey { get; set; }
        public int[] Values { get; set; }


    }
}
