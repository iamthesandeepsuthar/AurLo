using AurigainLoanERP.Data.Database;
using AurigainLoanERP.Shared.Common.Method;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace AurigainLoanERP.Services.UserRoles
{
    public class UserRoleService : BaseService, IUserRoleService
    {


        public readonly IMapper _mapper;
        private AurigainContext _db;
        public UserRoleService(IMapper mapper, AurigainContext db)
        {
            this._mapper = mapper;
            _db = db;
        }

        /// <summary>
        /// Get List Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiServiceResponseModel<List<UserRoleModel>>> GetAsync(IndexModel model)
        {
            ApiServiceResponseModel<List<UserRoleModel>> objResponse = new ApiServiceResponseModel<List<UserRoleModel>>();
            try
            {
                var result = (from role in _db.UserRole
                              where !role.IsDelete && (string.IsNullOrEmpty(model.Search) || role.Name.Contains(model.Search))
                              orderby (model.OrderByAsc == 1 && model.OrderBy == "Name" ? role.Name : "") ascending
                              orderby (model.OrderByAsc != 1 && model.OrderBy == "Name" ? role.Name : "") descending
                              select role);
                var data = await result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue).ToListAsync();
                objResponse.Data = _mapper.Map<List<UserRoleModel>>(data);


                if (result != null)
                {
                    return CreateResponse<List<UserRoleModel>>(objResponse.Data, ResponseMessage.Success, true, TotalRecord: result.Count());
                }
                else
                {
                    return CreateResponse<List<UserRoleModel>>(null, ResponseMessage.NotFound, true, TotalRecord: 0);
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<List<UserRoleModel>>(null, ResponseMessage.Fail, false, ex.Message ?? ex.InnerException.ToString());

            }

        }
        /// <summary>
        /// Get Detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiServiceResponseModel<UserRoleModel>> GetAsync(int id)
        {

            try
            {
                var result = await (from c1 in _db.UserRole
                                   // join st in _db.UserRole  on c1.ParentId equals st.Id

                                    where !c1.IsDelete && c1.IsActive.Value && c1.Id == id
                                    select c1 ).FirstOrDefaultAsync();

                if (result != null)
                {
                    return CreateResponse<UserRoleModel>(_mapper.Map<UserRoleModel>(result), ResponseMessage.Success, true);
                }
                else
                {
                    return CreateResponse<UserRoleModel>(null, ResponseMessage.NotFound, true);
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<UserRoleModel>(null, ResponseMessage.NotFound, false, ex.Message ?? ex.InnerException.ToString());

            }

        }
        /// <summary>
        /// Add And Update User Role 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Role Name</returns>
        public async Task<ApiServiceResponseModel<string>> AddUpdateAsync(UserRolePostModel model)
        {

            try
            {

                if (model.Id == 0)
                {
                    var userRole = _mapper.Map<UserRole>(model);
                    userRole.CreatedOn = DateTime.Now;
                    userRole.ParentId = model.ParentId > 0 ? model.ParentId : null;

                    var result = await _db.UserRole.AddAsync(userRole);

                }
                else
                {
                    var userRole = await _db.UserRole.FirstOrDefaultAsync(x => x.Id == model.Id);

                    userRole.Name = model.Name;
                    userRole.ParentId = model.ParentId > 0 ? model.ParentId : null;
                    userRole.IsActive = model.IsActive;
                    userRole.ModifiedOn = DateTime.Now;

                }
                await _db.SaveChangesAsync();
                return CreateResponse<string>(model.Name, model.Id > 0 ? ResponseMessage.Update : ResponseMessage.Save, true);


            }
            catch (Exception ex)
            {

                return CreateResponse<string>(null, ResponseMessage.Fail, false, ex.Message ?? ex.InnerException.ToString());

            }

        }

        /// <summary>
        /// Check Role already Exist Or Not
        /// </summary>
        /// <param name="name">Role Name</param>
        /// <param name="id">id required in edit case</param>
        /// <returns></returns>
        public async Task<ApiServiceResponseModel<object>> CheckRoleExist(string name, int? id = null)
        {
            try
            {
                UserRole objRole = await _db.UserRole.FirstOrDefaultAsync(r => !r.IsDelete && name.ToLower().Equals(r.Name.ToLower()) && (id == null || id == 0 || r.Id != id));

                if (objRole != null)
                {
                    return CreateResponse<object>(true, ResponseMessage.Found, true);

                }
                else
                {
                    return CreateResponse<object>(false, ResponseMessage.NotFound, true);

                }

            }
            catch (Exception)
            {

                return CreateResponse<object>(false, ResponseMessage.Fail, false);

            }
        }
        /// <summary>
        /// Record Mark as Active or deactive
        /// </summary>
        /// <param name="id">role id</param>
        /// <param name="status">true,false</param>
        /// <returns>true</returns>
        public async Task<ApiServiceResponseModel<object>> UpateActiveStatus(int id)
        {
            try
            {
                UserRole objRole = await _db.UserRole.FirstOrDefaultAsync(r => r.Id == id);
                objRole.IsActive = !objRole.IsActive;
                objRole.ModifiedOn = DateTime.Now;
                await _db.SaveChangesAsync();
                return CreateResponse<object>(true, ResponseMessage.Update, true);

            }
            catch (Exception)
            {

                return CreateResponse<object>(false, ResponseMessage.Fail, false);

            }
        }

        /// <summary>
        /// Record Mark as delete
        /// </summary>
        /// <param name="id">role id</param>
        /// <param name="status">true,false</param>
        /// <returns>true</returns>
        public async Task<ApiServiceResponseModel<object>> UpdateDeleteStatus(int id)
        {
            try
            {
                UserRole objRole = await _db.UserRole.FirstOrDefaultAsync(r => r.Id == id);
                objRole.IsDelete = !objRole.IsDelete;
                objRole.ModifiedOn = DateTime.Now;
                await _db.SaveChangesAsync();
                return CreateResponse<object>(true, ResponseMessage.Update, true);

            }
            catch (Exception)
            {

                return CreateResponse<object>(false, ResponseMessage.Fail, false);

            }
        }
    }
}
