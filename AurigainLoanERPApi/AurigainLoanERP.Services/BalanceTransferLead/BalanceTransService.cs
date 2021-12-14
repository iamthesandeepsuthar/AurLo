using AurigainLoanERP.Data.Database;
using AurigainLoanERP.Shared.Common;
using AurigainLoanERP.Shared.Common.Method;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using AurigainLoanERP.Shared.ExtensionMethod;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static AurigainLoanERP.Shared.Enums.FixedValueEnums;

namespace AurigainLoanERP.Services.BalanceTransferLead
{
    public class BalanceTransService : BaseService, IBalanceTransService
    {
        public readonly IMapper _mapper;
        private readonly FileHelper _fileHelper;
        private readonly Security _security;
        private readonly AurigainContext _db;
        public BalanceTransService(IMapper mapper, AurigainContext db, IHostingEnvironment environment, IConfiguration _configuration)
        {
            this._mapper = mapper;
            _db = db;
            _fileHelper = new FileHelper(environment);
            _security = new Security(_configuration);

        }
        public async Task<ApiServiceResponseModel<string>> AddUpdateBTGoldLoanExternalLeadAsync(BTGoldLoanLeadPostModel model)
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
                    objData.LeadStatus = "New";
                    objData.ApprovalStatus = "Pending";
                    objData.CustomerUserId = model.CustomerUserId;
                    objData.LoanAmount = model.LoanAmount;
                    objData.ProductId = model.ProductId;
                    objData.IsInternalLead = false;
                    objData.CreatedBy = _loginUserDetail.UserId ?? null;
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
                    objData.ModifiedBy = _loginUserDetail.UserId ?? null;
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

                //filte Conditions
                switch (_loginUserDetail.RoleId)
                {

                    case (int)UserRoleEnum.SuperAdmin:
                    case (int)(UserRoleEnum.Admin):
                    case (int)(UserRoleEnum.WebOperator):

                        result = (from goldLoanLead in _db.BtgoldLoanLead
                                  where !goldLoanLead.IsDelete
                                  && (string.IsNullOrEmpty(model.Search) || goldLoanLead.FullName.Contains(model.Search) || goldLoanLead.FatherName.Contains(model.Search) || goldLoanLead.Gender.Contains(model.Search) || goldLoanLead.BtgoldLoanLeadAddressDetail.FirstOrDefault().AeraPincode.Pincode.Contains(model.Search)) ||
                                  goldLoanLead.Product.Name.Contains(model.Search)
                                  select goldLoanLead);
                        break;

                    case (int)(UserRoleEnum.Supervisor):

                        List<long> employees = _db.UserReportingPerson.Where(x => x.ReportingUserId == _loginUserDetail.UserId).Select(x => x.UserId).ToList();

                        result = (from goldLoanLead in _db.BtgoldLoanLead
                                  where !goldLoanLead.IsDelete && (goldLoanLead.CreatedBy == _loginUserDetail.UserId || employees.Contains(goldLoanLead.LeadSourceByuserId))
                                  && (string.IsNullOrEmpty(model.Search) || goldLoanLead.FullName.Contains(model.Search) || goldLoanLead.FatherName.Contains(model.Search) || goldLoanLead.Gender.Contains(model.Search) || goldLoanLead.BtgoldLoanLeadAddressDetail.FirstOrDefault().AeraPincode.Pincode.Contains(model.Search)) ||
                                  goldLoanLead.Product.Name.Contains(model.Search)
                                  select goldLoanLead);

                        break;

                    case (int)(UserRoleEnum.Agent):
                    case (int)(UserRoleEnum.DoorStepAgent):

                        result = (from goldLoanLead in _db.BtgoldLoanLead
                                  where !goldLoanLead.IsDelete &&
                                   (goldLoanLead.CreatedBy == _loginUserDetail.UserId || goldLoanLead.LeadSourceByuserId == _loginUserDetail.UserId)
                                  && (string.IsNullOrEmpty(model.Search) || goldLoanLead.FullName.Contains(model.Search) || goldLoanLead.FatherName.Contains(model.Search) || goldLoanLead.Gender.Contains(model.Search) || goldLoanLead.BtgoldLoanLeadAddressDetail.FirstOrDefault().AeraPincode.Pincode.Contains(model.Search)) ||
                                  goldLoanLead.Product.Name.Contains(model.Search)
                                  select goldLoanLead);

                        break;

                    case (int)(UserRoleEnum.Customer):

                        result = (from goldLoanLead in _db.BtgoldLoanLead
                                  where !goldLoanLead.IsDelete && (goldLoanLead.CreatedBy == _loginUserDetail.UserId || goldLoanLead.CustomerUserId == _loginUserDetail.UserId)
                                  && (string.IsNullOrEmpty(model.Search) || goldLoanLead.FullName.Contains(model.Search) || goldLoanLead.FatherName.Contains(model.Search) || goldLoanLead.Gender.Contains(model.Search) || goldLoanLead.BtgoldLoanLeadAddressDetail.FirstOrDefault().AeraPincode.Pincode.Contains(model.Search)) ||
                                  goldLoanLead.Product.Name.Contains(model.Search)
                                  select goldLoanLead);

                        break;





                    default:

                        result = (from goldLoanLead in _db.BtgoldLoanLead
                                  where !goldLoanLead.IsDelete && goldLoanLead.CreatedBy == _loginUserDetail.UserId
                                  && (string.IsNullOrEmpty(model.Search) || goldLoanLead.FullName.Contains(model.Search) || goldLoanLead.FatherName.Contains(model.Search) || goldLoanLead.Gender.Contains(model.Search) || goldLoanLead.BtgoldLoanLeadAddressDetail.FirstOrDefault().AeraPincode.Pincode.Contains(model.Search)) ||
                                  goldLoanLead.Product.Name.Contains(model.Search)
                                  select goldLoanLead);

                        break;
                }


