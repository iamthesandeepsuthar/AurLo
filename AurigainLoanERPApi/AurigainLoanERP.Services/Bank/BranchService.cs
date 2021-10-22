using AurigainLoanERP.Data.Database;
using AurigainLoanERP.Shared.Common.Method;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AurigainLoanERP.Shared.Enums.FixedValueEnums;

namespace AurigainLoanERP.Services.Bank
{
    public class BranchService : BaseService, IBranchService
    {
        public readonly IMapper _mapper;
        private AurigainContext _db;
        public BranchService(IMapper mapper, AurigainContext db)
        {
            this._mapper = mapper;
            _db = db;
        }
        public async Task<ApiServiceResponseModel<List<DDLBrnachModel>>> Branches(int bankId)
        {
            try
            {
                var branches = await _db.BankBranchMaster.Where(x=>x.BankId == bankId).Select(x => new DDLBrnachModel
                {
                    Id = x.Id,
                    BrnachName = x.BranchName,
                    Ifsc = x.Ifsc
                }).ToListAsync();

                if (branches.Count() > 0)
                {
                    return CreateResponse<List<DDLBrnachModel>>(branches, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<List<DDLBrnachModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<List<DDLBrnachModel>>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task<ApiServiceResponseModel<BranchModel>> GetById(int id)
        {
            try
            {
                var result = await _db.BankBranchMaster.Where(x => x.IsDelete == false && x.Id == id).FirstOrDefaultAsync();
                if (result != null) { 
                
                    return CreateResponse<BranchModel>(_mapper.Map<BranchModel>(result), ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<BranchModel>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<BranchModel>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task<ApiServiceResponseModel<string>> AddUpdateAsync(BranchModel model)
        {
            try
            {
                if (model.Id == 0)
                {
                    var isExist = await _db.BankBranchMaster.Where(x => x.BranchName == model.BranchName && x.BankId == model.BankId).FirstOrDefaultAsync();
                    if (isExist != null)
                    {
                        return CreateResponse<string>("", ResponseMessage.RecordAlreadyExist, false, ((int)ApiStatusCode.AlreadyExist), "", null);
                    }
                    var branch = _mapper.Map<AurigainLoanERP.Data.Database.BankBranchMaster>(model);
                    branch.CreatedDate = DateTime.Now;
                    var result = await _db.BankBranchMaster.AddAsync(branch);                  
                    await _db.SaveChangesAsync();
                    return CreateResponse<string>(model.BranchName, model.Id > 0 ? ResponseMessage.Update : ResponseMessage.Save, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    var branch = await _db.BankBranchMaster.FirstOrDefaultAsync(x => x.Id == model.Id);
                    branch.BranchName = model.BranchName;
                    branch.ContactNumber = model.ContactNumber;
                    branch.IsActive = model.IsActive;
                    branch.ModifiedDate = DateTime.Now;
                    branch.BranchEmailId = model.BranchEmailId;
                    branch.BranchCode = model.BranchCode;
                    branch.Ifsc = model.Ifsc;
                }
                await _db.SaveChangesAsync();
                return CreateResponse<string>(model.BranchName, model.Id > 0 ? ResponseMessage.Update : ResponseMessage.Save, true, ((int)ApiStatusCode.Ok));
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
                var objRole = await _db.BankBranchMaster.FirstOrDefaultAsync(r => r.Id == id);
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
                var branch = await _db.BankBranchMaster.FirstOrDefaultAsync(r => r.Id == id);
                branch.IsActive = !branch.IsActive;
                branch.ModifiedDate = DateTime.Now;
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
