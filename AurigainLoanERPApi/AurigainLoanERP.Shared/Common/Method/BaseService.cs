﻿

using AurigainLoanERP.Shared.Common.Model;
using System;

namespace AurigainLoanERP.Shared.Common.Method
{
    public class BaseService
    {

        public readonly LoginUserViewModel _loginUserDetail;

        // public IConfiguration _configuration;
        public BaseService()
        {
            _loginUserDetail = new LoginUserViewModel();
            //  _configuration = configuration;
        }

        public virtual ApiServiceResponseModel<T> CreateResponse<T>(T objData, string Message, bool IsSuccess,int statusCode ,string exception = null, string validationMessage = null, int? TotalRecord = null) where T : class
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
            public int UserId { get; set; }
            public int RoleTypeId { get; set; }
            public string RoleType { get; set; }
            public int BaseRoleTypeId { get; set; }

            public string BaseRoleType { get; set; }
            public string Name { get; set; }


            public LoginUserViewModel()
            {



                //UserId = currentUser.Claims(c => c.Type == "Name");
                //UserId = staticClas.UserId;
                //RoleTypeId = staticClas.RoleTypeId;
                //RoleType = staticClas.RoleType;
                //BaseRoleTypeId = staticClas.BaseRoleTypeId;

                //BaseRoleType = staticClas.BaseRoleType;
                //Name = staticClas.Name;

                //use thi on method for retrive data from jwt
                // public object currentUser = HttpContext.User;

            }
        }


        public string GenerateUniqueId() {
            try
            {
                return new Random().Next(100000,999999).ToString();
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
