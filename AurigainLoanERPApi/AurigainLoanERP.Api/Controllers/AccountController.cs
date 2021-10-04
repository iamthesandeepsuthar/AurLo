﻿using AurigainLoanERP.Services.Account;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using AurigainLoanERP.Shared.ExtensionMethod;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace AurigainLoanERP.Api.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet]
        public ApiServiceResponseModel<string> Login()
        {
            return  _security.CreateToken("abc", "admin");
        }
        
        //Post api/Account/GetOTP
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<OtpModel>> GetOTP(OtpRequestModel model)
        {
            return await _accountService.GetOtp(model);
        }

        //Post api/Account/ChangeMPIN
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<string>> ChangeMPIN(ChangePasswordModel model)
        {
            return await _accountService.ChangePassword(model);
        }
        //Post api/Account/Login
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<string>> Login(LoginModel model)
        {
            return await _accountService.Login(model);
        }
        //Post api/Account/VarifiedMPIN
        [HttpPost("[action]")]
        public async Task<ApiServiceResponseModel<string>> VarifiedMPIN(OtpVerifiedModel model)
        {
            return await _accountService.VerifiedPin(model);
        }
    }
}