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

namespace AurigainLoanERP.Services.StateAndDistrict
{
    public class StateAndDistrictSrivce : BaseService, IStateAndDistrictService
    {
        public readonly IMapper _mapper;
        private AurigainContext _db;
        public StateAndDistrictSrivce(IMapper mapper, AurigainContext db)
        {
            this._mapper = mapper;
            _db = db;
        }
        #region State Method
        public async Task<ApiServiceResponseModel<List<StateModel>>> GetAllStateAsync(IndexModel model)
        {
            ApiServiceResponseModel<List<StateModel>> objResponse = new ApiServiceResponseModel<List<StateModel>>();
            try
            {
                var result = (from role in _db.State
                              where !role.IsDelete && (string.IsNullOrEmpty(model.Search) || role.Name.Contains(model.Search))
                              orderby (model.OrderByAsc  && model.OrderBy == "Name" ? role.Name : "") ascending
                              orderby (!model.OrderByAsc  && model.OrderBy == "Name" ? role.Name : "") descending
                              select role);
                var data = await result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue).ToListAsync();
                objResponse.Data = _mapper.Map<List<StateModel>>(data);


                if (result != null)
                {
                    return CreateResponse<List<StateModel>>(objResponse.Data, ResponseMessage.Success, true,((int)ApiStatusCode.Ok), TotalRecord: result.Count());
                }
                else
                {
                    return CreateResponse<List<StateModel>>(null, ResponseMessage.NotFound, true,((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<List<StateModel>>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }

        }
        public async Task<ApiServiceResponseModel<StateModel>> GetStateById(int id)
        {

            try
            {
                var result = await (from c1 in _db.State
                                        // join st in _db.UserRoles  on c1.ParentId equals st.Id

                                    where !c1.IsDelete && c1.IsActive.Value && c1.Id == id
                                    select c1).FirstOrDefaultAsync();

                if (result != null)
                {
                    return CreateResponse<StateModel>(_mapper.Map<StateModel>(result), ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<StateModel>(null, ResponseMessage.NotFound, true,((int)ApiStatusCode.RecordNotFound));
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<StateModel>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }

        }
        public async Task<ApiServiceResponseModel<List<DDLStateModel>>> GetStates()
        {
            try
            {
                var states = await _db.State.Select(x => new DDLStateModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

                if (states.Count > 0)
                {
                    return CreateResponse<List<DDLStateModel>>(states, ResponseMessage.Success, true,((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<List<DDLStateModel>>(null, ResponseMessage.NotFound, true,((int)ApiStatusCode.RecordNotFound));
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<List<DDLStateModel>>(null, ResponseMessage.NotFound, false,((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }
        }
        public async Task<ApiServiceResponseModel<object>> UpdateStateDeleteStatus(int id)
        {
            try
            {
                State objRole = await _db.State.FirstOrDefaultAsync(r => r.Id == id);
                objRole.IsDelete = !objRole.IsDelete;
                objRole.ModifiedOn = DateTime.Now;
                await _db.SaveChangesAsync();
                return CreateResponse<object>(true, ResponseMessage.Update, true,((int)ApiStatusCode.Ok));

            }
            catch (Exception)
            {

                return CreateResponse<object>(false, ResponseMessage.Fail, false,((int)ApiStatusCode.ServerException));

            }
        }
        public async Task<ApiServiceResponseModel<string>> AddUpdateStateAsync(StateModel model)
        {

            try
            {
                if (model.Id == 0)
                {
                    var state = _mapper.Map<State>(model);
                    state.CreatedOn = DateTime.Now;
                    var result = await _db.State.AddAsync(state);
                }
                else
                {
                    var state = await _db.State.FirstOrDefaultAsync(x => x.Id == model.Id);
                    state.Name = model.Name;
                    state.IsActive = model.IsActive;
                    state.ModifiedOn = DateTime.Now;

                }
                await _db.SaveChangesAsync();
                return CreateResponse<string>(model.Name, model.Id > 0 ? ResponseMessage.Update : ResponseMessage.Save, true , ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {

                return CreateResponse<string>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException) ,ex.Message ?? ex.InnerException.ToString());

            }

        }
        public async Task<ApiServiceResponseModel<object>> UpateStateActiveStatus(int id)
        {
            try
            {         
                State state = await _db.State.FirstOrDefaultAsync(r => r.Id == id);
                state.IsActive = !state.IsActive;
                state.ModifiedOn = DateTime.Now;
                await _db.SaveChangesAsync();
                return CreateResponse<object>(true, ResponseMessage.Update, true,((int)ApiStatusCode.Ok));

            }
            catch (Exception)
            {

                return CreateResponse<object>(false, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException));

            }
        }
        #endregion

        #region District Method
        public async Task<ApiServiceResponseModel<List<DistrictModel>>> GetAllDistrictAsync(IndexModel model)
        {
            ApiServiceResponseModel<List<DistrictModel>> objResponse = new ApiServiceResponseModel<List<DistrictModel>>();
            try
            {
                var result = (from district in _db.District
                              where !district.IsDelete && (string.IsNullOrEmpty(model.Search) || district.Name.Contains(model.Search))
                              orderby (model.OrderByAsc  && model.OrderBy == "Name" ? district.Name : "") ascending
                              orderby (!model.OrderByAsc && model.OrderBy == "Name" ? district.Name : "") descending
                              select district);
                var data = await result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue).ToListAsync();
                objResponse.Data = _mapper.Map<List<DistrictModel>>(data);


                if (result != null)
                {
                    return CreateResponse<List<DistrictModel>>(objResponse.Data, ResponseMessage.Success, true,((int)ApiStatusCode.Ok) ,TotalRecord: result.Count());
                }
                else
                {
                    return CreateResponse<List<DistrictModel>>(null, ResponseMessage.NotFound,true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<List<DistrictModel>>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException) ,ex.Message ?? ex.InnerException.ToString());

            }

        }
        public async Task<ApiServiceResponseModel<DistrictModel>> GetDistrictById(long id)
        {

            try
            {
                
                var result = await _db.District.Where(x => x.Id == id).Include(x=>x.State).FirstOrDefaultAsync();
                var response = _mapper.Map<DistrictModel>(result);
                response.StateName = result.State.Name;
                if (result != null)
                {
                    return CreateResponse<DistrictModel>(response, ResponseMessage.Success, true,((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<DistrictModel>(null, ResponseMessage.NotFound, true,((int)ApiStatusCode.RecordNotFound));
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<DistrictModel>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }

        }
        public async Task<ApiServiceResponseModel<List<DDLDistrictModel>>> GetDistricts(int id) //stateId
        {
            try
            {
                var districts = await _db.District.Where(x => x.StateId == id).Select(x => new DDLDistrictModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Pincode = x.Pincode
                }).ToListAsync();

                if (districts.Count() > 0)
                {
                    return CreateResponse<List<DDLDistrictModel>>(districts, ResponseMessage.Success, true,((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<List<DDLDistrictModel>>(null, ResponseMessage.NotFound, true,((int)ApiStatusCode.RecordNotFound));
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<List<DDLDistrictModel>>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }
        }
        public async Task<ApiServiceResponseModel<object>> UpdateDistrictDeleteStatus(long id)
        {
            try
            {
                District objRole = await _db.District.FirstOrDefaultAsync(r => r.Id == id);
                objRole.IsDelete = !objRole.IsDelete;
                objRole.ModifiedOn = DateTime.Now;
                await _db.SaveChangesAsync();
                return CreateResponse<object>(true, ResponseMessage.Update, true,((int)ApiStatusCode.Ok));
            }
            catch (Exception)
            {

                return CreateResponse<object>(false, ResponseMessage.Fail, false,((int)ApiStatusCode.ServerException));

            }
        }
        public async Task<ApiServiceResponseModel<string>> AddUpdateDistrictAsync(DistrictModel model)
        {

            try
            {
                
                if (model.Id == 0)
                {
                    District d = new District 
                    {
                        Name = model.Name,
                        StateId = model.StateId,
                        CreatedOn = DateTime.Now,
                        IsActive = model.IsActive,
                        IsDelete = model.IsDelete,
                        Pincode = model.Pincode,
                        ModifiedOn = model.ModifiedOn
                    };                    
                    var result = await _db.District.AddAsync(d);
                }
                else
                {
                    var district = await _db.District.FirstOrDefaultAsync(x => x.Id == model.Id);
                    district.Name = model.Name;
                    district.StateId = model.StateId;
                    district.IsActive = model.IsActive;
                    district.ModifiedOn = DateTime.Now;
                }
                await _db.SaveChangesAsync();
                return CreateResponse<string>(model.Name, model.Id > 0 ? ResponseMessage.Update : ResponseMessage.Save, true,((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {

                return CreateResponse<string>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }

        }
        public async Task<ApiServiceResponseModel<object>> UpateDistrictActiveStatus(long id)
        {
            try
            {
                District district = await _db.District.FirstOrDefaultAsync(r => r.Id == id);
                district.IsActive = !district.IsActive;
                district.ModifiedOn = DateTime.Now;
                await _db.SaveChangesAsync();
                return CreateResponse<object>(true, ResponseMessage.Update, true,((int)ApiStatusCode.Ok));
            }
            catch (Exception)
            {
                return CreateResponse<object>(false, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException));
            }
        }

        #endregion

    }
}
