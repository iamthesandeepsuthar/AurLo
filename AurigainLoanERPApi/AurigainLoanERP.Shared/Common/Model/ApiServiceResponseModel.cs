namespace AurigainLoanERP.Shared.Common.Model
{
    public class ApiServiceResponseModel<TResult> where TResult : class
    {

        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public TResult Data { get; set; }
        public string Exception { get; set; }
        public int? TotalRecord { get; set; }
    }

    public class FilterDropDownPostModel
    {
        public string Key { get; set; }
        public string FileterFromKey { get; set; }
        public int[] Values { get; set; }


    }
}
