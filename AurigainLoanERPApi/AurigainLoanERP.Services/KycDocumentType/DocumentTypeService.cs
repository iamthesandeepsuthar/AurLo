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

namespace AurigainLoanERP.Services.KycDocumentType
{
    public class DocumentTypeService : BaseService, IDocumentTypeService
    {
        public readonly IMapper _mapper;
        private AurigainContext _db;
        public DocumentTypeService(IMapper mapper, AurigainContext db)
        {
            this._mapper = mapper;
            _db = db;
        }
        public async Task<ApiServiceResponseModel<List<DocumentTypeModel>>> GetAllAsync(IndexModel model)
        {
            ApiServiceResponseModel<List<DocumentTypeModel>> objResponse = new ApiServiceResponseModel<List<DocumentTypeModel>>();
            try
            {
                var result = (from role in _db.DocumentType
                              where !role.IsDelete && (string.IsNullOrEmpty(model.Search) || role.DocumentName.Contains(model.Search))
                              orderby (model.OrderByAsc && model.OrderBy == "Name" ? role.DocumentName : "") ascending
                              orderby (!model.OrderByAsc && model.OrderBy == "Name" ? role.DocumentName : "") descending
                              select role);
                var data = await result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue).ToListAsync();
                objResponse.Data = _mapper.Map<List<DocumentTypeModel>>(data);


                if (result != null)
                {
                    return CreateResponse<List<DocumentTypeModel>>(objResponse.Data, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: result.Count());
                }
                else
                {
                    return CreateResponse<List<DocumentTypeModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<List<DocumentTypeModel>>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }

        }
        public async Task<ApiServiceResponseModel<List<DDLDocumentTypeModel>>> GetDocumentType(bool? isKYC = null)
        {
            try
            {
                var types = await _db.DocumentType.Where(x => x.IsKyc == (isKYC.HasValue ? isKYC.Value : x.IsKyc)).Select(x => new DDLDocumentTypeModel
                {
                    Id = x.Id,
                    Name = x.DocumentName,
                    IsNumeric = x.IsNumeric,
                    DocumentNumberLength = x.DocumentNumberLength ?? null,
                    IsKyc = x.IsKyc,
                    RequiredFileCount = x.RequiredFileCount
                }).ToListAsync();

                if (types.Count > 0)
                {
                    return CreateResponse<List<DDLDocumentTypeModel>>(types, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<List<DDLDocumentTypeModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<List<DDLDocumentTypeModel>>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }
        }
        public async Task<ApiServiceResponseModel<DocumentTypeModel>> GetById(int id)
        {

            try
            {

                var result = await (from c1 in _db.DocumentType
                                    where !c1.IsDelete && c1.IsActive.Value && c1.Id == id
                                    select c1).FirstOrDefaultAsync();
                if (result != null)
                {
                    return CreateResponse<DocumentTypeModel>(_mapper.Map<DocumentTypeModel>(result), ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<DocumentTypeModel>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<DocumentTypeModel>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }

        }
        public async Task<ApiServiceResponseModel<string>> AddUpdateAsync(DocumentTypeModel model)
        {
            try
            {
                await _db.Database.BeginTransactionAsync();
                if (model.Id == 0)
                {
                    var isExist = await _db.DocumentType.Where(x => x.DocumentName == model.DocumentName).FirstOrDefaultAsync();
                    if (isExist != null)
                    {
                        return CreateResponse<string>("", ResponseMessage.RecordAlreadyExist, false, ((int)ApiStatusCode.AlreadyExist));
                    }
                    var type = _mapper.Map<DocumentType>(model);
                    type.CreatedOn = DateTime.Now;
                    var result = await _db.DocumentType.AddAsync(type);

                }
                else
                {
                    var type = await _db.DocumentType.FirstOrDefaultAsync(x => x.Id == model.Id);
                    type.DocumentName = model.DocumentName;
                    type.IsActive = model.IsActive;
                    type.IsNumeric = model.IsNumeric;
                    type.DocumentNumberLength = model.DocumentNumberLength;
                    type.IsKyc = model.IsKyc;
                    type.RequiredFileCount = model.RequiredFileCount;
                    type.ModifiedOn = DateTime.Now;

                }
                await _db.SaveChangesAsync();
                _db.Database.CommitTransaction();
                return CreateResponse<string>(model.DocumentName, model.Id > 0 ? ResponseMessage.Update : ResponseMessage.Save, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {
                _db.Database.RollbackTransaction();
                return CreateResponse<string>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }

        }
        public async Task<ApiServiceResponseModel<object>> UpdateDeleteStatus(int id)
        {
            try
            {
                DocumentType objRole = await _db.DocumentType.FirstOrDefaultAsync(r => r.Id == id);
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
                DocumentType type = await _db.DocumentType.FirstOrDefaultAsync(r => r.Id == id);
                type.IsActive = !type.IsActive;
                type.ModifiedOn = DateTime.Now;
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
