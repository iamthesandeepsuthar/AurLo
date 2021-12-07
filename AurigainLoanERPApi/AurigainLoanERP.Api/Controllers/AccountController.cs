using AurigainLoanERP.Services.Account;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using AurigainLoanERP.Shared.ExtensionMethod;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace AurigainLoanERP.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly Security _security;
        private readonly IAccountService _accountService;
        public AccountController(IConfiguration _configuration, IAccountService accountService)
        {
            _security = new Security(_configuration);
            _accountService = accountService;
        }
        [HttpPost]
        public async Task<ApiServiceResponseModel<LoginResponseModel>> WebLogin(LoginModel model)
        {
            return await _accountService.WebLogin(model);           
        }
        
        //Post api/Account/GetOTP
        [HttpPost]
        public async Task<ApiServiceResponseModel<OtpModel>> GetOTP(OtpRequestModel model)
        {
            return await _accountService.GetOtp(model);
        }

        //Post api/Account/WebChangePassword
        [HttpPost]
        public async Task<ApiServiceResponseModel<string>>WebChangePassword(ChangePasswordModel model)
        {
            return await _accountService.WebChangePassword(model);
        }

        //Post api/Account/ChangeMPIN
        [HttpPost]
        public async Task<ApiServiceResponseModel<string>> ChangeMPIN(ChangePasswordModel model)
        {
            return await _accountService.ChangePassword(model);
        }

        //Post api/Account/Login
        [HttpPost]
        public async Task<ApiServiceResponseModel<LoginResponseModel>> Login(LoginModel model)
        {
            return await _accountService.Login(model);
        }

        //Post api/Account/VarifiedMPIN
        [HttpPost]
        public async Task<ApiServiceResponseModel<string>> VarifiedMPIN(OtpVerifiedModel model)
        {
            return await _accountService.VerifiedPin(model);
        }

        [HttpGet]
        //Get api/Account/ValidateUserWithMobileNUmber
        public async Task<ApiServiceResponseModel<string>> ValidateUserWithMobileNUmber(string mobileNumber) 
        {
            return await _accountService.CheckUserExist(mobileNumber);
        }

        [HttpGet("{value}")]
        public ApiServiceResponseModel<string> GenerateEncrptPassword(string value)
        {
            return _accountService.GetEncrptedPassword(value);
        }
    }
}
