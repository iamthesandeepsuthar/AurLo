using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.User
{
    public  interface IUserService
    {
        /// <summary>
        /// Get List of User Role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiServiceResponseModel<List<AgentViewModel>>> GetAgentAsync(IndexModel model);
        /// <summary>
        /// Get Detail of Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiServiceResponseModel<AgentViewModel>> GetAgentDetailAsync(long id);

        Task<ApiServiceResponseModel<DoorStepAgentViewModel>> GetDoorStepAgentDetailAsync(long id);
        /// <summary>
        /// Save Or Update Record 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiServiceResponseModel<string>> AddUpdateAgentAsync(AgentPostModel model);

        Task<ApiServiceResponseModel<string>> AddUpdateDoorStepAgentAsync(DoorStepAgentPostModel model);

        /// <summary>
        /// Record Mark as Active or deactive
        /// </summary>
        /// <param name="id">record id</param>
        /// <param name="status">true,false</param>
        /// <returns>true</returns>
        Task<ApiServiceResponseModel<object>> UpateActiveStatus(long id);

        /// <summary>
        /// Record Mark as delete
        /// </summary>
        /// <param name="id">record id</param>
        /// <param name="status">true,false</param>
        /// <returns>true</returns>
        Task<ApiServiceResponseModel<object>> UpdateDeleteStatus(long id);
    }
}
