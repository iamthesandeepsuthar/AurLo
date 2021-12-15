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
        private readonly AurigainContext _db;
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
                              orderby (model.OrderByAsc && model.OrderBy == "Name" ? role.Name : "") ascending
                              orderby (!model.OrderByAsc && model.OrderBy == "Name" ? role.Name : "") descending
                              select role);
                var data = await result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue).ToListAsync();
                objResponse.Data = _mapper.Map<List<StateModel>>(data);


                if (result != null)
                {
                    return CreateResponse<List<StateModel>>(objResponse.Data, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: result.Count());
                }
                else
                {
                    return CreateResponse<List<StateModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
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
                    return CreateResponse<StateModel>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
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
                var states = await _db.State.Where(x => x.IsDelete == false && x.IsActive == true).Select(x => new DDLStateModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

                if (states.Count > 0)
                {
                    return CreateResponse<List<DDLStateModel>>(states, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<List<DDLStateModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<List<DDLStateModel>>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

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
                return CreateResponse<object>(true, ResponseMessage.Update, true, ((int)ApiStatusCode.Ok));

            }
            catch (Exception)
            {

                return CreateResponse<object>(false, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException));

            }
        }
        public async Task<ApiServiceResponseModel<string>> AddUpdateStateAsync(StateModel model)
        {
            try
            {
                if (model.Id == 0)
                {
                    var isExist = await _db.State.Where(x => x.Name == model.Name).FirstOrDefaultAsync();
                    if (isExist != null)
                    {
                        return CreateResponse<string>("", ResponseMessage.RecordAlreadyExist, false, ((int)ApiStatusCode.AlreadyExist));
                    }
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
                return CreateResponse<string>(model.Name, model.Id > 0 ? ResponseMessage.Update : ResponseMessage.Save, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {

                return CreateResponse<string>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

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
                return CreateResponse<object>(true, ResponseMessage.Update, true, ((int)ApiStatusCode.Ok));

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
                              join state in _db.State on district.StateId equals state.Id
                              where !district.IsDelete && (string.IsNullOrEmpty(model.Search) || district.Name.Contains(model.Search) || state.Name.Contains(model.Search))
                              orderby (model.OrderByAsc && model.OrderBy == "Name" ? district.Name : "") ascending
                              orderby (!model.OrderByAsc && model.OrderBy == "Name" ? district.Name : "") descending
                              orderby (model.OrderByAsc && model.OrderBy == "StateName" ? district.State.Name : "") ascending
                              orderby (!model.OrderByAsc && model.OrderBy == "StateName" ? district.State.Name : "") descending
                              select district);
                var data = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);
                objResponse.Data = await (from x in data
                                          select
       new DistrictModel()
       {
           Id = x.Id,
           StateId = x.StateId,
           StateName = x.State.Name,
           Name = x.Name,
           IsActive = x.IsActive
       }).ToListAsync();

                if (result != null)
                {
                    return CreateResponse<List<DistrictModel>>(objResponse.Data, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: result.Count());
                }
                else
                {
                    return CreateResponse<List<DistrictModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<List<DistrictModel>>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }

        }
        public async Task<ApiServiceResponseModel<DistrictModel>> GetDistrictById(long id)
        {

            try
            {

                var result = await _db.District.Where(x => x.Id == id).Include(x => x.State).Include(x => x.PincodeArea).FirstOrDefaultAsync();
                var response = _mapper.Map<DistrictModel>(result);
                response.StateName = result.State.Name;
                if (result.PincodeArea.Count > 0)
                {
                    response.Areas = result.PincodeArea.Select(x => new PincodeAreaModel
                    {
                        Id = x.Id,
                        Pincode = x.Pincode,
                        AreaName = x.AreaName,
                        IsActive = x.IsActive,
                        IsDelete = x.IsDelete,
                        DistrictId = x.DistrictId,
                        CreatedDate = x.CreatedDate,
                        Modifieddate = x.Modifieddate

                    }).ToList();

                }

                if (result != null)
                {
                    return CreateResponse<DistrictModel>(response, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<DistrictModel>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
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
                var districts = await _db.District.Where(x => x.StateId == id && x.IsDelete == false && x.IsActive == true).Select(x => new DDLDistrictModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

                if (districts.Count() > 0)
                {
                    return CreateResponse<List<DDLDistrictModel>>(districts, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<List<DDLDistrictModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
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
                return CreateResponse<object>(true, ResponseMessage.Update, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception)
            {

                return CreateResponse<object>(false, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException));

            }
        }
        public async Task<ApiServiceResponseModel<string>> AddUpdateDistrictAsync(DistrictModel model)
        {

            try
            {
                await _db.Database.BeginTransactionAsync();

                long districtId = 0;
                if (model.Id == 0)
                {
                    var isExist = await _db.District.Where(x => x.Name == model.Name && x.StateId == model.StateId).FirstOrDefaultAsync();
                    if (isExist != null)
                    {
                        return CreateResponse<string>("", ResponseMessage.RecordAlreadyExist, false, ((int)ApiStatusCode.AlreadyExist));
                    }
                    District d = new District
                    {
                        Name = model.Name,
                        StateId = model.StateId,
                        CreatedOn = DateTime.Now,
                        IsActive = model.IsActive,
                        IsDelete = model.IsDelete,
                        ModifiedOn = model.ModifiedOn
                    };
                    var result = await _db.District.AddAsync(d);
                    await _db.SaveChangesAsync();
                    districtId = result.Entity.Id;
                }
                else
                {
                    var district = await _db.District.FirstOrDefaultAsync(x => x.Id == model.Id);
                    district.Name = model.Name;
                    district.StateId = model.StateId;
                    district.IsActive = model.IsActive;
                    district.ModifiedOn = DateTime.Now;
                    await _db.SaveChangesAsync();
                    districtId = model.Id;
                }

                if (model.Areas.Count > 0 && districtId > 0)
                {
                    await AddUpdateAreas(model.Areas, districtId);
                }
                _db.Database.CommitTransaction();

                return CreateResponse<string>(model.Name, model.Id > 0 ? ResponseMessage.Update : ResponseMessage.Save, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {
                _db.Database.RollbackTransaction();

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
                return CreateResponse<object>(true, ResponseMessage.Update, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception)
            {
                return CreateResponse<object>(false, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException));
            }
        }

        #endregion

        #region << Area Method>>
        public async Task<ApiServiceResponseModel<List<AvailableAreaModel>>> GetUserAvailableAreaAsync(string pinCode, int roleId, long id = 0)
        {
            try
            {
                var area = await (from record in _db.PincodeArea
                                  where record.Pincode.Contains(pinCode) && !record.UserAvailability.Any(x => (id == 0 || x.Id != id) && x.User.UserRoleId == roleId && x.IsActive == true && !x.IsDelete)
                                  select new AvailableAreaModel
                                  {
                                      Id = record.Id,
                                      PinCode = record.Pincode,
                                      AreaName = string.Concat(record.AreaName, ", ", record.District.Name, " (", record.District.State.Name, ")")
                                  }).ToListAsync();

                if (area != null)
                {
                    return CreateResponse(area, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<List<AvailableAreaModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }

            }
            catch (Exception ex)
            {
                return CreateResponse<List<AvailableAreaModel>>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task<ApiServiceResponseModel<List<AvailableAreaModel>>> GetAreaByPincode(string pinCode)
        {
            try
            {
                var area = await (from record in _db.PincodeArea
                                  where record.Pincode.Contains(pinCode)
                                  select new AvailableAreaModel
                                  {
                                      Id = record.Id,
                                      PinCode = record.Pincode,
                                      AreaName = string.Concat(record.AreaName, ", ", record.District.Name, " (", record.District.State.Name, ")"),
                                      DistrictId = record.DistrictId,
                                      StateId = record.District.StateId

                                  }).ToListAsync();

                if (area != null)
                {
                    return CreateResponse(area, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<List<AvailableAreaModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }

            }
            catch (Exception ex)
            {
                return CreateResponse<List<AvailableAreaModel>>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task<ApiServiceResponseModel<AddressDetailModel>> GetAddressDetailByPincode(string pincode)
        {
            try
            {
                var result = await _db.PincodeArea.Where(x => x.Pincode == pincode).Include(x => x.District).Include(x => x.District.State).FirstOrDefaultAsync();
                if (result != null)
                {
                    AddressDetailModel detail = new AddressDetailModel
                    {
                        AreaName = result.AreaName,
                        DistrictName = result.District.Name,
                        StateName = result.District.State.Name,
                        DistrictId = result.DistrictId,
                        StateId = result.District.StateId,
                        AreaPincodeId = result.Id
                    };
                    return CreateResponse(detail, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<AddressDetailModel>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.RecordNotFound));
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<AddressDetailModel>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        private async Task<bool> AddUpdateAreas(List<PincodeAreaModel> model, long DistrictId)
        {
            try
            {
                foreach (var item in model)
                {
                    if (item.Id == default || item.Id == 0)
                    {
                        PincodeArea pincodeAreaData = new PincodeArea
                        {
                            Pincode = item.Pincode,
                            AreaName = item.AreaName,
                            DistrictId = DistrictId,
                            CreatedDate = DateTime.Now,
                            IsActive = item.IsActive,
                            IsDelete = item.IsDelete,

                        };
                        var result = await _db.PincodeArea.AddAsync(pincodeAreaData);
                        await _db.SaveChangesAsync();

                    }
                    else
                    {
                        var areaDetail = await _db.PincodeArea.FirstOrDefaultAsync(x => x.Id == item.Id);
                        if (areaDetail != null)
                        {
                            areaDetail.Pincode = item.Pincode;
                            areaDetail.AreaName = item.AreaName;
                            //   areaDetail.DistrictId = DistrictId;
                            areaDetail.Modifieddate = DateTime.Now;
                            await _db.SaveChangesAsync();
                        }


                    }

                }
                return true;
            }
            catch (Exception)
            {

                throw;


            }
        }

        #endregion
    }
}
