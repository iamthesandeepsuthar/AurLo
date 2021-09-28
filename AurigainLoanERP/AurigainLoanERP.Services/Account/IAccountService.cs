using AurigainLoanERP.Data.ContractModel;
using AurigainLoanERP.Shared.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.Account
{
   public interface IAccountService
    {
        Task<ApiServiceResponseModel<OtpModel>>GetOtp(OtpRequestModel model);
        Task<ApiServiceResponseModel<string>> ChangePassword(ChangePasswordModel model);
        Task<ApiServiceResponseModel<string>> Login(LoginModel model);
        Task<ApiServiceResponseModel<string>> VerifiedPin(OtpVerifiedModel model);
    }
}
