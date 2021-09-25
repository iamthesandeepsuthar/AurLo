using AurigainLoanERP.Data.ContractModel;
using AurigainLoanERP.Shared.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.UserRoles
{
    public interface IUserRoleService
    {
        /// <summary>
        /// Get List of User Role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiServiceResponseModel<List<UserRoleModel>>> GetAsync(IndexModel model);
        /// <summary>
        /// Get Detail of Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiServiceResponseModel<UserRoleModel>> GetAsync(int id);
        /// <summary>
        /// Save Or Update Record 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiServiceResponseModel<string>> AddUpdateAsync(UserRolePostModel model);
        /// <summary>
        /// Check Role already Exist Or Not
        /// </summary>
        /// <param name="name">Role Name</param>
        /// <param name="id">id required in edit case</param>
        /// <returns></returns>
        Task<ApiServiceResponseModel<object>> CheckRoleExist(string name ,int? id=null);
        /// <summary>
        /// Record Mark as Active or deactive
        /// </summary>
        /// <param name="id">role id</param>
        /// <param name="status">true,false</param>
        /// <returns>true</returns>
        Task<ApiServiceResponseModel<object>> UpateActiveStatus(int id,bool? status);

        /// <summary>
        /// Record Mark as delete
        /// </summary>
        /// <param name="id">role id</param>
        /// <param name="status">true,false</param>
        /// <returns>true</returns>
        Task<ApiServiceResponseModel<object>> ChangeActiveStatus(int id, bool? status=null);

    }
}
