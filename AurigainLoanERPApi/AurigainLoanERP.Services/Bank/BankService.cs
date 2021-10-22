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
    public class BankService : BaseService, IBankService
    {
        public readonly IMapper _mapper;
        private AurigainContext _db;
        public BankService(IMapper mapper, AurigainContext db)
        {
            this._mapper = mapper;
            _db = db;
        }
        public async Task<ApiServiceResponseModel<List<BankModel>>> GetAllAsync(IndexModel model)
        {
            ApiServiceResponseModel<List<BankModel>> objResponse = new ApiServiceResponseModel<List<BankModel>>();
            try
            {
                var result = (from bank in _db.BankMaster
                              where !bank.IsDelete && (string.IsNullOrEmpty(model.Search) || bank.Name.Contains(model.Search))
                              orderby (model.OrderByAsc && model.OrderBy == "Name" ? bank.Name : "") ascending
                              orderby (!model.OrderByAsc && model.OrderBy == "Name" ? bank.Name : "") descending
                              select bank);
                var data = await result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue).ToListAsync();
                objResponse.Data = _mapper.Map<List<BankModel>>(data);

                if (result != null)
                {
                    return CreateResponse<List<BankModel>>(objResponse.Data, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: result.Count());
                }
                else
                {
                    return CreateResponse<List<BankModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<List<BankModel>>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task<ApiServiceResponseModel<List<DDLBankModel>>> Banks()
        {
            try
            {
                var banks = await _db.BankMaster.Select(x => new DDLBankModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

                if(banks.Count() > 0)
                {
                    return CreateResponse<List<DDLBankModel>>(banks, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<List<DDLBankModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<List<DDLBankModel>>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task<ApiServiceResponseModel<BankModel>> GetById(int id)
        {
            try
            {
                var result = await _db.BankMaster.Where(x => x.IsDelete == false && x.Id == id).Include(x=>x.BankBranchMaster).FirstOrDefaultAsync();
                if (result != null)
                {              
                    return CreateResponse<BankModel>(_mapper.Map<BankModel>(result), ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else                
                {
                    return CreateResponse<BankModel>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<BankModel>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task<ApiServiceResponseModel<string>> AddUpdateAsync(BankModel model)
        {
            try
            {
                if (model.Id == 0)
                {
                    var isExist = await _db.BankMaster.Where(x => x.Name == model.Name).FirstOrDefaultAsync();
                    if (isExist != null)
                    {
                        return CreateResponse<string>("", ResponseMessage.RecordAlreadyExist, false, ((int)ApiStatusCode.AlreadyExist), "", null);
                    }
                    var bank = _mapper.Map<AurigainLoanERP.Data.Database.BankMaster>(model);
                    bank.CreatedDate = DateTime.Now;
                    var result = await _db.BankMaster.AddAsync(bank);
                    
                    foreach (var branch in model.Branches) 
                    {
                        BankBranchMaster b = new BankBranchMaster 
                        {
                            BankId = result.Entity.Id,
                            IsActive =branch.IsActive,
                            IsDelete = branch.IsDelete,
                            ContactNumber = branch.ContactNumber,
                            BranchName= branch.BranchName,
                            BranchCode= branch.BranchCode,
                            Ifsc= branch.Ifsc,
                            CreatedDate= DateTime.Now,
                            Address= branch.Address,
                            BranchEmailId= branch.BranchEmailId,
                            ConfigurationSettingJson= null
                        };
                        await _db.BankBranchMaster.AddAsync(b);                       
                    }
                    await _db.SaveChangesAsync();
                    return CreateResponse<string>(model.Name, model.Id > 0 ? ResponseMessage.Update : ResponseMessage.Save, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    var bank = await _db.BankMaster.FirstOrDefaultAsync(x => x.Id == model.Id);
                    bank.Name = model.Name;
                    bank.ContactNumber = model.ContactNumber;
                    bank.FaxNumber = model.FaxNumber;
                    bank.WebsiteUrl = model.WebsiteUrl;
                    bank.IsActive = model.IsActive;
                    bank.ModifiedDate = DateTime.Now;
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
                var objRole = await _db.BankMaster.FirstOrDefaultAsync(r => r.Id == id);
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
                var bank = await _db.BankMaster.FirstOrDefaultAsync(r => r.Id == id);
                bank.IsActive = !bank.IsActive;
                bank.ModifiedDate = DateTime.Now;
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
