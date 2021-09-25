using AurigainLoanERP.Shared.ExtensionMethod;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AurigainLoanERP.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly Security _security;

        public AccountController(IConfiguration _configuration)
        {
            _security = new Security(_configuration);

        }

        [HttpGet]
        public object Login()
        {
            return _security.CreateToken("abc", "admin");
        }



    }
}
