using System.Collections.Generic;

namespace AurigainLoanERP.Shared.Common.Model
{
    public class IndexModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
        public string OrderBy { get; set; }
        public bool OrderByAsc { get; set; }
        public long? UserId { get; set; }
        public IDictionary<string, object> AdvanceSearchModel { get; set; }

        public IndexModel()
        {
            PageSize = 10;
            OrderByAsc = true;
        }
    }
    public class LeadQueryModel : IndexModel
    {
        public int ProductCategoryId { get; set;}
    }
}
