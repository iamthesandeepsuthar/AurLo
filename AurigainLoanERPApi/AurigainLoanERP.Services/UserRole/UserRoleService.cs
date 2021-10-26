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
using static AurigainLoanERP.Shared.Enums.FixedValueEnums;

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

        public async Task<ApiServiceResponseModel<List<DDLUserRole>>> Roles() 
        {
            try
            {
                var roles = await _db.UserRole.Select(x => new DDLUserRole
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

                if (roles.Count() > 0)
                {
                    return CreateResponse<List<DDLUserRole>>(roles, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<List<DDLUserRole>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<List<DDLUserRole>>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        /// <summary>
        /// Get List Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiServiceResponseModel<List<UserRoleViewModel>>> GetAsync(IndexModel model)
        {
            ApiServiceResponseModel<List<UserRoleViewModel>> objResponse = new ApiServiceResponseModel<List<UserRoleViewModel>>();
            try
            {
                var result = (from role in _db.UserRole
                              join c2 in _db.UserRole on role.ParentId equals c2.Id
                               into parentRoleData
                              from c2 in parentRoleData.DefaultIfEmpty()
                              where !role.IsDelete && (string.IsNullOrEmpty(model.Search) || role.Name.Contains(model.Search) || c2.Name.Contains(model.Search))
                              select new
                              {
                                  role = role,
                                  parentRole = c2
                              });
                switch (model.OrderBy)
                {
                    case "Name":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.role.Name ascending select orderData) : (from orderData in result orderby orderData.role.Name descending select orderData);
                        break;
                    case "ParentRole":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.parentRole.Name ascending select orderData) : (from orderData in result orderby orderData.parentRole.Name descending select orderData);
                        break;
                    case "IsActive":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.role.IsActive ascending select orderData) : (from orderData in result orderby orderData.role.IsActive descending select orderData);
                        break;
                    default:
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.role.Name ascending select orderData) : (from orderData in result orderby orderData.role.Name descending select orderData);
                        break;
                }

                var data = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);
                objResponse.Data = await (from c1 in data
                                          select new UserRoleViewModel
                                          {
                                              Id = c1.role.Id,
                                              Name = c1.role.Name,
                                              ParentId = c1.role.ParentId ?? null,
                                              IsActive = c1.role.IsActive,
                                              ParentRole = c1.parentRole.Name ?? string.Empty,
                                              IsDelete = c1.role.IsDelete,
                                              CreatedOn = c1.role.CreatedOn,
                                              CreatedBy = c1.role.CreatedBy
                                          }).ToListAsync();


                if (result != null)
                {
                    return CreateResponse<List<UserRoleViewModel>>(objResponse.Data, ResponseMessage.Success, true,((int)ApiStatusCode.Ok) ,TotalRecord: result.Count());
                }
                else
                {
                    return CreateResponse<List<UserRoleViewModel>>(null, ResponseMessage.NotFound, true,((int)ApiStatusCode.RecordNotFound) ,TotalRecord: 0);
                }

            }
            catch (Exception ex)
            {
                return CreateResponse<List<UserRoleViewModel>>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException),ex.Message ?? ex.InnerException.ToString());
            }

        }
        /// <summary>
        /// Get Detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiServiceResponseModel<UserRoleViewModel>> GetAsync(int id)
        {

            try
            {
                var result = await (from c1 in _db.UserRole
                                    join c2 in _db.UserRole on c1.ParentId equals c2.Id
                                    where !c1.IsDelete && c1.IsActive.Value && c1.Id == id
                                    select new UserRoleViewModel
                                    {
                                        Id = c1.Id,
                                        Name = c1.Name,
                                        ParentId = c1.ParentId,
                                        IsActive = c1.IsActive,
                                        ParentRole = c2.Name,
                                        IsDelete = c1.IsDelete,
                                        CreatedOn = c1.CreatedOn,
                                        CreatedBy = c1.CreatedBy
                                    }).FirstOrDefaultAsync();

                if (result != null)
                {
                    return CreateResponse<UserRoleViewModel>(result, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<UserRoleViewModel>(null, ResponseMessage.NotFound, true , ((int)ApiStatusCode.RecordNotFound));
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<UserRoleViewModel>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException) ,ex.Message ?? ex.InnerException.ToString());

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
                    var isExist = await _db.UserRole.Where(x => x.Name == model.Name).FirstOrDefaultAsync();
                    if (isExist != null)
                    {
                        return CreateResponse<string>("", ResponseMessage.RecordAlreadyExist, false, ((int)ApiStatusCode.AlreadyExist));
                    }
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
                return CreateResponse<string>(model.Name, model.Id > 0 ? ResponseMessage.Update : ResponseMessage.Save, true , ((int)ApiStatusCode.Ok));


            }
            catch (Exception ex)
            {

                return CreateResponse<string>(null, ResponseMessage.Fail, false,((int)ApiStatusCode.ServerException) ,ex.Message ?? ex.InnerException.ToString());

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
                    return CreateResponse<object>(true, ResponseMessage.Found, true , ((int)ApiStatusCode.Ok));

                }
                else
                {
                    return CreateResponse<object>(false, ResponseMessage.NotFound, true, ((int)ApiStatusCode.ServerException));

                }

            }
            catch (Exception)
            {

                return CreateResponse<object>(false, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException));

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
                return CreateResponse<object>(true, ResponseMessage.Update, true, ((int)ApiStatusCode.Ok));

            }
            catch (Exception)
            {

                return CreateResponse<object>(false, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException));

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
                return CreateResponse<object>(true, ResponseMessage.Update, true , ((int)ApiStatusCode.Ok));

            }
            catch (Exception)
            {

                return CreateResponse<object>(false, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException));

            }
        }
    }
}
