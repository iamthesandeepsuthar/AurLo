using AurigainLoanERP.Data.Database;
using AurigainLoanERP.Shared.Common.Method;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using AurigainLoanERP.Shared.ExtensionMethod;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using static AurigainLoanERP.Shared.Enums.FixedValueEnums;

namespace AurigainLoanERP.Services.Account
{
    public class AccountService : BaseService, IAccountService
    {
        public readonly IMapper _mapper;
        private AurigainContext _db;
        private readonly Security _security;
        private readonly EmailHelper _emailHelper;
        private readonly SMSHelper _smsHelper;
        public AccountService(IMapper mapper, AurigainContext db, IConfiguration _configuration, IHostingEnvironment environment)
        {
            this._mapper = mapper;
            _security = new Security(_configuration);
            _db = db;
            _emailHelper = new EmailHelper(_configuration, environment);
            _smsHelper = new SMSHelper(_configuration);

        }
        public async Task<ApiServiceResponseModel<OtpModel>> GetOtp(OtpRequestModel model)
        {
            ApiServiceResponseModel<OtpModel> objModel = new ApiServiceResponseModel<OtpModel>();
            try
            {
                if (!model.IsResendOtp)
                {
                    Random random = new Random();
                    var randomNumber = random.Next(100000, 199999);
                    var otp_response = await _smsHelper.SendSMS(randomNumber.ToString(), model.MobileNumber);
                    if (!string.IsNullOrEmpty(otp_response.error))
                    {
                        return CreateResponse<OtpModel>(null, otp_response.error, false, ((int)ApiStatusCode.OtpInvalid));
                    }
                    var encrptOTP = _security.Base64Encode(randomNumber.ToString());
                    UserOtp otp = new UserOtp();
                    otp.IsVerify = false;
                    otp.Otp = encrptOTP;
                    otp.Mobile = model.MobileNumber;
                    otp.MessgeId = otp_response.msg_id[0];
                    otp.SessionStartOn = DateTime.Now.ToLocalTime();
                    otp.ExpireOn = DateTime.Now.ToLocalTime().AddSeconds(180);
                    await _db.UserOtp.AddAsync(otp);
                    await _db.SaveChangesAsync();
                    var response = _mapper.Map<OtpModel>(otp);
                    response.ExpireOn = response.ExpireOn.Value.ToLocalTime();
                    response.SessionStartOn = response.SessionStartOn.ToLocalTime();
                    return CreateResponse<OtpModel>(response, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {

                    var otp = await _db.UserOtp.Where(x => x.Mobile == model.MobileNumber).FirstOrDefaultAsync();
                    if (otp.ExpireOn.Value < DateTime.Now)
                    {
                        Random random = new Random();
                        var randomNumber = random.Next(100000, 199999);
                        var otp_response = await _smsHelper.SendSMS(randomNumber.ToString(), model.MobileNumber);
                        if (!string.IsNullOrEmpty(otp_response.error))
                        {
                            return CreateResponse<OtpModel>(null, otp_response.error, false, ((int)ApiStatusCode.OtpInvalid));
                        }
                        var encrptOTP = _security.Base64Encode(randomNumber.ToString());
                        otp.MessgeId = otp_response.msg_id[0];
                        otp.Otp = encrptOTP;
                        otp.SessionStartOn = DateTime.Now.ToLocalTime();
                        otp.ExpireOn = DateTime.Now.ToLocalTime().AddSeconds(180);
                        await _db.SaveChangesAsync();
                        var response = _mapper.Map<OtpModel>(otp);
                        response.ExpireOn = response.ExpireOn.Value.ToLocalTime();
                        response.SessionStartOn = response.SessionStartOn.ToLocalTime();
                        return CreateResponse<OtpModel>(response, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                    }
                    else
                    {
                        objModel.Data = null;
                        objModel.IsSuccess = false;
                        objModel.Message = "New otp send after 180 second";
                        return objModel;
                    }
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<OtpModel>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task<ApiServiceResponseModel<string>> VerifiedPin(OtpVerifiedModel model)
        {
            try
            {
                var otp = await _db.UserOtp.Where(x => x.Mobile == model.MobileNumber).FirstOrDefaultAsync();
                if (otp == null)
                {
                    return CreateResponse<string>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.RecordNotFound));
                }
                var encrptOTP = _security.Base64Encode(model.Otp);
                //if (otp.ExpireOn > DateTime.Now.ToLocalTime())
                //{
                    if (otp.Otp == encrptOTP)
                    {
                        _db.UserOtp.Remove(otp);
                        _db.SaveChanges();
                        return CreateResponse<string>(null, "Otp varified successful.", true, ((int)ApiStatusCode.Ok));
                    }
                    else
                    {
                        return CreateResponse<string>(null, "otp varification failed", false, ((int)ApiStatusCode.OTPVarificationFailed));
                    }
               //}
             //  else
             //  {
             //     return CreateResponse<string>(null, "Otp validity expaire, Generate new otp", false, ((int)ApiStatusCode.OTPValidityExpire));
             //}
            }
            catch (Exception ex)
            {
                return CreateResponse<string>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task<ApiServiceResponseModel<string>> ChangePassword(ChangePasswordModel model)
        {
            try
            {
                var user = await _db.UserMaster.Where(x => x.Mobile == model.MobileNumber).FirstOrDefaultAsync();
                if (user != null)
                {
                    user.Mpin = model.Password;
                    await _db.SaveChangesAsync();
                    return CreateResponse<string>(model.MobileNumber.ToString(), "pin update successful", true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<string>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<string>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task<ApiServiceResponseModel<LoginResponseModel>> Login(LoginModel model)
        {
            ApiServiceResponseModel<LoginResponseModel> ResponseObject = new Shared.Common.Model.ApiServiceResponseModel<LoginResponseModel>();
            LoginResponseModel response = new LoginResponseModel();
            try
            {
                if (model.Plateform == "mobile") // For mobile Permission
                {
                    var user = await _db.UserMaster.Where(x => x.Mobile == model.MobileNumber && x.Mpin == model.Password).Include(x => x.UserRole).FirstOrDefaultAsync();
                    if (user != null)
                    {
                        if (!user.IsApproved)
                        {
                            return CreateResponse<LoginResponseModel>(null, "Admin not approved your account, Please confirm with admin!", false, ((int)ApiStatusCode.UnApproved));
                        }
                        UserLoginLog log = new UserLoginLog
                        {
                            LoggedInTime = DateTime.Now,
                            LoggedOutTime = DateTime.Now.AddDays(30),
                            Mobile = model.MobileNumber,
                            UserId = user.Id
                        };
                        await _db.UserLoginLog.AddAsync(log);
                        var fresh_token = _security.CreateToken(user.Id, model.MobileNumber, user.UserRole.Name, user.UserRoleId);
                        if (!string.IsNullOrEmpty(fresh_token.Data))
                        {
                            user.Token = fresh_token.Data;
                        }
                        await _db.SaveChangesAsync();
                        response.UserId = user.Id;
                        response.Token = fresh_token.Data;
                        response.RoleId = user.UserRoleId;
                        response.RoleLevel = user.UserRole.UserRoleLevel;
                        response.UserName = user.UserName;
                        response.RoleName = user.UserRole.Name;
                    }
                    else
                    {
                        return CreateResponse<LoginResponseModel>(null, "You have not register with us,Please Signup", false, ((int)ApiStatusCode.RecordNotFound));
                    }
                }
                return CreateResponse<LoginResponseModel>(response, "Login Successful", true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {
                return CreateResponse<LoginResponseModel>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task<ApiServiceResponseModel<string>> CheckUserExist(string mobileNumber)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(mobileNumber))
                {
                    var user = await _db.UserMaster.Where(x => x.Mobile == mobileNumber).FirstOrDefaultAsync();
                    if (user != null)
                    {
                        return CreateResponse<string>(null, "User mobile number already exist", true, ((int)ApiStatusCode.AlreadyExist));
                    }
                    return CreateResponse<string>(null, "User mobile number not exist with system", true, ((int)ApiStatusCode.Ok));
                }
                return CreateResponse<string>(null, "User mobile number not exist with system", true, ((int)ApiStatusCode.BadRequest));
            }
            catch (Exception ex)
            {
                return CreateResponse<string>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task<ApiServiceResponseModel<LoginResponseModel>> WebLogin(LoginModel model)
        {
            ApiServiceResponseModel<LoginResponseModel> ResponseObject = new Shared.Common.Model.ApiServiceResponseModel<LoginResponseModel>();
            LoginResponseModel response = new LoginResponseModel();
            try
            {
                var encrptPassword = _security.Base64Encode(model.Password.Trim());
                var user = await _db.UserMaster.Where(x => (x.Mobile == model.MobileNumber.Trim() || x.Email == model.MobileNumber.Trim()) && x.Password == encrptPassword && !x.IsDelete).Include(x => x.UserRole).FirstOrDefaultAsync();
                if (user != null)
                {
                    if (!user.IsApproved && !user.IsActive.Value)
                    {
                        return CreateResponse<LoginResponseModel>(null, "Your account not activated, Please confirm with admin!", false, ((int)ApiStatusCode.UnApproved));
                    }
                    UserLoginLog log = new UserLoginLog
                    {
                        LoggedInTime = DateTime.Now,
                        LoggedOutTime = DateTime.Now.AddDays(30),
                        Mobile = user.Mobile,
                        UserId = user.Id
                    };
                    await _db.UserLoginLog.AddAsync(log);

                    var fresh_token = _security.CreateToken(user.Id, model.MobileNumber, user.UserRole.Name, user.UserRoleId);
                    if (!string.IsNullOrEmpty(fresh_token.Data))
                    {
                        user.Token = fresh_token.Data;
                    }
                    await _db.SaveChangesAsync();
                    response.UserId = user.Id;
                    response.Token = fresh_token.Data;
                    response.RoleId = user.UserRoleId;
                    response.RoleLevel = user.UserRole.UserRoleLevel;
                    response.UserName = user.UserName;
                    response.RoleName = user.UserRole.Name;
                }
                else
                {
                    return CreateResponse<LoginResponseModel>(null, "You are not registered with us. Please sign up.", false, ((int)ApiStatusCode.RecordNotFound));
                }
                return CreateResponse<LoginResponseModel>(response, "Login Successful", true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {
                return CreateResponse<LoginResponseModel>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task<ApiServiceResponseModel<string>> WebChangePassword(ChangePasswordModel model)
        {
            try
            {
                var encrptPassword = _security.Base64Encode(model.Password);
                var user = await _db.UserMaster.Where(x => x.Mobile == model.MobileNumber).FirstOrDefaultAsync();
                if (user != null)
                {
                    user.Password = encrptPassword;
                    await _db.SaveChangesAsync();
                    return CreateResponse<string>(model.MobileNumber.ToString(), "pin update successful", true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<string>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.RecordNotFound));
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<string>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        public ApiServiceResponseModel<string> GetEncrptedPassword(string value)
        {
            ApiServiceResponseModel<string> objModel = new ApiServiceResponseModel<string>();
            try
            {
                var encrptPassword = _security.Base64Encode(value);
                objModel.Data = encrptPassword;
                objModel.IsSuccess = true;
                return objModel;
            }
            catch (Exception ex)
            {
                objModel.Data = "";
                objModel.IsSuccess = false;
                objModel.Message = ex.Message + "-- " + ex.InnerException.Message;
                return objModel;
            }
        }
    }
}
