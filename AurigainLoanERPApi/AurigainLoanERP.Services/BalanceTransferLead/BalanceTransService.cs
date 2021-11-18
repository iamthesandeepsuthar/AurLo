using AurigainLoanERP.Data.Database;
using AurigainLoanERP.Shared.Common;
using AurigainLoanERP.Shared.Common.Method;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using AurigainLoanERP.Shared.ExtensionMethod;
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

namespace AurigainLoanERP.Services.BalanceTransferLead
{
    public class BalanceTransService : BaseService, IBalanceTransService
    {
        public readonly IMapper _mapper;
        private readonly FileHelper _fileHelper;

        private AurigainContext _db;
        public BalanceTransService(IMapper mapper, AurigainContext db, IHostingEnvironment environment)
        {
            this._mapper = mapper;
            _db = db;
            _fileHelper = new FileHelper(environment);

        }
        public async Task<ApiServiceResponseModel<string>> AddUpdateAsync(BTGoldLoanLeadPostModel model)
        {
            try
            {
                
                await _db.Database.BeginTransactionAsync();
                if (model.Id == 0 || model.Id == default)
                {
                    var customerUserId = await SaveCustomerBTFreshLead(model);
                    model.CustomerUserId = customerUserId;
                    var objData = new BtgoldLoanLead();
                    objData.FullName = !string.IsNullOrEmpty(model.FullName) ? model.FullName : null;
                    objData.FatherName = !string.IsNullOrEmpty(model.FatherName) ? model.FatherName : null;
                    objData.Gender = !string.IsNullOrEmpty(model.Gender) ? model.Gender : null;
                    objData.DateOfBirth = model.DateOfBirth != null ? model.DateOfBirth : DateTime.MinValue;
                    objData.Profession = !string.IsNullOrEmpty(model.Profession) ? model.Profession : null;
                    objData.Mobile = !string.IsNullOrEmpty(model.Mobile) ? model.Mobile : null;
                    objData.EmailId = !string.IsNullOrEmpty(model.EmailId) ? model.EmailId : null;
                    objData.SecondaryMobile = !string.IsNullOrEmpty(model.SecondaryMobile) ? model.SecondaryMobile : null;
                    objData.Purpose = !string.IsNullOrEmpty(model.Purpose) ? model.Purpose : null;
                    objData.LeadSourceByuserId = model.LeadSourceByuserId;
                    objData.CreatedOn = DateTime.Now;
                    objData.CustomerUserId = model.CustomerUserId;
                    objData.LoanAmount = model.LoanAmount;
                    objData.ProductId = model.ProductId;
                    objData.CreatedBy = null;
                    objData.IsActive = true;

                    var result = await _db.BtgoldLoanLead.AddAsync(objData);
                    await _db.SaveChangesAsync();
                    model.Id = result.Entity.Id;
                }
                else
                {
                    var objData = await _db.BtgoldLoanLead.FirstOrDefaultAsync(x => x.Id == model.Id && x.IsActive.Value && !x.IsDelete);
                    objData.FullName = !string.IsNullOrEmpty(model.FullName) ? model.FullName : null;
                    objData.FatherName = !string.IsNullOrEmpty(model.FatherName) ? model.FatherName : null;
                    objData.Gender = !string.IsNullOrEmpty(model.Gender) ? model.Gender : null;
                    objData.DateOfBirth = model.DateOfBirth != null ? model.DateOfBirth : DateTime.MinValue;
                    objData.Profession = !string.IsNullOrEmpty(model.Profession) ? model.Profession : null;
                    objData.Mobile = !string.IsNullOrEmpty(model.Mobile) ? model.Mobile : null;
                    objData.EmailId = !string.IsNullOrEmpty(model.EmailId) ? model.EmailId : null;
                    objData.SecondaryMobile = !string.IsNullOrEmpty(model.SecondaryMobile) ? model.SecondaryMobile : null;
                    objData.Purpose = !string.IsNullOrEmpty(model.Purpose) ? model.Purpose : null;
                    objData.LeadSourceByuserId = model.LeadSourceByuserId;
                    objData.ModifiedOn = DateTime.Now;
                    objData.CreatedBy = null;
                    await _db.SaveChangesAsync();
                }

                if (model.AddressDetail != null)
                {
                    await SetAddressDetailAsync(model.AddressDetail, model.Id);
                }
                if (model.AppointmentDetail != null)
                {
                    await SetAppointmentDetailAsync(model.AppointmentDetail, model.Id);
                }
                if (model.DocumentDetail != null)
                {
                    await SetDocumentDetail(model.DocumentDetail, model.Id);

                }
                if (model.KYCDetail != null)
                {
                    await SetKYCDetail(model.KYCDetail, model.Id);

                }
                if (model.ExistingLoanDetail != null)
                {
                    await SetExistingLoanDetail(model.ExistingLoanDetail, model.Id);

                }
                if (model.JewelleryDetail != null)
                {
                    await SetJewelleryDetail(model.JewelleryDetail, model.Id);

                }
                _db.Database.CommitTransaction();
                return CreateResponse<string>(model.Id.ToString(), model.Id > 0 ? ResponseMessage.Update : ResponseMessage.Save, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {
                _db.Database.RollbackTransaction();

                return CreateResponse<string>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());

            }

        }
        public async Task<ApiServiceResponseModel<List<BTGoldLoanLeadListModel>>> BTGolddLoanLeadList(IndexModel model)
        {
            ApiServiceResponseModel<List<BTGoldLoanLeadListModel>> objResponse = new ApiServiceResponseModel<List<BTGoldLoanLeadListModel>>();
            try
            {
                IQueryable<BtgoldLoanLead> result;
                if (model.UserId == null)
                {
                    result = (from goldLoanLead in _db.BtgoldLoanLead
                              where !goldLoanLead.IsDelete && (string.IsNullOrEmpty(model.Search) || goldLoanLead.FullName.Contains(model.Search) || goldLoanLead.FatherName.Contains(model.Search) || goldLoanLead.Gender.Contains(model.Search) || goldLoanLead.BtgoldLoanLeadAddressDetail.FirstOrDefault().AeraPincode.Pincode.Contains(model.Search)) || goldLoanLead.Product.Name.Contains(model.Search)
                              select goldLoanLead);
                }
                else
                {
                    result = (from goldLoanLead in _db.BtgoldLoanLead
                              where !goldLoanLead.IsDelete && goldLoanLead.CustomerUserId == model.UserId && (string.IsNullOrEmpty(model.Search) || goldLoanLead.FullName.Contains(model.Search) || goldLoanLead.FatherName.Contains(model.Search) || goldLoanLead.Gender.Contains(model.Search) || goldLoanLead.BtgoldLoanLeadAddressDetail.FirstOrDefault().AeraPincode.Pincode.Contains(model.Search)) ||
                              goldLoanLead.Product.Name.Contains(model.Search)
                              select goldLoanLead);
                }

                switch (model.OrderBy)
                {
                    case "FullName":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.FullName ascending select orderData) : (from orderData in result orderby orderData.FullName descending select orderData);
                        break;
                    case "FatherName":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.FatherName ascending select orderData) : (from orderData in result orderby orderData.FatherName descending select orderData);
                        break;
                    case "LoanAmountRequired":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.FatherName ascending select orderData) : (from orderData in result orderby orderData.FatherName descending select orderData);
                        break;
                    case "Pincode":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.Gender ascending select orderData) : (from orderData in result orderby orderData.Gender descending select orderData);
                        break;
                    case "ProductName":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.Gender ascending select orderData) : (from orderData in result orderby orderData.Gender descending select orderData);
                        break;
                    default:
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.Mobile ascending select orderData) : (from orderData in result orderby orderData.Mobile descending select orderData);
                        break;
                }

                var data = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);

                objResponse.Data = await (from detail in data
                                          where detail.IsDelete == false
                                          select new BTGoldLoanLeadListModel
                                          {
                                              Id = detail.Id,
                                              FullName = detail.FullName ?? null,
                                              LeadSourceByUserName = detail.LeadSourceByuser.UserName,
                                              LoanAmountRequired = ((double)detail.LoanAmount),
                                              PrimaryMobileNumber = detail.Mobile,
                                              FatherName = detail.FatherName,
                                              Gender = detail.Gender ?? null,
                                              Email = detail.EmailId,
                                              IsActive = detail.IsActive.Value,
                                              ProductName = detail.Product.Name,
                                              Pincode = detail.BtgoldLoanLeadAddressDetail.FirstOrDefault().AeraPincode.Pincode,
                                          }).ToListAsync();
                if (result != null)
                {
                    return CreateResponse(objResponse.Data, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: result.Count());
                }
                else
                {
                    return CreateResponse<List<BTGoldLoanLeadListModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
                }

            }
            catch (Exception ex)
            {
                return CreateResponse<List<BTGoldLoanLeadListModel>>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }

        #region <<Private Method Of Balance Transafer Gold Loan Lead>>
        private async Task<long> SaveCustomerBTFreshLead(BTGoldLoanLeadPostModel model)
        {
            try
            {
                var isExist = await _db.UserMaster.Where(x => x.Email == model.EmailId || x.Mobile == model.Mobile && x.IsDelete == false).FirstOrDefaultAsync();
                if (isExist != null)
                {
                    return isExist.Id;
                }
                Random random = new Random();
                UserMaster user = new UserMaster
                {
                    Email = model.EmailId,
                    Mobile = model.Mobile,
                    IsActive = true,
                    IsApproved = false,
                    IsWhatsApp = true,
                    CreatedOn = DateTime.Now,
                    Password = "12345",
                    UserName = model.FullName,
                    Mpin = random.Next(100000, 199999).ToString(),
                    UserRoleId = ((int)UserRoleEnum.Customer),
                    IsDelete = false,
                    ProfilePath = null
                };
                var customerUser = await _db.UserMaster.AddAsync(user);
                await _db.SaveChangesAsync();

                UserCustomer customer = new UserCustomer
                {
                    FullName = model.FullName,
                    FatherName = model.FatherName,
                    DateOfBirth = model.DateOfBirth,
                    UserId = customerUser.Entity.Id,
                    Gender = model.Gender,
                    IsActive = true,
                    IsDelete = false,
                    PincodeAreaId = model.AddressDetail.AeraPincodeId,
                    Address = model.AddressDetail.Address,
                    CreatedBy = (int)UserRoleEnum.Admin
                };
                var result = await _db.UserCustomer.AddAsync(customer);
                await _db.SaveChangesAsync();
                return customerUser.Entity.Id;
            }
            catch
            {
                throw;
            }
        }
        private async Task<bool> SetAddressDetailAsync(BtGoldLoanLeadAddressPostModel model, long LeadId)
        {
            try
            {
                if (model.Id == default || model.Id == 0)
                {
                    var objModel = new BtgoldLoanLeadAddressDetail()
                    {

                        LeadId = LeadId,
                        Address = string.IsNullOrEmpty(model.Address) ? null : model.Address,
                        AeraPincodeId = model.AeraPincodeId.HasValue ? model.AeraPincodeId : null,
                        CorrespondAddress = string.IsNullOrEmpty(model.CorrespondAddress) ? null : model.CorrespondAddress,
                        CorrespondAeraPincodeId = model.CorrespondAeraPincodeId.HasValue ? model.CorrespondAeraPincodeId : null,

                    };

                    var result = await _db.BtgoldLoanLeadAddressDetail.AddAsync(objModel);
                    await _db.SaveChangesAsync();

                }
                else
                {
                    var objModel = await _db.BtgoldLoanLeadAddressDetail.FirstOrDefaultAsync(x => x.Id == model.Id);
                    if (objModel != null)
                    {
                        objModel.Address = string.IsNullOrEmpty(model.Address) ? null : model.Address;
                        objModel.AeraPincodeId = model.AeraPincodeId.HasValue ? model.AeraPincodeId : null;
                        objModel.CorrespondAddress = string.IsNullOrEmpty(model.CorrespondAddress) ? null : model.CorrespondAddress;
                        objModel.CorrespondAeraPincodeId = model.CorrespondAeraPincodeId.HasValue ? model.CorrespondAeraPincodeId : null;
                        await _db.SaveChangesAsync();
                    }
                }

                return true;

            }
            catch
            {
                throw;

            }
        }
        private async Task<bool> SetAppointmentDetailAsync(BtGoldLoanLeadAppointmentPostModel model, long LeadId)
        {
            try
            {
                if (model.Id == default || model.Id == 0)
                {
                    var objModel = new BtgoldLoanLeadAppointmentDetail()
                    {

                        LeadId = LeadId,
                        BranchId = model.BranchId.HasValue ? model.BranchId : null,
                        AppointmentDate = model.AppointmentDate.HasValue ? model.AppointmentDate : null,
                        AppointmentTime = model.AppointmentTime.ToTimeSpanValue()

                    };

                    var result = await _db.BtgoldLoanLeadAppointmentDetail.AddAsync(objModel);
                    await _db.SaveChangesAsync();

                }
                else
                {
                    var objModel = await _db.BtgoldLoanLeadAppointmentDetail.FirstOrDefaultAsync(x => x.Id == model.Id);
                    if (objModel != null)
                    {
                        objModel.LeadId = LeadId;
                        objModel.BranchId = model.BranchId.HasValue ? model.BranchId : null;
                        objModel.AppointmentDate = model.AppointmentDate.HasValue ? model.AppointmentDate : null;
                        objModel.AppointmentTime = model.AppointmentTime.ToTimeSpanValue();
                        await _db.SaveChangesAsync();
                    }
                }

                return true;

            }
            catch
            {
                throw;

            }
        }
        private async Task<bool> SetDocumentDetail(BtGoldLoanLeadDocumentPostModel model, long LeadId)
        {
            try
            {
                string fileSavePath = string.Concat(FilePathConstant.BTGoldLeadsDocsFile, "", LeadId, "\\");

                if (model.Id == default || model.Id == 0)
                {
                    var objModel = new BtgoldLoanLeadDocumentDetail()
                    {

                        LeadId = LeadId,
                        CustomerPhoto = model.CustomerPhoto != null ? Path.Combine(fileSavePath, _fileHelper.Save(model.CustomerPhoto.File, fileSavePath, model.CustomerPhoto.FileName) ): null,
                        KycdocumentPoi = model.KycDocumentPoi != null ? Path.Combine(fileSavePath, _fileHelper.Save(model.KycDocumentPoi.File, fileSavePath, model.KycDocumentPoi.FileName) ): null,
                        KycdocumentPoa = model.KycDocumentPoa != null ? Path.Combine(fileSavePath, _fileHelper.Save(model.KycDocumentPoa.File, fileSavePath, model.KycDocumentPoa.FileName) ): null,
                        BlankCheque1 = model.BlankCheque1 != null ? Path.Combine(fileSavePath, _fileHelper.Save(model.BlankCheque1.File, fileSavePath, model.BlankCheque1.FileName) ): null,
                        BlankCheque2 = model.BlankCheque2 != null ? Path.Combine(fileSavePath, _fileHelper.Save(model.BlankCheque2.File, fileSavePath, model.BlankCheque2.FileName) ): null,
                        LoanDocument = model.LoanDocument != null ? Path.Combine(fileSavePath, _fileHelper.Save(model.LoanDocument.File, fileSavePath, model.LoanDocument.FileName) ): null,
                        AggrementLastPage = model.AggrementLastPage != null ? Path.Combine(fileSavePath, _fileHelper.Save(model.AggrementLastPage.File, fileSavePath, model.AggrementLastPage.FileName) ): null,
                        PromissoryNote = model.PromissoryNote != null ? Path.Combine(fileSavePath, _fileHelper.Save(model.PromissoryNote.File, fileSavePath, model.PromissoryNote.FileName) ): null,
                        AtmwithdrawalSlip = model.AtmwithdrawalSlip != null ? Path.Combine(fileSavePath, _fileHelper.Save(model.AtmwithdrawalSlip.File, fileSavePath, model.AtmwithdrawalSlip.FileName) ): null,
                        ForeClosureLetter = model.ForeClosureLetter != null ? Path.Combine(fileSavePath, _fileHelper.Save(model.ForeClosureLetter.File, fileSavePath, model.ForeClosureLetter.FileName) ): null,
                    };

                    var result = await _db.BtgoldLoanLeadDocumentDetail.AddAsync(objModel);
                    await _db.SaveChangesAsync();

                }
                else
                {
                    var objModel = await _db.BtgoldLoanLeadDocumentDetail.FirstOrDefaultAsync(x => x.Id == model.Id && x.LeadId == LeadId);
                    if (objModel != null)
                    {
                        if (model.CustomerPhoto != null && model.CustomerPhoto.IsEditMode && string.IsNullOrEmpty(model.CustomerPhoto.File))
                        {
                            _fileHelper.Delete(Path.Combine(fileSavePath, objModel.CustomerPhoto));

                        }
                        else
                        {
                            if (model.CustomerPhoto.File.IsBase64() && !string.IsNullOrEmpty(objModel.CustomerPhoto))

                            {
                                _fileHelper.Delete(Path.Combine(fileSavePath, objModel.CustomerPhoto));
                                objModel.CustomerPhoto = model.CustomerPhoto != null ? _fileHelper.Save(model.CustomerPhoto.File, fileSavePath, model.CustomerPhoto.FileName) : null;

                            }

                        }

                        if (model.KycDocumentPoi != null && model.KycDocumentPoi.IsEditMode && string.IsNullOrEmpty(model.KycDocumentPoi.File))
                        {
                            _fileHelper.Delete(Path.Combine(fileSavePath, objModel.KycdocumentPoi));

                        }
                        else
                        {
                            if (model.KycDocumentPoi.File.IsBase64() && !string.IsNullOrEmpty(objModel.KycdocumentPoi))

                            {
                                _fileHelper.Delete(Path.Combine(fileSavePath, objModel.KycdocumentPoi));
                                objModel.KycdocumentPoi = model.KycDocumentPoi != null ? _fileHelper.Save(model.KycDocumentPoi.File, fileSavePath, model.KycDocumentPoi.FileName) : null;

                            }

                        }

                        if (model.KycDocumentPoa != null && model.KycDocumentPoa.IsEditMode && string.IsNullOrEmpty(model.KycDocumentPoa.File))
                        {
                            _fileHelper.Delete(Path.Combine(fileSavePath, objModel.KycdocumentPoa));

                        }
                        else
                        {
                            if (model.KycDocumentPoa.File.IsBase64() && !string.IsNullOrEmpty(objModel.KycdocumentPoa))

                            {
                                _fileHelper.Delete(Path.Combine(fileSavePath, objModel.KycdocumentPoa));
                                objModel.KycdocumentPoa = model.KycDocumentPoa != null ? _fileHelper.Save(model.KycDocumentPoa.File, fileSavePath, model.KycDocumentPoa.FileName) : null;

                            }

                        }

                        if (model.BlankCheque1 != null && model.BlankCheque1.IsEditMode && string.IsNullOrEmpty(model.BlankCheque1.File))
                        {
                            _fileHelper.Delete(Path.Combine(fileSavePath, objModel.KycdocumentPoi));

                        }
                        else
                        {
                            if (model.BlankCheque1.File.IsBase64() && !string.IsNullOrEmpty(objModel.KycdocumentPoi))

                            {
                                _fileHelper.Delete(Path.Combine(fileSavePath, objModel.KycdocumentPoi));
                                objModel.KycdocumentPoi = model.BlankCheque1 != null ? _fileHelper.Save(model.BlankCheque1.File, fileSavePath, model.BlankCheque1.FileName) : null;

                            }

                        }

                        if (model.BlankCheque2 != null && model.BlankCheque2.IsEditMode && string.IsNullOrEmpty(model.BlankCheque2.File))
                        {
                            _fileHelper.Delete(Path.Combine(fileSavePath, objModel.BlankCheque2));

                        }
                        else
                        {
                            if (model.BlankCheque2.File.IsBase64() && !string.IsNullOrEmpty(objModel.BlankCheque2))

                            {
                                _fileHelper.Delete(Path.Combine(fileSavePath, objModel.BlankCheque2));
                                objModel.BlankCheque2 = model.BlankCheque2 != null ? _fileHelper.Save(model.BlankCheque2.File, fileSavePath, model.BlankCheque2.FileName) : null;

                            }

                        }

                        if (model.LoanDocument != null && model.LoanDocument.IsEditMode && string.IsNullOrEmpty(model.LoanDocument.File))
                        {
                            _fileHelper.Delete(Path.Combine(fileSavePath, objModel.KycdocumentPoi));

                        }
                        else
                        {
                            if (model.LoanDocument.File.IsBase64() && !string.IsNullOrEmpty(objModel.KycdocumentPoi))

                            {
                                _fileHelper.Delete(Path.Combine(fileSavePath, objModel.KycdocumentPoi));
                                objModel.KycdocumentPoi = model.LoanDocument != null ? _fileHelper.Save(model.LoanDocument.File, fileSavePath, model.LoanDocument.FileName) : null;

                            }

                        }

                        if (model.AggrementLastPage != null && model.AggrementLastPage.IsEditMode && string.IsNullOrEmpty(model.AggrementLastPage.File))
                        {
                            _fileHelper.Delete(Path.Combine(fileSavePath, objModel.AggrementLastPage));

                        }
                        else
                        {
                            if (model.AggrementLastPage.File.IsBase64() && !string.IsNullOrEmpty(objModel.AggrementLastPage))

                            {
                                _fileHelper.Delete(Path.Combine(fileSavePath, objModel.AggrementLastPage));
                                objModel.AggrementLastPage = model.AggrementLastPage != null ? _fileHelper.Save(model.AggrementLastPage.File, fileSavePath, model.AggrementLastPage.FileName) : null;

                            }

                        }

                        if (model.PromissoryNote != null && model.PromissoryNote.IsEditMode && string.IsNullOrEmpty(model.PromissoryNote.File))
                        {
                            _fileHelper.Delete(Path.Combine(fileSavePath, objModel.KycdocumentPoi));

                        }
                        else
                        {
                            if (model.PromissoryNote.File.IsBase64() && !string.IsNullOrEmpty(objModel.KycdocumentPoi))

                            {
                                _fileHelper.Delete(Path.Combine(fileSavePath, objModel.KycdocumentPoi));
                                objModel.KycdocumentPoi = model.PromissoryNote != null ? _fileHelper.Save(model.PromissoryNote.File, fileSavePath, model.PromissoryNote.FileName) : null;

                            }

                        }

                        if (model.AtmwithdrawalSlip != null && model.AtmwithdrawalSlip.IsEditMode && string.IsNullOrEmpty(model.AtmwithdrawalSlip.File))
                        {
                            _fileHelper.Delete(Path.Combine(fileSavePath, objModel.AtmwithdrawalSlip));

                        }
                        else
                        {
                            if (model.AtmwithdrawalSlip.File.IsBase64() && !string.IsNullOrEmpty(objModel.AtmwithdrawalSlip))

                            {
                                _fileHelper.Delete(Path.Combine(fileSavePath, objModel.AtmwithdrawalSlip));
                                objModel.AtmwithdrawalSlip = model.AtmwithdrawalSlip != null ? _fileHelper.Save(model.AtmwithdrawalSlip.File, fileSavePath, model.AtmwithdrawalSlip.FileName) : null;

                            }

                        }

                        if (model.ForeClosureLetter != null && model.ForeClosureLetter.IsEditMode && string.IsNullOrEmpty(model.ForeClosureLetter.File))
                        {
                            _fileHelper.Delete(Path.Combine(fileSavePath, objModel.KycdocumentPoi));

                        }
                        else
                        {
                            if (model.ForeClosureLetter.File.IsBase64() && !string.IsNullOrEmpty(objModel.KycdocumentPoi))

                            {
                                _fileHelper.Delete(Path.Combine(fileSavePath, objModel.KycdocumentPoi));
                                objModel.KycdocumentPoi = model.ForeClosureLetter != null ? _fileHelper.Save(model.ForeClosureLetter.File, fileSavePath, model.ForeClosureLetter.FileName) : null;

                            }

                        }


                        await _db.SaveChangesAsync();
                    }
                }

                return true;

            }
            catch
            {
                throw;

            }
        }
        private async Task<bool> SetExistingLoanDetail(BtGoldLoanLeadExistingLoanPostModel model, long LeadId)
        {
            try
            {
                if (model.Id == default || model.Id == 0)
                {
                    var objModel = new BtgoldLoanLeadExistingLoanDetail()
                    {

                        LeadId = LeadId,
                        BankName = !string.IsNullOrEmpty(model.BankName) ? model.BankName : null,
                        Date = model.Date.HasValue ? model.Date : null,
                        Amount = model.Amount.HasValue ? model.Amount : null,
                        JewelleryValuation = model.JewelleryValuation.HasValue ? model.JewelleryValuation : null,
                        OutstandingAmount = model.OutstandingAmount.HasValue ? model.OutstandingAmount : null,
                        BalanceTransferAmount = model.BalanceTransferAmount.HasValue ? model.BalanceTransferAmount : null,
                        RequiredAmount = model.RequiredAmount.HasValue ? model.RequiredAmount : null,
                        Tenure = model.Tenure.HasValue ? model.Tenure : null,

                    };

                    var result = await _db.BtgoldLoanLeadExistingLoanDetail.AddAsync(objModel);
                    await _db.SaveChangesAsync();

                }
                else
                {
                    var objModel = await _db.BtgoldLoanLeadExistingLoanDetail.FirstOrDefaultAsync(x => x.Id == model.Id && x.LeadId == LeadId);
                    if (objModel != null)
                    {
                        objModel.BankName = !string.IsNullOrEmpty(model.BankName) ? model.BankName : null;
                        objModel.Date = model.Date.HasValue ? model.Date : null;
                        objModel.Amount = model.Amount.HasValue ? model.Amount : null;
                        objModel.JewelleryValuation = model.JewelleryValuation.HasValue ? model.JewelleryValuation : null;
                        objModel.OutstandingAmount = model.OutstandingAmount.HasValue ? model.OutstandingAmount : null;
                        objModel.BalanceTransferAmount = model.BalanceTransferAmount.HasValue ? model.BalanceTransferAmount : null;
                        objModel.RequiredAmount = model.RequiredAmount.HasValue ? model.RequiredAmount : null;
                        objModel.Tenure = model.Tenure.HasValue ? model.Tenure : null;
                        await _db.SaveChangesAsync();
                    }
                }

                return true;

            }
            catch
            {
                throw;
            }
        }
        private async Task<bool> SetJewelleryDetail(BtGoldLoanLeadJewelleryDetailPostModel model, long LeadId)
        {
            try
            {
                if (model.Id == default || model.Id == 0)
                {
                    var objModel = new BtgoldLoanLeadJewelleryDetail()
                    {

                        LeadId = LeadId,
                        JewelleryTypeId = model.JewelleryTypeId.HasValue ? model.JewelleryTypeId : null,
                        Quantity = model.Quantity.HasValue ? model.Quantity : null,
                        Weight = model.Weight.HasValue ? model.Weight : null,
                        Karats = model.Karats.HasValue ? model.Karats : null

                    };

                    var result = await _db.BtgoldLoanLeadJewelleryDetail.AddAsync(objModel);
                    await _db.SaveChangesAsync();

                }
                else
                {
                    var objModel = await _db.BtgoldLoanLeadJewelleryDetail.FirstOrDefaultAsync(x => x.Id == model.Id);
                    if (objModel != null)
                    {

                        objModel.JewelleryTypeId = model.JewelleryTypeId.HasValue ? model.JewelleryTypeId : null;
                        objModel.Quantity = model.Quantity.HasValue ? model.Quantity : null;
                        objModel.Weight = model.Weight.HasValue ? model.Weight : null;
                        objModel.Karats = model.Karats.HasValue ? model.Karats : null;
                        await _db.SaveChangesAsync();
                    }
                }

                return true;

            }
            catch
            {
                throw;
            }
        }
        private async Task<bool> SetKYCDetail(BtGoldLoanLeadKYCDetailPostModel model, long LeadId)
        {
            try
            {
                if (model.Id == default || model.Id == 0)
                {
                    var objModel = new BtgoldLoanLeadKycdetail()
                    {

                        LeadId = LeadId,
                        PoidocumentTypeId = model.PoidocumentTypeId,
                        PoadocumentTypeId = model.PoadocumentTypeId,
                        PoidocumentNumber = !string.IsNullOrEmpty(model.PoidocumentNumber) ? model.PoidocumentNumber : null,
                        PoadocumentNumber = !string.IsNullOrEmpty(model.PoadocumentNumber) ? model.PoadocumentNumber : null

                    };

                    var result = await _db.BtgoldLoanLeadKycdetail.AddAsync(objModel);
                    await _db.SaveChangesAsync();

                }
                else
                {
                    var objModel = await _db.BtgoldLoanLeadKycdetail.FirstOrDefaultAsync(x => x.Id == model.Id);
                    if (objModel != null)
                    {

                        objModel.PoidocumentTypeId = model.PoidocumentTypeId;
                        objModel.PoadocumentTypeId = model.PoadocumentTypeId;
                        objModel.PoidocumentNumber = !string.IsNullOrEmpty(model.PoidocumentNumber) ? model.PoidocumentNumber : null;
                        objModel.PoadocumentNumber = !string.IsNullOrEmpty(model.PoadocumentNumber) ? model.PoadocumentNumber : null;
                        await _db.SaveChangesAsync();
                    }
                }

                return true;

            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
