using Microsoft.AspNetCore.Mvc.Filters;

namespace AurigainLoanERP.Shared.Common.API
{
    public class InterseptionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;

            base.OnActionExecuting(context);
        }
    }
}
