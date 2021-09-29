using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
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
