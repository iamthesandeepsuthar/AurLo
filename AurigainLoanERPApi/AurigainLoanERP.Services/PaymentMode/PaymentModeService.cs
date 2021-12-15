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

namespace AurigainLoanERP.Services.PaymentMode
{
    public class PaymentModeService : BaseService, IPaymentModeService
    {
        public readonly IMapper _mapper;
        private readonly AurigainContext _db;
        public PaymentModeService(IMapper mapper, AurigainContext db)
        {
            this._mapper = mapper;
            _db = db;
        }
        public async Task<ApiServiceResponseModel<List<PaymentModeModel>>> GetAllAsync(IndexModel model)
        {
            ApiServiceResponseModel<List<PaymentModeModel>> objResponse = new ApiServiceResponseModel<List<PaymentModeModel>>();
            try
            {
                var result = (from role in _db.PaymentMode
                              where !role.IsDelete && (string.IsNullOrEmpty(model.Search) || role.Mode.Contains(model.Search))
                              orderby (model.OrderByAsc && model.OrderBy == "Name" ? role.Mode : "") ascending
                              orderby (!model.OrderByAsc && model.OrderBy == "Name" ? role.Mode : "") descending
                              select role);
                var data = await result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue).ToListAsync();
                objResponse.Data = _mapper.Map<List<PaymentModeModel>>(data);


                if (result != null)
                {
                    return CreateResponse<List<PaymentModeModel>>(objResponse.Data, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: result.Count());
                }
                else
                {
                    return CreateResponse<List<PaymentModeModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<List<PaymentModeModel>>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }

        }
        public async Task<ApiServiceResponseModel<List<DDLPaymentModeModel>>> GetPaymentModes()
        {
            try
            {
                var modes = await _db.PaymentMode.Select(x => new DDLPaymentModeModel
                {
                    Id = x.Id,
                    Mode = x.Mode
                }).ToListAsync();

                if (modes.Count() > 0)
                {
                    return CreateResponse<List<DDLPaymentModeModel>>(modes, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<List<DDLPaymentModeModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<List<DDLPaymentModeModel>>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }
        }
        public async Task<ApiServiceResponseModel<PaymentModeModel>> GetById(int id)
        {

            try
            {
                var result = await (from c1 in _db.PaymentMode
                                    where !c1.IsDelete && c1.IsActive.Value && c1.Id == id
                                    select c1).FirstOrDefaultAsync();

                if (result != null)
                {
                    return CreateResponse<PaymentModeModel>(_mapper.Map<PaymentModeModel>(result), ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<PaymentModeModel>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<PaymentModeModel>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }

        }
        public async Task<ApiServiceResponseModel<string>> AddUpdateAsync(PaymentModeModel model)
        {
            try
            {
                if (model.Id == 0)
                {
                    var isExist = await _db.PaymentMode.Where(x => x.Mode == model.Mode).FirstOrDefaultAsync();
                    if (isExist != null)
                    {
                        return CreateResponse<string>("", ResponseMessage.RecordAlreadyExist, false, ((int)ApiStatusCode.AlreadyExist));
                    }
                    var mode = _mapper.Map<AurigainLoanERP.Data.Database.PaymentMode>(model);
                    mode.CreatedOn = DateTime.Now;
                    var result = await _db.PaymentMode.AddAsync(mode);
                }
                else
                {
                    var mode = await _db.PaymentMode.FirstOrDefaultAsync(x => x.Id == model.Id);
                    mode.Mode = model.Mode;
                    mode.IsActive = model.IsActive;
                    mode.ModifiedOn = DateTime.Now;
                }
                await _db.SaveChangesAsync();
                return CreateResponse<string>(model.Mode, model.Id > 0 ? ResponseMessage.Update : ResponseMessage.Save, true, ((int)ApiStatusCode.Ok));
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
                AurigainLoanERP.Data.Database.PaymentMode objRole = await _db.PaymentMode.FirstOrDefaultAsync(r => r.Id == id);
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
                AurigainLoanERP.Data.Database.PaymentMode mode = await _db.PaymentMode.FirstOrDefaultAsync(r => r.Id == id);
                mode.IsActive = !mode.IsActive;
                mode.ModifiedOn = DateTime.Now;
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
