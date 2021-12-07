

using AurigainLoanERP.Shared.Common.Model;
using Microsoft.AspNetCore.Http;
using System;

namespace AurigainLoanERP.Shared.Common.Method
{
    public class BaseService
    {

        public  LoginUserViewModel _loginUserDetail;

        // public IConfiguration _configuration;
        public BaseService()
        {
            _loginUserDetail = new LoginUserViewModel();
            //  _configuration = configuration;
        }

        public virtual ApiServiceResponseModel<T> CreateResponse<T>(T objData, string Message, bool IsSuccess, int statusCode, string exception = null, string validationMessage = null, int? TotalRecord = null) where T : class
        {
            ApiServiceResponseModel<T> objReturn = new ApiServiceResponseModel<T>();

            objReturn.Message = Message;
            objReturn.IsSuccess = IsSuccess;
            objReturn.StatusCode = statusCode;
            objReturn.Data = objData;
            objReturn.Exception = exception;
            objReturn.TotalRecord = TotalRecord;
            return objReturn;
        }

        public class LoginUserViewModel
        {
            public long? UserId { get; set; }
            public int? RoleId { get; set; }
            public string RoleName { get; set; }
            public string UserName { get; set; }


            public LoginUserViewModel()
            { 
                UserId = LoginUserModel.UserId ?? null;
                UserName = LoginUserModel.UserName;
                RoleId = LoginUserModel.RoleId ?? null;
                RoleName = LoginUserModel.RoleName;

            }
        }

        public string GenerateUniqueId()
        {
            try
            {
                return new Random().Next(100000, 999999).ToString();
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public string GetBaseUrl()
        {
            IHttpContextAccessor _httpContext = new HttpContextAccessor();

            return string.Concat(_httpContext.HttpContext.Request.IsHttps ? "https://" : "http://", _httpContext.HttpContext.Request.HttpContext.Request.Host.Value);
        }
    }

}
