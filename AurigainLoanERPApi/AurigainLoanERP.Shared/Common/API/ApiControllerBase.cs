using AurigainLoanERP.Shared.Common.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AurigainLoanERP.Shared.Common.API
{
    [ApiController]
    [ServiceFilter(typeof(InterseptionAttribute))]
    [Authorize]
    public abstract class ApiControllerBase : ControllerBase
    {
        public ApiControllerBase()
        {
            SetLoginUserDetail();
        }


        private void SetLoginUserDetail()
        {
            try
            {
                var user = new HttpContextAccessor()?.HttpContext?.User;

                if (user != null && user.Claims.Count() > 0)
                {

                    LoginUserModel.UserId = user.HasClaim(x => x.Type == TokenClaimsConstant.UserId) ? (long?)Convert.ToInt64(user.FindFirst(TokenClaimsConstant.UserId).Value) : null;

                    LoginUserModel.UserName = user.HasClaim(x => x.Type == TokenClaimsConstant.UserName) ? user.FindFirst(TokenClaimsConstant.UserName).Value : null;

                    LoginUserModel.RoleId = user.HasClaim(x => x.Type == TokenClaimsConstant.RoleId) ? (int?)Convert.ToInt32(user.FindFirst(TokenClaimsConstant.RoleId).Value) : null;

                    LoginUserModel.RoleName = user.HasClaim(x => x.Type == TokenClaimsConstant.RoleName) ? user.FindFirst(TokenClaimsConstant.RoleName).Value : null;

                    LoginUserModel.LoginTime = user.HasClaim(x => x.Type == TokenClaimsConstant.GenerateTime) ? (DateTime?)DateTime.ParseExact(user.FindFirst(TokenClaimsConstant.GenerateTime).Value, "dd-mm-yyyy HH:mm:ss", null) : null;

                }

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
