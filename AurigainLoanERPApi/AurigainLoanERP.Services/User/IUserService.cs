using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.User
{
    public interface IUserService
    {
        /// <summary>
        /// Get List of User Role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiServiceResponseModel<List<AgentListViewModel>>> GetAgentAsync(IndexModel model);
        /// <summary>
        /// Get Detail of Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiServiceResponseModel<AgentViewModel>> GetAgentDetailAsync(long id);
        Task<ApiServiceResponseModel<string>> AddUpdateAgentAsync(AgentPostModel model);

        Task<ApiServiceResponseModel<List<DoorStepAgentListModel>>> GetDoorStepAgentAsync(IndexModel model);
        Task<ApiServiceResponseModel<DoorStepAgentViewModel>> GetDoorStepAgentDetailAsync(long id);
        /// <summary>
        /// Save Or Update Record 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        Task<ApiServiceResponseModel<string>> AddUpdateDoorStepAgentAsync(DoorStepAgentPostModel model);
        /// <summary>
        /// Get User Profile
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiServiceResponseModel<UserViewModel>> GetUserProfile(long id);

        /// <summary>
        /// Record Mark as Active or deactive
        /// </summary>
        /// <param name="id">record id</param>
        /// <param name="status">true,false</param>
        /// <returns>true</returns>
        Task<ApiServiceResponseModel<object>> UpateActiveStatus(long id);
        /// <summary>
        /// Update Approve Status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiServiceResponseModel<object>> UpdateApproveStatus(long id);

        /// <summary>
        /// Delete Document File
        /// </summary>
        /// <param name="id"></param>
        /// <param name="documentId"></param>
        /// <returns></returns>
        Task<ApiServiceResponseModel<object>> DeleteDocumentFile(long id, long documentId);
        /// <summary>
        /// Get User Availibilty
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiServiceResponseModel<List<UserAvailabilityViewModel>>> GetUserAvailibilty(long id);

        /// <summary>
        /// Record Mark as delete
        /// </summary>
        /// <param name="id">record id</param>
        /// <param name="status">true,false</param>
        /// <returns>true</returns>
        Task<ApiServiceResponseModel<object>> UpdateDeleteStatus(long id);

        Task<ApiServiceResponseModel<string>> SetUserAvailibilty(UserAvailibilityPostModel model);

        /// <summary>
        /// Update User Profile
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiServiceResponseModel<string>> UpdateProfile(UserSettingPostModel model);

        /// <summary>
        ///  Add and Update Manager Type User 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApiServiceResponseModel<string>> AddUpdateManagerAsync(UserManagerModel model);
        Task<ApiServiceResponseModel<List<UserManagerModel>>> ManagersList(IndexModel model);
        Task<ApiServiceResponseModel<UserManagerModel>> UserManagerDetailAsync(long Id);
        Task<ApiServiceResponseModel<UserProfileModel>> GetProfile(long Id);
        Task<ApiServiceResponseModel<string>> UpdateProfile(UserProfileModel model);
        Task<ApiServiceResponseModel<List<ReportingUser>>> ReportingUsersAsync();
        Task<ApiServiceResponseModel<object>> AssignReportingPersonAsync(UserReportingPersonPostModel model);
    }
}
