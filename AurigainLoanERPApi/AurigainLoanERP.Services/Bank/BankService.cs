using AurigainLoanERP.Data.Database;
using AurigainLoanERP.Shared.Common;
using AurigainLoanERP.Shared.Common.Method;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AurigainLoanERP.Shared.Enums.FixedValueEnums;

namespace AurigainLoanERP.Services.Bank
{
    public class BankService : BaseService, IBankService
    {
        public readonly IMapper _mapper;
        private readonly FileHelper _fileHelper;

        private AurigainContext _db;
        public BankService(IMapper mapper, AurigainContext db, IHostingEnvironment environment)
        {
            this._mapper = mapper;
            _db = db;
            _fileHelper = new FileHelper(environment);

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
                    var bank = _mapper.Map<BankModel>(result);
                    foreach (var branch in result.BankBranchMaster) 
                    {
                        BranchModel b = new BranchModel 
                        {
                            Id= branch.Id,
                            BranchName = branch.BranchName,
                            BranchCode = branch.BranchCode,
                            Ifsc = branch.Ifsc,
                            BankId = branch.BankId,
                            Pincode = branch.Pincode,
                            BranchEmailId = branch.BranchEmailId,
                            ContactNumber = branch.ContactNumber,
                            IsActive = branch.IsActive,
                            IsDelete = branch.IsDelete,
                            Address = branch.Address
                        };
                        bank.Branches.Add(b);
                    }
                    return CreateResponse<BankModel>(bank, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
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
                    await _db.SaveChangesAsync();
                    model.Id = result.Entity.Id;
                    foreach (var branch in model.Branches) 
                    {
                        var isExistBranch =await  _db.BankBranchMaster.Where(x => x.BranchCode == branch.BranchCode && x.Ifsc == branch.Ifsc).FirstOrDefaultAsync();
                        if(isExistBranch==null) 
                        {
                            BankBranchMaster b = new BankBranchMaster
                            {
                                BankId = model.Id,
                                IsActive = branch.IsActive,
                                IsDelete = branch.IsDelete,
                                ContactNumber = branch.ContactNumber,
                                BranchName = branch.BranchName,
                                BranchCode = branch.BranchCode,
                                Ifsc = branch.Ifsc,
                                CreatedDate = DateTime.Now,
                                Address = branch.Address,
                                BranchEmailId = branch.BranchEmailId,
                                ConfigurationSettingJson = null
                            };
                           await _db.BankBranchMaster.AddAsync(b);                           
                        }      
                        
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
                    foreach (var branch in model.Branches) 
                    {
                        if (branch.Id == 0)
                        {
                            BankBranchMaster b = new BankBranchMaster {
                                ContactNumber = branch.ContactNumber,
                                Address= branch.Address,
                                Ifsc= branch.Ifsc,
                                IsActive= branch.IsActive,
                                BankId= model.Id,
                                BranchCode = branch.BranchCode,
                                Pincode = branch.Pincode,
                                BranchEmailId= branch.BranchEmailId,
                                BranchName= branch.BranchName,
                                CreatedDate = DateTime.Now,
                                ConfigurationSettingJson = null,
                                IsDelete = false
                            };
                         await  _db.BankBranchMaster.AddAsync(b);                        
                        }
                        else 
                        {
                            var existBranch =await _db.BankBranchMaster.Where(x => x.Id == branch.Id).FirstOrDefaultAsync();
                            if (existBranch != null) 
                            {
                                existBranch.BranchName = branch.BranchName;
                                existBranch.BranchCode = branch.BranchCode;
                                existBranch.BranchEmailId = branch.BranchEmailId;
                                existBranch.IsActive = branch.IsActive;
                                existBranch.ModifiedDate = DateTime.Now;
                                existBranch.Ifsc = branch.Ifsc;
                                existBranch.Pincode = branch.Pincode;
                                existBranch.Address = branch.Address;
                                existBranch.ContactNumber = branch.ContactNumber;                                
                            }                           
                        }
                        await _db.SaveChangesAsync();
                    }                   
                }                
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
        public async Task<ApiServiceResponseModel<List<AvailableBranchModel>>> BranchByPincode(string pincode)
        {
            try
            {
                var branches = await  (from  branch in _db.BankBranchMaster join bank in _db.BankMaster on  branch.BankId equals bank.Id
                                   where pincode.Contains(pincode) 
                                   select new AvailableBranchModel                            
                                   {
                                      Id = branch.Id,
                                      BranchName = branch.BranchName,
                                      BankName = bank.Name,
                                      Ifsc = branch.Ifsc,
                                      BankId  = branch.BankId 
                                   }).ToListAsync();

                if (branches.Count >0)
                {
                    return CreateResponse(branches, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<List<AvailableBranchModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }

            }
            catch (Exception ex)
            {
                return CreateResponse<List<AvailableBranchModel>>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        /// <summary>
        /// Update bank Logo
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiServiceResponseModel<string>> UpdateLogo(BankLogoPostModel model)
        {
            try
            {
                if (model != null && !string.IsNullOrEmpty(model.Base64) && model.Id > 0)
                {


                    await _db.Database.BeginTransactionAsync();
                    var user = await _db.BankMaster.FirstOrDefaultAsync(X => X.Id == model.Id);
                    string savedFilePath = !string.IsNullOrEmpty(model.Base64) ? Path.Combine(FilePathConstant.BankLogoFile, _fileHelper.Save(model.Base64, FilePathConstant.BankLogoFile, model.FileName)) : null;
                    user.BankLogoUrl = savedFilePath;
                    await _db.SaveChangesAsync();

                    _db.Database.CommitTransaction();
                    return CreateResponse<string>(savedFilePath, ResponseMessage.FileUpdated, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<string>(null, ResponseMessage.InvalidData, true, ((int)ApiStatusCode.InvaildModel));
                }
            }
            catch (Exception ex)
            {
                _db.Database.RollbackTransaction();
                return CreateResponse<string>(null, ResponseMessage.Fail, true, ((int)ApiStatusCode.DataBaseTransactionFailed), ex.Message ?? ex.InnerException.ToString());

            }
        }
    }
}
