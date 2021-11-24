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
using System.Collections.Generic;
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
        public AccountService(IMapper mapper, AurigainContext db, IConfiguration _configuration, IHostingEnvironment environment) 
        {
            this._mapper = mapper;
            _security = new Security(_configuration);
            _db = db;
            _emailHelper = new EmailHelper(_configuration,environment);

        }
        public async Task<ApiServiceResponseModel<OtpModel>> GetOtp(OtpRequestModel model) 
        {
            try
            {
                if (!model.IsResendOtp)
                {
                
                        var user = await _db.UserMaster.Where(x => x.Mobile == model.MobileNumber).FirstOrDefaultAsync();
                        if (user != null)
                        {
                            Random random = new Random();
                            var randowmNumber = random.Next(100000, 199999);
                            UserOtp otp = new UserOtp();
                            otp.IsVerify = false;
                            otp.Otp = randowmNumber.ToString();
                            otp.Mobile = model.MobileNumber;
                            otp.UserId = user.Id;
                            otp.SessionStartOn = DateTime.Now;
                            otp.ExpireOn = DateTime.Now.AddSeconds(180);
                            await _db.UserOtp.AddAsync(otp);
                            await _db.SaveChangesAsync();
                            var response = _mapper.Map<OtpModel>(otp);
                            return CreateResponse<OtpModel>(response, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                        }                  
                    
                    return CreateResponse<OtpModel>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }
                else
                {
                    var user = await _db.UserMaster.Where(x => x.Mobile == model.MobileNumber).FirstOrDefaultAsync();
                    if (user != null)
                    {
                        var otp = await _db.UserOtp.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                        Random random = new Random();
                        var randomNumber = random.Next(100000, 199999);
                        otp.Otp = randomNumber.ToString();
                        otp.SessionStartOn = DateTime.Now;
                        otp.ExpireOn = DateTime.Now.AddSeconds(180);
                        await _db.SaveChangesAsync();
                        var response = _mapper.Map<OtpModel>(otp);
                        return CreateResponse<OtpModel>(response, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                    }
                    return CreateResponse<OtpModel>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }
            }
            catch (Exception ex)
            {

                return CreateResponse<OtpModel>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

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
                    return CreateResponse<string>(model.MobileNumber, "pin update successful", true, ((int)ApiStatusCode.Ok));
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
                            return CreateResponse<LoginResponseModel>(null,"Admin not approved your account, Please confirm with admin!", false, ((int)ApiStatusCode.UnApproved));
                        }
                        UserLoginLog log = new UserLoginLog
                        {
                            LoggedInTime = DateTime.Now,
                            LoggedOutTime = DateTime.Now.AddDays(30),
                            UserId = user.Id
                        };
                        await _db.UserLoginLog.AddAsync(log);
                        var fresh_token = _security.CreateToken(model.MobileNumber, user.UserRole.Name);
                        if (!string.IsNullOrEmpty(fresh_token.Data))
                        {
                            user.Token = fresh_token.Data;
                        }
                        await _db.SaveChangesAsync();
                        response.UserId = user.Id;
                        response.Token = fresh_token.Data;
                        response.RoleId = user.UserRoleId;                                               
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
        public async Task<ApiServiceResponseModel<string>> VerifiedPin(OtpVerifiedModel model) 
        {
            try
            {
                var otp = await _db.UserOtp.Where(x => x.Mobile == model.MobileNumber && x.Otp == model.Otp).FirstOrDefaultAsync();
                if (otp == null)
                {
                    return CreateResponse<string>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }
                _db.UserOtp.Remove(otp);
                _db.SaveChanges();
                return CreateResponse<string>(null, "Otp varified successful.", true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {
                return CreateResponse<string>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task<ApiServiceResponseModel<string>> CheckUserExist(string mobileNumber) 
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(mobileNumber)) 
                {
                    var user =await  _db.UserMaster.Where(x => x.Mobile == mobileNumber).FirstOrDefaultAsync();
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
               // Dictionary<string, string> replaceValues = new Dictionary<string, string>();
               // replaceValues.Add("{{UserName}}", "Akash");
               //await _emailHelper.SendHTMLBodyMail("akash14singhal@gmail.com","test mail html", EmailPathConstant.RegisterTemplate, replaceValues);

                    var user = await _db.UserMaster.Where(x => (x.Mobile == model.MobileNumber.Trim() || x.Email == model.MobileNumber.Trim()) && x.Password == model.Password.Trim()  && !x.IsDelete ).Include(x => x.UserRole).FirstOrDefaultAsync();
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
                            UserId = user.Id
                        };
                        await _db.UserLoginLog.AddAsync(log);
                        var fresh_token = _security.CreateToken(model.MobileNumber, user.UserRole.Name);
                        if (!string.IsNullOrEmpty(fresh_token.Data))
                        {
                            user.Token = fresh_token.Data;
                        }
                        await _db.SaveChangesAsync();
                        response.UserId = user.Id;
                        response.Token = fresh_token.Data;
                        response.RoleId = user.UserRoleId;
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
    }
}