                //result = (from goldLoanLead in _db.BtgoldLoanLead
                //          where !goldLoanLead.IsDelete &&
                //          (IsShowAll ? true : (goldLoanLead.CreatedBy == _loginUserDetail.UserId || (_loginUserDetail.RoleId == (int)UserRoleEnum.Customer ? goldLoanLead.CustomerUserId == _loginUserDetail.UserId : goldLoanLead.LeadSourceByuserId == _loginUserDetail.UserId)))

                //          && (string.IsNullOrEmpty(model.Search) || goldLoanLead.FullName.Contains(model.Search) || goldLoanLead.FatherName.Contains(model.Search) || goldLoanLead.Gender.Contains(model.Search) || goldLoanLead.BtgoldLoanLeadAddressDetail.FirstOrDefault().AeraPincode.Pincode.Contains(model.Search)) ||
                //          goldLoanLead.Product.Name.Contains(model.Search)
                //          select goldLoanLead);


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
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.CreatedOn ascending select orderData) : (from orderData in result orderby orderData.CreatedOn descending select orderData);
                        break;
                }

                var data = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue).Include(x => x.BtgoldLoanLeadApprovalActionHistory).ThenInclude(x => x.ActionTakenByUser).ThenInclude(x => x.UserRole);

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
                                              IsInternalLead = detail.IsInternalLead,
                                              Email = detail.EmailId,
                                              IsActive = detail.IsActive.Value,
                                              LeadStatus = detail.LeadStatus,
                                              ProductName = detail.Product.Name,
                                              IsStatusCompleted = detail.BtgoldLoanLeadStatusActionHistory.Where(x => x.LeadStatus.Value == ((int)LeadStatus.Completed)).Count() > 0 ? true : false,
                                              Pincode = detail.BtgoldLoanLeadAddressDetail.FirstOrDefault().AeraPincode.Pincode,
                                              ApprovalStatus = detail.ApprovalStatus,
                                              ApprovedStage = detail.BtgoldLoanLeadApprovalActionHistory.Count > 0 ?
                                              detail.BtgoldLoanLeadApprovalActionHistory.OrderByDescending(x => x.Id).FirstOrDefault(x => x.ActionTakenByUserId.HasValue).ActionTakenByUser.UserRole.UserRoleLevel.Value : (int?)null
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
        public async Task<ApiServiceResponseModel<BTGoldLoanLeadViewModel>> DetailbyIdAsync(long id)
        {
            BTGoldLoanLeadViewModel objModel = new BTGoldLoanLeadViewModel();
            try
            {
                var detail = await _db.BtgoldLoanLead.Where(x => x.Id == id && !x.IsDelete && x.IsActive == true)
                    .Include(x => x.LeadSourceByuser).Include(x => x.CustomerUser)
                    .Include(x => x.Product).ThenInclude(x => x.ProductCategory)
                    .Include(x => x.BtgoldLoanLeadAddressDetail).ThenInclude(x => x.AeraPincode).ThenInclude(x => x.District).ThenInclude(x => x.State)
                    .Include(x => x.BtgoldLoanLeadAppointmentDetail).ThenInclude(x => x.Branch).ThenInclude(x => x.Bank)
                    .Include(x => x.BtgoldLoanLeadExistingLoanDetail)
                    .Include(x => x.BtgoldLoanLeadJewelleryDetail).ThenInclude(x => x.JewelleryType)
                    .Include(x => x.BtgoldLoanLeadDocumentDetail)
                    .Include(x => x.BtgoldLoanLeadKycdetail).ThenInclude(x => x.PoadocumentType)
                    .Include(x => x.BtgoldLoanLeadKycdetail).ThenInclude(x => x.PoidocumentType).FirstOrDefaultAsync();
                if (detail != null)
                {
                    objModel.Id = detail.Id;
                    objModel.ProductCategoryName = detail.Product.ProductCategory.Name;
                    objModel.ProductId = detail.ProductId;
                    objModel.ProductName = detail.Product.Name ?? null;
                    objModel.FullName = detail.FullName ?? null;
                    objModel.FatherName = detail.FatherName ?? null;
                    objModel.Gender = detail.Gender ?? null;
                    objModel.DateOfBirth = detail.DateOfBirth;
                    objModel.Profession = detail.Profession ?? null;
                    objModel.Mobile = detail.Mobile ?? null;
                    objModel.IsInternalLead = detail.IsInternalLead;
                    objModel.EmailId = detail.EmailId ?? null;
                    objModel.CustomerUserId = detail.CustomerUserId;
                    objModel.CustomerUserName = detail.CustomerUser.UserName ?? null;
                    objModel.SecondaryMobile = detail.SecondaryMobile ?? null;
                    objModel.Purpose = detail.Purpose ?? null;
                    objModel.LoanAmount = detail.LoanAmount;
                    objModel.LeadSourceByuserId = detail.LeadSourceByuserId;
                    objModel.LeadSourceByuserName = detail.LeadSourceByuser.UserName ?? null;
                    if (detail.BtgoldLoanLeadAddressDetail != null)
                    {
                        objModel.DetailAddress = detail.BtgoldLoanLeadAddressDetail.Select(x => new BtGoldLoanLeadAddressViewModel
                        {
                            Id = x.Id,
                            Address = x.Address ?? null,
                            AeraPincodeId = x.AeraPincodeId ?? null,
                            AreaName = x.AeraPincode.AreaName ?? null,
                            PinCode = x.AeraPincode.Pincode ?? null,
                            State = x.AeraPincode.District.State.Name ?? null,
                            District = x.AeraPincode.District.Name ?? null,
                            CorrespondAddress = x.CorrespondAddress ?? null,
                            CorrespondAeraPincodeId = x.CorrespondAeraPincodeId ?? null,
                            CorrespondAreaName = x.CorrespondAeraPincode.AreaName ?? null,
                            CorrespondPinCode = x.CorrespondAeraPincode.Pincode ?? null,
                            CorrespondState = x.CorrespondAeraPincode.District.State.Name ?? null,
                            CorrespondDistrict = x.CorrespondAeraPincode.District.Name ?? null
                        }).FirstOrDefault();
                    }
                    if (detail.BtgoldLoanLeadAppointmentDetail != null)
                    {
                        objModel.AppointmentDetail = detail.BtgoldLoanLeadAppointmentDetail.Select(x => new BtGoldLoanLeadAppointmentViewModel
                        {
                            Id = x.Id,
                            BranchId = x.BranchId ?? null,
                            BranchName = x.Branch.BranchName ?? null,
                            BankName = x.Branch.Bank.Name ?? null,
                            Ifsc = x.Branch.Ifsc ?? null,
                            Pincode = x.Branch.Pincode ?? null,
                            AppointmentDate = x.AppointmentDate ?? null,
                            AppointmentTime = x.AppointmentTime.HasValue ? new DateTime(x.AppointmentTime.Value.Ticks).ToString("HH:mm:ss") : null
                        }).FirstOrDefault();
                    }
                    if (detail.BtgoldLoanLeadDocumentDetail != null)
                    {
                        objModel.DocumentDetail = detail.BtgoldLoanLeadDocumentDetail.Select(x => new BtGoldLoanLeadDocumentViewModel
                        {
                            Id = x.Id,
                            CustomerPhoto = !string.IsNullOrEmpty(x.CustomerPhoto) ? x.CustomerPhoto.ToAbsolutePath() : null,
                            KycDocumentPoi = !string.IsNullOrEmpty(x.KycdocumentPoi) ? x.KycdocumentPoi.ToAbsolutePath() : null,
                            KycDocumentPoa = !string.IsNullOrEmpty(x.KycdocumentPoa) ? x.KycdocumentPoa.ToAbsolutePath() : null,
                            BlankCheque1 = !string.IsNullOrEmpty(x.BlankCheque1) ? x.BlankCheque1.ToAbsolutePath() : null,
                            BlankCheque2 = !string.IsNullOrEmpty(x.BlankCheque2) ? x.BlankCheque2.ToAbsolutePath() : null,
                            LoanDocument = !string.IsNullOrEmpty(x.LoanDocument) ? x.LoanDocument.ToAbsolutePath() : null,
                            AggrementLastPage = !string.IsNullOrEmpty(x.AggrementLastPage) ? x.AggrementLastPage.ToAbsolutePath() : null,
                            PromissoryNote = !string.IsNullOrEmpty(x.PromissoryNote) ? x.PromissoryNote.ToAbsolutePath() : null,
                            AtmwithdrawalSlip = !string.IsNullOrEmpty(x.AtmwithdrawalSlip) ? x.AtmwithdrawalSlip.ToAbsolutePath() : null,
                            ForeClosureLetter = !string.IsNullOrEmpty(x.ForeClosureLetter) ? x.ForeClosureLetter.ToAbsolutePath() : null,
                        }).FirstOrDefault();
                    }
                    if (detail.BtgoldLoanLeadExistingLoanDetail != null)
                    {
                        objModel.ExistingLoanDetail = detail.BtgoldLoanLeadExistingLoanDetail.Select(x => new BtGoldLoanLeadExistingLoanViewModel
                        {
                            Id = x.Id,
                            BankName = x.BankName ?? null,
                            Amount = x.Amount ?? null,
                            Date = x.Date ?? null,
                            JewelleryValuation = x.JewelleryValuation ?? null,
                            OutstandingAmount = x.OutstandingAmount ?? null,
                            BalanceTransferAmount = x.BalanceTransferAmount ?? null,
                            RequiredAmount = x.RequiredAmount ?? null,
                            Tenure = x.Tenure ?? null
                        }).FirstOrDefault();
                    }
                    if (detail.BtgoldLoanLeadJewelleryDetail != null)
                    {
                        objModel.JewelleryDetail = detail.BtgoldLoanLeadJewelleryDetail.Select(x => new BtGoldLoanLeadJewelleryDetailViewModel
                        {
                            Id = x.Id,
                            JewelleryTypeId = x.JewelleryTypeId ?? null,
                            JewelleryType = x.JewelleryType.Name ?? null,
                            Quantity = x.Quantity ?? null,
                            Weight = x.Weight ?? null,
                            Karats = x.Karats ?? null,
                        }).FirstOrDefault();
                    }
                    if (detail.BtgoldLoanLeadKycdetail != null)
                    {
                        objModel.KYCDetail = detail.BtgoldLoanLeadKycdetail.Select(x => new BtGoldLoanLeadKYCDetailViewModel
                        {
                            Id = x.Id,
                            PoidocumentTypeId = x.PoidocumentTypeId,
                            PoidocumentType = x.PoidocumentType.DocumentName ?? null,
                            PoidocumentNumber = x.PoidocumentNumber ?? null,
                            PoadocumentTypeId = x.PoadocumentTypeId,
                            PoadocumentNumber = x.PoadocumentNumber ?? null,
                            PoadocumentType = x.PoadocumentType.DocumentName ?? null

                        }).FirstOrDefault();
                    }
                    return CreateResponse(objModel, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<BTGoldLoanLeadViewModel>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.NotFound));
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<BTGoldLoanLeadViewModel>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task<ApiServiceResponseModel<string>> AddUpdateBTGoldLoanInternalLeadAsync(BTGoldLoanLeadPostModel model)
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
                    objData.LeadStatus = "New";
                    objData.ApprovalStatus = "Pending";
                    objData.LoanAccountNumber = !string.IsNullOrEmpty(model.LoanAccountNumber) ? model.LoanAccountNumber : null;
                    objData.IsInternalLead = true;
                    objData.CreatedBy = _loginUserDetail.UserId ?? null;
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
                    objData.LoanAccountNumber = !string.IsNullOrEmpty(model.LoanAccountNumber) ? model.LoanAccountNumber : null;
                    objData.LoanAmount = model.LoanAmount;
                    objData.ModifiedOn = DateTime.Now;
                    objData.ModifiedBy = _loginUserDetail.UserId ?? null;
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

                _db.Database.CommitTransaction();
                return CreateResponse<string>(model.Id.ToString(), model.Id > 0 ? ResponseMessage.Update : ResponseMessage.Save, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {

                _db.Database.RollbackTransaction();

                return CreateResponse<string>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }

        }
        public async Task<ApiServiceResponseModel<object>> UpdateLeadApprovalStageAsync(BtGoldLoanLeadApprovalStagePostModel model)
        {
            try
            {
                await _db.Database.BeginTransactionAsync();
                var objModel = new BtgoldLoanLeadApprovalActionHistory()
                {
                    LeadId = model.LeadId,
                    ActionDate = DateTime.Now,
                    ActionTakenByUserId = _loginUserDetail.UserId,
                    Remarks = !string.IsNullOrEmpty(model.Remarks) ? model.Remarks : null,
                    ApprovalStatus = model.ApprovalStatus,
                };

                var result = await _db.BtgoldLoanLeadApprovalActionHistory.AddAsync(objModel);
                var leadDetail = await _db.BtgoldLoanLead.Where(x => x.Id == model.LeadId).FirstOrDefaultAsync();
                if (leadDetail != null)
                {
                    switch (model.ApprovalStatus)
                    {
                        case 1:
                            leadDetail.ApprovalStatus = LeadApprovalStatus.Approved.GetStringValue();
                            break;
                        case 2:
                            leadDetail.ApprovalStatus = LeadApprovalStatus.Rejected.GetStringValue();
                            break;

                        default:
                            leadDetail.LeadStatus = "Pending";
                            break;
                    }

                    await _db.SaveChangesAsync();
                    _db.Database.CommitTransaction();
                }
                else
                {
                    _db.Database.RollbackTransaction();
                    return CreateResponse<object>(false, ResponseMessage.NotFound, false, ((int)ApiStatusCode.NotFound));
                }
                return CreateResponse<object>(true, ResponseMessage.Save, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception)
            {
                _db.Database.RollbackTransaction();
                return CreateResponse<object>(false, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException));
            }

        }
        public async Task<ApiServiceResponseModel<object>> UpdateLeadStatusAsync(LeadStatusModel model)
        {
            try
            {
                await _db.Database.BeginTransactionAsync();
                var objModel = new BtgoldLoanLeadStatusActionHistory()
                {
                    LeadId = model.LeadId,
                    ActionDate = DateTime.Now,
                    ActionTakenByUserId = _loginUserDetail.UserId,
                    Remarks = !string.IsNullOrEmpty(model.Remark) ? model.Remark : null,
                    LeadStatus = model.LeadStatus,
                };
                var result = await _db.BtgoldLoanLeadStatusActionHistory.AddAsync(objModel);
                var leadDetail = await _db.BtgoldLoanLead.Where(x => x.Id == model.LeadId).FirstOrDefaultAsync();
                if (leadDetail != null)
                {
                    switch (model.LeadStatus)
                    {
                        case 1:
                            leadDetail.LeadStatus = LeadStatus.Pending.GetStringValue();
                            break;
                        case 2:
                            leadDetail.LeadStatus = LeadStatus.Mismatched.GetStringValue();
                            break;

                        case 3:
                            leadDetail.LeadStatus = LeadStatus.InCompleted.GetStringValue();
                            break;
                        case 4:
                            leadDetail.LeadStatus = LeadStatus.Rejected.GetStringValue();
                            break;
                        case 5:
                            leadDetail.LeadStatus = LeadStatus.Completed.GetStringValue();
                            break;

                        default:
                            leadDetail.LeadStatus = "New";
                            break;
                    }

                    await _db.SaveChangesAsync();
                    _db.Database.CommitTransaction();
                }
                else
                {
                    _db.Database.RollbackTransaction();
                    return CreateResponse<object>(false, ResponseMessage.NotFound, false, ((int)ApiStatusCode.NotFound));
                }
                await _db.SaveChangesAsync();
                _db.Database.CommitTransaction();
                return CreateResponse<object>(true, ResponseMessage.Save, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception)
            {
                _db.Database.RollbackTransaction();
                return CreateResponse<object>(false, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException));

            }

        }
        public async Task<ApiServiceResponseModel<List<LeadStatusActionHistory>>> BTGoldLoanLeadStatusHistory(long leadId)
        {
            List<LeadStatusActionHistory> history = new List<LeadStatusActionHistory>();
            try
            {
                var data = _db.BtgoldLoanLeadStatusActionHistory.Where(x => x.LeadId == leadId).Include(x => x.ActionTakenByUser).ThenInclude(y => y.UserRole);
                if (data != null)
                {
                    history = await data.Select(d => new LeadStatusActionHistory
                    {
                        ActionDate = d.ActionDate,
                        LeadId = d.LeadId,
                        LeadStatus = d.LeadStatus == 1 ? LeadStatus.Pending.GetStringValue() :
                                d.LeadStatus == 2 ? LeadStatus.Mismatched.GetStringValue() :
                                d.LeadStatus == 3 ? LeadStatus.InCompleted.GetStringValue() :
                                d.LeadStatus == 4 ? LeadStatus.Rejected.GetStringValue() :
                                d.LeadStatus == 5 ? LeadStatus.Completed.GetStringValue() :
                                "New",
                        ActionTakenBy = d.ActionTakenByUserId.HasValue ? $"{d.ActionTakenByUser.UserName} ({d.ActionTakenByUser.UserRole.Name})" : null,
                        ActionTakenByUserId = d.ActionTakenByUserId ?? null,
                        Id = d.Id,
                        Remark = d.Remarks
                    }).ToListAsync();
                    return CreateResponse<List<LeadStatusActionHistory>>(history, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<List<LeadStatusActionHistory>>(history, ResponseMessage.Success, false, ((int)ApiStatusCode.RecordNotFound));
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<List<LeadStatusActionHistory>>(null, ex.Message, false, ((int)ApiStatusCode.ServerException));
            }
        }
        public async Task<ApiServiceResponseModel<List<LeadStatusActionHistory>>> BTGoldLoanApprovalStatusHistory(long leadId)
        {
            List<LeadStatusActionHistory> history = new List<LeadStatusActionHistory>();
            try
            {
                var data = _db.BtgoldLoanLeadApprovalActionHistory.Where(x => x.LeadId == leadId).Include(x => x.ActionTakenByUser).ThenInclude(y => y.UserRole);
                if (data != null)
                {
                    history = await data.Select(d => new LeadStatusActionHistory
                    {
                        ActionDate = d.ActionDate,
                        LeadId = d.LeadId,
                        LeadStatus = d.ApprovalStatus == 1 ? LeadApprovalStatus.Approved.GetStringValue() : LeadApprovalStatus.Rejected.GetStringValue(),

                        ActionTakenBy = d.ActionTakenByUserId.HasValue ? $"{d.ActionTakenByUser.UserName} ({d.ActionTakenByUser.UserRole.Name})" : null,
                        ActionTakenByUserId = d.ActionTakenByUserId ?? null,
                        Id = d.Id,
                        Remark = d.Remarks
                    }).ToListAsync();
                    return CreateResponse<List<LeadStatusActionHistory>>(history, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<List<LeadStatusActionHistory>>(history, ResponseMessage.Success, false, ((int)ApiStatusCode.RecordNotFound));
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<List<LeadStatusActionHistory>>(null, ex.Message, false, ((int)ApiStatusCode.ServerException));
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
                var encrptPassword = _security.Base64Encode("12345");
                Random random = new Random();
                UserMaster user = new UserMaster
                {
                    Email = model.EmailId,
                    Mobile = model.Mobile,
                    IsActive = true,
                    IsApproved = false,
                    IsWhatsApp = true,
                    CreatedOn = DateTime.Now,
                    Password = encrptPassword,
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
                    CreatedBy = _loginUserDetail.UserId ?? null //(int)UserRoleEnum.Admin
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
                        CustomerPhoto = model.CustomerPhoto != null ? Path.Combine(fileSavePath, _fileHelper.Save(model.CustomerPhoto.File, fileSavePath, model.CustomerPhoto.FileName)) : null,
                        KycdocumentPoi = model.KycDocumentPoi != null ? Path.Combine(fileSavePath, _fileHelper.Save(model.KycDocumentPoi.File, fileSavePath, model.KycDocumentPoi.FileName)) : null,
                        KycdocumentPoa = model.KycDocumentPoa != null ? Path.Combine(fileSavePath, _fileHelper.Save(model.KycDocumentPoa.File, fileSavePath, model.KycDocumentPoa.FileName)) : null,
                        BlankCheque1 = model.BlankCheque1 != null ? Path.Combine(fileSavePath, _fileHelper.Save(model.BlankCheque1.File, fileSavePath, model.BlankCheque1.FileName)) : null,
                        BlankCheque2 = model.BlankCheque2 != null ? Path.Combine(fileSavePath, _fileHelper.Save(model.BlankCheque2.File, fileSavePath, model.BlankCheque2.FileName)) : null,
                        LoanDocument = model.LoanDocument != null ? Path.Combine(fileSavePath, _fileHelper.Save(model.LoanDocument.File, fileSavePath, model.LoanDocument.FileName)) : null,
                        AggrementLastPage = model.AggrementLastPage != null ? Path.Combine(fileSavePath, _fileHelper.Save(model.AggrementLastPage.File, fileSavePath, model.AggrementLastPage.FileName)) : null,
                        PromissoryNote = model.PromissoryNote != null ? Path.Combine(fileSavePath, _fileHelper.Save(model.PromissoryNote.File, fileSavePath, model.PromissoryNote.FileName)) : null,
                        AtmwithdrawalSlip = model.AtmwithdrawalSlip != null ? Path.Combine(fileSavePath, _fileHelper.Save(model.AtmwithdrawalSlip.File, fileSavePath, model.AtmwithdrawalSlip.FileName)) : null,
                        ForeClosureLetter = model.ForeClosureLetter != null ? Path.Combine(fileSavePath, _fileHelper.Save(model.ForeClosureLetter.File, fileSavePath, model.ForeClosureLetter.FileName)) : null,
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
                        Karats = model.Karats.HasValue ? model.Karats : null,


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
