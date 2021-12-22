using AurigainLoanERP.Data.Database;
using AurigainLoanERP.Shared.Common.Method;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AurigainLoanERP.Shared.Enums.FixedValueEnums;

namespace AurigainLoanERP.Services.Purpose
{
   public class PurposeService : BaseService, IPurposeService
    {       
        private readonly AurigainContext _db;
        public PurposeService(AurigainContext db)
        {           
            _db = db;
        }
        public async Task<ApiServiceResponseModel<List<PurposeModel>>> GetAllAsync(IndexModel model)
        {
            ApiServiceResponseModel<List<PurposeModel>> objResponse = new ApiServiceResponseModel<List<PurposeModel>>();
            try
            {
                var result = (from role in _db.Purpose
                              where !role.IsDelete && (string.IsNullOrEmpty(model.Search) || role.Name.Contains(model.Search))
                              orderby (model.OrderByAsc && model.OrderBy == "Name" ? role.Name : "") ascending
                              orderby (!model.OrderByAsc && model.OrderBy == "Name" ? role.Name : "") descending
                              select role);
                var data =  result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);
                objResponse.Data =await (from detail in data
                                         where detail.IsDelete == false
                                         select new PurposeModel { 
                                          Name = detail.Name}).ToListAsync();


                if (result != null)
                {
                    return CreateResponse<List<PurposeModel>>(objResponse.Data, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: result.Count());
                }
                else
                {
                    return CreateResponse<List<PurposeModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<List<PurposeModel>>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }

        }
        public async Task<ApiServiceResponseModel<List<ddlPurposeModel>>> PurposeList()
        {
            try
            {
                var purpose = await _db.Purpose.Select(x => new ddlPurposeModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

                if (purpose.Count() > 0)
                {
                    return CreateResponse<List<ddlPurposeModel>>(purpose, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<List<ddlPurposeModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<List<ddlPurposeModel>>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }
        }
        public async Task<ApiServiceResponseModel<PurposeModel>> GetById(int id)
        {

            try
            {
                var result = await _db.Purpose.Where(x => x.IsDelete == false && x.Id == id).FirstOrDefaultAsync();

                if (result != null)
                {
                    PurposeModel purpose = new PurposeModel();
                    purpose.Name = result.Name;
                    purpose.IsActive = result.IsActive.Value;
                   
                    return CreateResponse<PurposeModel>(purpose, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<PurposeModel>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<PurposeModel>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }

        }
        public async Task<ApiServiceResponseModel<string>> AddUpdateAsync(PurposeModel model)
        {
            try
            {
                if (model.Id == 0)
                {
                    var isExist = await _db.Purpose.Where(x => x.Name == model.Name).FirstOrDefaultAsync();
                    if (isExist != null)
                    {
                        return CreateResponse<string>("", ResponseMessage.RecordAlreadyExist, false, ((int)ApiStatusCode.AlreadyExist), "", null);
                    }
                    AurigainLoanERP.Data.Database.Purpose purpose = new AurigainLoanERP.Data.Database.Purpose
                    {
                        Name = model.Name,
                        IsActive = model.IsActive,
                        IsDelete = false,
                        CreatedDate = DateTime.Now
                    };                    
                    var result = await _db.Purpose.AddAsync(purpose);
                }
                else
                {
                    var purpose = await _db.Purpose.FirstOrDefaultAsync(x => x.Id == model.Id);
                    purpose.Name = model.Name;
                    purpose.IsActive = model.IsActive;                    
                    purpose.ModifiedDate = DateTime.Now;                }
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
                var objRole = await _db.Purpose.FirstOrDefaultAsync(r => r.Id == id);
                objRole.IsDelete = !objRole.IsDelete;
                objRole.ModifiedDate = DateTime.Now;
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
                var category = await _db.Purpose.FirstOrDefaultAsync(r => r.Id == id);
                category.IsActive = !category.IsActive;
                category.ModifiedDate = DateTime.Now;
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
