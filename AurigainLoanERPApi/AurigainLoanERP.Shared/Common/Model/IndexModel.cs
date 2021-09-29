using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Shared.Common.Model
{
   public class IndexModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string OrderBy { get; set; }
        public int OrderByAsc { get; set; }

        public IDictionary<string, object> AdvanceSearchModel { get; set; }

        public IndexModel()
        {
            PageSize = 10;
            OrderByAsc = 1;
        }
    }
}
