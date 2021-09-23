

using AurigainLoanERP.Shared.Common.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

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

        public virtual ApiServiceResponseModel<T> CreateResponse<T>(T objData, string Message, bool IsSuccess, string exception = "", string validationMessage = "") where T : class
        {
            ApiServiceResponseModel<T> objReturn = new ApiServiceResponseModel<T>();

            objReturn.Message = Message;
            objReturn.IsSuccess = IsSuccess;
            objReturn.Data = objData;
            objReturn.Exception = exception;

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
    }
}
