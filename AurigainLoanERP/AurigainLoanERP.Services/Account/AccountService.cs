using AurigainLoanERP.Data.ContractModel;
using AurigainLoanERP.Data.Database;
using AurigainLoanERP.Shared.Common.Method;
using AurigainLoanERP.Shared.Common.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.Account
{
    public class AccountService : BaseService, IAccountService
    {
        public readonly IMapper _mapper;
        private AurigainContext _db;
        public AccountService(IMapper mapper, AurigainContext db)
        {
            this._mapper = mapper;
            _db = db;
        }
        public async Task<ApiServiceResponseModel<OtpModel>> GetOtp(OtpRequestModel model)
        {
            try
            {
                if (!model.IsResendOtp)
                {
                    var user = await _db.UserMasters.Where(x => x.Mobile == model.MobileNumber).FirstOrDefaultAsync();
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
                        
                        await _db.UserOtps.AddAsync(otp);
                        await _db.SaveChangesAsync();
                        var response = _mapper.Map<OtpModel>(otp);
                        return CreateResponse<OtpModel>(response, ResponseMessage.Success, true);
                    }
                    return CreateResponse<OtpModel>(null, ResponseMessage.NotFound, true);
                }
                else
                {
                    var user = await _db.UserMasters.Where(x => x.Mobile == model.MobileNumber).FirstOrDefaultAsync();
                    if (user != null)
                    {
                        var otp = await _db.UserOtps.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                        Random random = new Random();
                        var randomNumber = random.Next(100000, 199999);
                        otp.Otp = randomNumber.ToString();
                        otp.SessionStartOn = DateTime.Now;
                        otp.ExpireOn = DateTime.Now.AddSeconds(180);                       
                        await _db.SaveChangesAsync();
                        var response = _mapper.Map<OtpModel>(otp);
                        return CreateResponse<OtpModel>(response, ResponseMessage.Success, true);
                    }
                    return CreateResponse<OtpModel>(null, ResponseMessage.NotFound, true);
                }
            }
            catch (Exception ex)
            {

                return CreateResponse<OtpModel>(null, ResponseMessage.NotFound, false, ex.Message ?? ex.InnerException.ToString());

            }

        }
        public async Task<ApiServiceResponseModel<string>> ChangePassword(ChangePasswordModel model)
        {
            try
            {
                var user = await _db.UserMasters.Where(x => x.Mobile == model.MobileNumber).FirstOrDefaultAsync();
                if (user != null)
                {
                    user.Mpin = model.Password;
                    await _db.SaveChangesAsync();
                    return CreateResponse<string>(model.MobileNumber,"pin update successful", true);
                }
                else
                {
                    return CreateResponse<string>(null, ResponseMessage.NotFound, true);
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<string>(null, ResponseMessage.NotFound, false, ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task<ApiServiceResponseModel<string>> Login(LoginModel model)
        {
            try
            {
                if (model.Plateform == "mobile") // For mobile Permission
                {
                    var user = await _db.UserMasters.Where(x => x.Mobile == model.MobileNumber && x.Mpin == model.Password).FirstOrDefaultAsync();                    
                    if (user == null)
                    {
                        UserLoginLog log = new UserLoginLog {
                            LoggedInTime = DateTime.Now,
                            UserId = user.Id
                        };
                        await  _db.UserLoginLogs.AddAsync(log);
                        await _db.SaveChangesAsync();
                        return CreateResponse<string>(null, ResponseMessage.NotFound, true);                        
                    }
                    var data = user.UserAgents as UserAgent;
                    return CreateResponse<string>(data.FullName, "Login Successful", true);
                }
                else // For web permission 
                {
                    var user = await _db.UserMasters.Where(x => x.Mobile == model.MobileNumber && x.Mpin == model.Password).FirstOrDefaultAsync();
                    if (user == null)
                    {
                        UserLoginLog log = new UserLoginLog
                        {
                            LoggedInTime = DateTime.Now,
                            UserId = user.Id
                        };
                        await _db.UserLoginLogs.AddAsync(log);
                        await _db.SaveChangesAsync();
                        return CreateResponse<string>(null, ResponseMessage.NotFound, true);
                    }
                    var data = user.UserAgents as UserAgent;
                    return CreateResponse<string>(data.FullName, "Login Successful", true);
                }              
            }
            catch (Exception ex) {
                return CreateResponse<string>(null, ResponseMessage.NotFound, false, ex.Message ?? ex.InnerException.ToString());
            }
        }

        public async Task<ApiServiceResponseModel<string>> VerifiedPin(OtpVerifiedModel model) 
        {
            try
            {
                var otp =await  _db.UserOtps.Where(x => x.Mobile == model.MobileNumber && x.Otp == model.Otp).FirstOrDefaultAsync();
                if (otp == null) 
                {
                    return CreateResponse<string>(null, ResponseMessage.NotFound, true);
                }
                _db.UserOtps.Remove(otp);
                _db.SaveChanges();
                return CreateResponse<string>(null, "Otp varified successful.", true);
            }
            catch (Exception ex)
            {
                return CreateResponse<string>(null, ResponseMessage.NotFound, false, ex.Message ?? ex.InnerException.ToString());
            }
        }
    }
}
