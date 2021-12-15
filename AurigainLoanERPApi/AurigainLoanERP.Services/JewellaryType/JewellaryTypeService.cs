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

namespace AurigainLoanERP.Services.JewellaryType
{
    public class JewellaryTypeService : BaseService, IJewellaryTypeService
    {
        public readonly IMapper _mapper;
        private readonly AurigainContext _db;
        public JewellaryTypeService(IMapper mapper, AurigainContext db)
        {
            this._mapper = mapper;
            _db = db;
        }
        public async Task<ApiServiceResponseModel<List<JewellaryTypeModel>>> GetAllAsync(IndexModel model)
        {
            ApiServiceResponseModel<List<JewellaryTypeModel>> objResponse = new ApiServiceResponseModel<List<JewellaryTypeModel>>();
            try
            {
                var result = (from role in _db.JewellaryType
                              where !role.IsDelete.Value && (string.IsNullOrEmpty(model.Search) || role.Name.Contains(model.Search))
                              orderby (model.OrderByAsc && model.OrderBy == "Name" ? role.Name : "") ascending
                              orderby (!model.OrderByAsc && model.OrderBy == "Name" ? role.Name : "") descending
                              select role);
                var data = await result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue).ToListAsync();
                objResponse.Data = _mapper.Map<List<JewellaryTypeModel>>(data);


                if (result != null)
                {
                    return CreateResponse<List<JewellaryTypeModel>>(objResponse.Data, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: result.Count());
                }
                else
                {
                    return CreateResponse<List<JewellaryTypeModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<List<JewellaryTypeModel>>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }

        }
        public async Task<ApiServiceResponseModel<List<DDLJewellaryType>>> JewellaryTypes()
        {
            try
            {
                var jewellaryType = await _db.JewellaryType.Select(x => new DDLJewellaryType
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

                if (jewellaryType.Count() > 0)
                {
                    return CreateResponse<List<DDLJewellaryType>>(jewellaryType, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<List<DDLJewellaryType>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<List<DDLJewellaryType>>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }
        }
        public async Task<ApiServiceResponseModel<JewellaryTypeModel>> GetById(int id)
        {

            try
            {
                var result = await _db.JewellaryType.Where(x => x.IsDelete == false && x.Id == id).FirstOrDefaultAsync();

                if (result != null)
                {
                    return CreateResponse<JewellaryTypeModel>(_mapper.Map<JewellaryTypeModel>(result), ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<JewellaryTypeModel>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<JewellaryTypeModel>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }

        }
        public async Task<ApiServiceResponseModel<string>> AddUpdateAsync(JewellaryTypeModel model)
        {
            try
            {
                if (model.Id == 0)
                {
                    var isExist = await _db.JewellaryType.Where(x => x.Name == model.Name).FirstOrDefaultAsync();
                    if (isExist != null)
                    {
                        return CreateResponse<string>("", ResponseMessage.RecordAlreadyExist, false, ((int)ApiStatusCode.AlreadyExist), "", null);
                    }
                    var jewellaryType = _mapper.Map<AurigainLoanERP.Data.Database.JewellaryType>(model);
                    jewellaryType.CreatedOn = DateTime.Now;
                    var result = await _db.JewellaryType.AddAsync(jewellaryType);
                }
                else
                {
                    var jewellaryType = await _db.JewellaryType.FirstOrDefaultAsync(x => x.Id == model.Id);
                    jewellaryType.Name = model.Name;
                    jewellaryType.IsActive = model.IsActive;
                    jewellaryType.Description = model.Description;
                    jewellaryType.ModifiedOn = DateTime.Now;
                }
                await _db.SaveChangesAsync();
                return CreateResponse<string>(model.Name, model.Id > 0 ? ResponseMessage.Update : ResponseMessage.Save, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {

                return CreateResponse<string>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }

        }
        public async Task<ApiServiceResponseModel<object>> UpdateDeleteStatus(int id)
        {
            try
            {
                var objRole = await _db.JewellaryType.FirstOrDefaultAsync(r => r.Id == id);
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
        public async Task<ApiServiceResponseModel<object>> UpateActiveStatus(int id)
        {
            try
            {
                var category = await _db.JewellaryType.FirstOrDefaultAsync(r => r.Id == id);
                category.IsActive = !category.IsActive;
                category.ModifiedOn = DateTime.Now;
                await _db.SaveChangesAsync();
                return CreateResponse<object>(true, ResponseMessage.Update, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception)
            {
                return CreateResponse<object>(false, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException));
            }


        }
    }
}
