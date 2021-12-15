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

namespace AurigainLoanERP.Services.Qualification
{
    public class QualificationService : BaseService, IQualificationService
    {
        public readonly IMapper _mapper;
        private readonly AurigainContext _db;
        public QualificationService(IMapper mapper, AurigainContext db)
        {
            this._mapper = mapper;
            _db = db;
        }
        public async Task<ApiServiceResponseModel<List<QualificationModel>>> GetAllAsync(IndexModel model)
        {
            ApiServiceResponseModel<List<QualificationModel>> objResponse = new ApiServiceResponseModel<List<QualificationModel>>();
            try
            {
                var result = (from role in _db.QualificationMaster
                              where !role.IsDelete && (string.IsNullOrEmpty(model.Search) || role.Name.Contains(model.Search))
                              orderby (model.OrderByAsc && model.OrderBy == "Name" ? role.Name : "") ascending
                              orderby (!model.OrderByAsc && model.OrderBy == "Name" ? role.Name : "") descending
                              select role);
                var data = await result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue).ToListAsync();
                objResponse.Data = _mapper.Map<List<QualificationModel>>(data);


                if (result != null)
                {
                    return CreateResponse<List<QualificationModel>>(objResponse.Data, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: result.Count());
                }
                else
                {
                    return CreateResponse<List<QualificationModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<List<QualificationModel>>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }

        }
        public async Task<ApiServiceResponseModel<List<DDLQualificationModel>>> GetQualifications()
        {
            try
            {
                var qualification = await _db.QualificationMaster.Select(x => new DDLQualificationModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

                if (qualification.Count() > 0)
                {
                    return CreateResponse<List<DDLQualificationModel>>(qualification, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<List<DDLQualificationModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<List<DDLQualificationModel>>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }
        }
        public async Task<ApiServiceResponseModel<QualificationModel>> GetById(int id)
        {

            try
            {
                var result = await _db.QualificationMaster.Where(x => x.IsDelete == false && x.Id == id).FirstOrDefaultAsync();

                if (result != null)
                {
                    return CreateResponse<QualificationModel>(_mapper.Map<QualificationModel>(result), ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<QualificationModel>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<QualificationModel>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }

        }
        public async Task<ApiServiceResponseModel<string>> AddUpdateAsync(QualificationModel model)
        {
            try
            {
                if (model.Id == 0)
                {
                    var isExist = await _db.QualificationMaster.Where(x => x.Name == model.Name).FirstOrDefaultAsync();
                    if (isExist != null)
                    {
                        return CreateResponse<string>("", ResponseMessage.RecordAlreadyExist, false, ((int)ApiStatusCode.AlreadyExist), "", null);
                    }
                    var qualification = _mapper.Map<QualificationMaster>(model);
                    qualification.CreatedOn = DateTime.Now;
                    var result = await _db.QualificationMaster.AddAsync(qualification);
                }
                else
                {
                    var qualification = await _db.QualificationMaster.FirstOrDefaultAsync(x => x.Id == model.Id);
                    qualification.Name = model.Name;
                    qualification.IsActive = model.IsActive;
                    qualification.ModifiedOn = DateTime.Now;
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
                QualificationMaster objRole = await _db.QualificationMaster.FirstOrDefaultAsync(r => r.Id == id);
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
                QualificationMaster qualification = await _db.QualificationMaster.FirstOrDefaultAsync(r => r.Id == id);
                qualification.IsActive = !qualification.IsActive;
                qualification.ModifiedOn = DateTime.Now;
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
