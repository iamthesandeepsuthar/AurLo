using AurigainLoanERP.Data.Database;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AurigainLoanERP.Shared.Enums.FixedValueEnums;

namespace AurigainLoanERP.Services.FreshLead
{
    public class FreshLeadService : BaseService, IFreshLeadService
    {
        public readonly IMapper _mapper;
        private AurigainContext _db;
        private readonly EmailHelper _emailHelper;

        public FreshLeadService(IMapper mapper, AurigainContext db, IConfiguration _configuration, IHostingEnvironment environment)
        {
            this._mapper = mapper;
            _db = db;
            _emailHelper = new EmailHelper(_configuration, environment);

        }
        #region  <<Gold Loan Fresh Lead>>

        public async Task<ApiServiceResponseModel<List<GoldLoanFreshLeadListModel>>> GoldLoanFreshLeadListAsync(IndexModel model)
        {
            ApiServiceResponseModel<List<GoldLoanFreshLeadListModel>> objResponse = new ApiServiceResponseModel<List<GoldLoanFreshLeadListModel>>();
            try
            {
                IQueryable<GoldLoanFreshLead> result;
                if (model.UserId == null)
                {
                    result = (from goldLoanLead in _db.GoldLoanFreshLead
                              where !goldLoanLead.IsDelete &&
                              (string.IsNullOrEmpty(model.Search)
                              || goldLoanLead.FullName.Contains(model.Search)
                              || goldLoanLead.PrimaryMobileNumber.Contains(model.Search)
                              || goldLoanLead.FatherName.Contains(model.Search)
                              || goldLoanLead.LeadSourceByUser.UserName.Contains(model.Search)
                              || goldLoanLead.GoldLoanFreshLeadKycDocument.FirstOrDefault().PincodeArea.Pincode.Contains(model.Search))
                              || goldLoanLead.Product.Name.Contains(model.Search)
                              select goldLoanLead);
                }
                else
                {
                    result = (from goldLoanLead in _db.GoldLoanFreshLead
                              where !goldLoanLead.IsDelete && goldLoanLead.CustomerUserId == model.UserId
                              && (string.IsNullOrEmpty(model.Search) ||
                              goldLoanLead.FullName.Contains(model.Search) || goldLoanLead.PrimaryMobileNumber.Contains(model.Search) ||
                              goldLoanLead.FatherName.Contains(model.Search) ||
                              goldLoanLead.LeadSourceByUser.UserName.Contains(model.Search) ||
                              goldLoanLead.GoldLoanFreshLeadKycDocument.FirstOrDefault().PincodeArea.Pincode.Contains(model.Search)) ||
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
                    case "PrimaryMobileNumber":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.FatherName ascending select orderData) : (from orderData in result orderby orderData.FatherName descending select orderData);
                        break;
                    case "LeadSourceByUserName":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.FatherName ascending select orderData) : (from orderData in result orderby orderData.FatherName descending select orderData);
                        break;
                    case "Pincode":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.Gender ascending select orderData) : (from orderData in result orderby orderData.Gender descending select orderData);
                        break;
                    case "ProductName":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.Gender ascending select orderData) : (from orderData in result orderby orderData.Gender descending select orderData);
                        break;
                    default:
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.PrimaryMobileNumber ascending select orderData) : (from orderData in result orderby orderData.PrimaryMobileNumber descending select orderData);
                        break;
                }

                var data = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);

                objResponse.Data = await (from detail in data
                                          where detail.IsDelete == false
                                          select new GoldLoanFreshLeadListModel
                                          {
                                              Id = detail.Id,
                                              FullName = detail.FullName ?? null,
                                              PrimaryMobileNumber = detail.PrimaryMobileNumber,
                                              LeadSourceByUserName = detail.LeadSourceByUser.UserName,
                                              FatherName = detail.FatherName,
                                              Gender = detail.Gender ?? null,
                                              Email = detail.Email,
                                              LoanAmountRequired = detail.LoanAmountRequired,
                                              IsActive = detail.IsActive.Value,
                                              ProductName = detail.Product.Name,
                                              Pincode = detail.GoldLoanFreshLeadKycDocument.FirstOrDefault().PincodeArea.Pincode,
                                          }).ToListAsync();
                if (result != null)
                {
                    return CreateResponse(objResponse.Data, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: result.Count());
                }
                else
                {
                    return CreateResponse<List<GoldLoanFreshLeadListModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
                }

            }
            catch (Exception ex)
            {
                return CreateResponse<List<GoldLoanFreshLeadListModel>>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task<ApiServiceResponseModel<string>> SaveGoldLoanFreshLeadAsync(GoldLoanFreshLeadModel model)
        {
            ApiServiceResponseModel<string> objResponse = new ApiServiceResponseModel<string>();
            try
            {
                if (model != null)
                {
                    await _db.Database.BeginTransactionAsync();
                    long customerUserId = 0;
                    if (model.CustomerUserId == 0)
                    {
                        customerUserId = await SaveCustomerFreshLead(model);
                    }
                    else
                    {
                        customerUserId = model.CustomerUserId.Value;
                        var adminUserId = await _db.UserMaster.Where(x => x.UserRoleId == ((int)UserRoleEnum.Admin)).Select(x => x.Id).FirstOrDefaultAsync();
                        model.LeadSourceByUserId = adminUserId;
                    }
                    long leadId = 0;
                    if (customerUserId > 0)
                    {
                        leadId = await SaveBasicDetailGoldFreshLead(model, customerUserId);
                        if (leadId > 0)
                        {
                            if (model.KycDocument != null)
                            {
                                await SaveKycDocumentDetail(model.KycDocument, leadId);
                            }

                            if (model.AppointmentDetail != null)
                            {
                                await SaveAppointmentDetail(model.AppointmentDetail, leadId);
                            }

                            if (model.JewelleryDetail != null)
                            {
                                await SaveJewelleryDetail(model.JewelleryDetail, leadId);
                            }


                            //Dictionary<string, string> replaceValues = new Dictionary<string, string>();
                            //replaceValues.Add("{{UserName}}", user.UserName);
                            //await _emailHelper.SendHTMLBodyMail(user.Email, "Aurigain: Gold Loan Application Notification", EmailPathConstant.GoldLoanLeadGenerationTemplate, replaceValues);

                            _db.Database.CommitTransaction();
                            return CreateResponse<string>(leadId.ToString(), ResponseMessage.Save, true, ((int)ApiStatusCode.Ok));
                        }
                        else
                        {
                            _db.Database.RollbackTransaction();
                            return CreateResponse<string>(null, ResponseMessage.UserExist, false, ((int)ApiStatusCode.DataBaseTransactionFailed));
                        }
                    }
                    else
                    {
                        _db.Database.RollbackTransaction();
                        return CreateResponse<string>(null, ResponseMessage.UserExist, false, ((int)ApiStatusCode.DataBaseTransactionFailed));
                    }
                }
                else
                {
                    return CreateResponse<string>(null, ResponseMessage.InvalidData, false, ((int)ApiStatusCode.InvaildModel));
                }
            }
            catch (Exception ex)
            {
                _db.Database.RollbackTransaction();
                return CreateResponse<string>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        public async Task<ApiServiceResponseModel<GoldLoanFreshLeadViewModel>> FreshGoldLoanLeadDetailAsync(long id)
        {
            GoldLoanFreshLeadViewModel leadDetail = new GoldLoanFreshLeadViewModel();
            try
            {
                var detail = await _db.GoldLoanFreshLead.Where(x => x.Id == id)
                                    .Include(x => x.Product).ThenInclude(x => x.ProductCategory)
                                    .Include(x => x.LeadSourceByUser)
                                    .Include(x => x.GoldLoanFreshLeadJewelleryDetail)
                                    .Include(x => x.GoldLoanFreshLeadKycDocument).ThenInclude(y => y.KycDocumentType).
                                    Include(a => a.GoldLoanFreshLeadKycDocument).ThenInclude(x => x.PincodeArea).
                                    Include(a => a.GoldLoanFreshLeadKycDocument).ThenInclude(x => x.PincodeArea.District.State)
                                    //   .Include(x => x.GoldLoanFreshLeadJewelleryDetail).ThenInclude(z=>z.JewelleryTypeId)
                                    .Include(x => x.GoldLoanFreshLeadAppointmentDetail).ThenInclude(p => p.Branch).ThenInclude(p => p.Bank).FirstOrDefaultAsync();

                if (detail != null)
                {
                    leadDetail.Id = detail.Id;
                    leadDetail.ProductId = detail.ProductId;
                    leadDetail.ProductName = detail.Product.Name;
                    leadDetail.ProductCategoryName = detail.Product.ProductCategory.Name;
                    leadDetail.FullName = detail.FullName;
                    leadDetail.FatherName = detail.FatherName;
                    leadDetail.Gender = detail.Gender;
                    leadDetail.DateOfBirth = detail.DateOfBirth;
                    leadDetail.CustomerUserId = detail.CustomerUserId;
                    leadDetail.LeadSourceByUserId = detail.LeadSourceByUserId;
                    leadDetail.LeadSourceUserName = detail.LeadSourceByUser.UserName;
                    leadDetail.PrimaryMobileNumber = detail.PrimaryMobileNumber;
                    leadDetail.SecondaryMobileNumber = detail.SecondaryMobileNumber;
                    leadDetail.Purpose = detail.Purpose;
                    leadDetail.LoanAmountRequired = detail.LoanAmountRequired;
                    leadDetail.Email = detail.Email;
                    leadDetail.IsActive = detail.IsActive;

                    if (detail.GoldLoanFreshLeadKycDocument != null)
                    {
                        leadDetail.KycDocument = detail.GoldLoanFreshLeadKycDocument.Where(x => x.GlfreshLeadId == detail.Id).Select(x => new GoldLoanFreshLeadKycDocumentViewModel
                        {
                            KycDocumentTypeId = x.KycDocumentTypeId,
                            KycDocumentTypeName = x.KycDocumentType.DocumentName,
                            DocumentNumber = x.DocumentNumber,
                            PanNumber = x.PanNumber,
                            AddressLine1 = x.AddressLine1,
                            PincodeAreaId = x.PincodeAreaId,
                            PincodeAreaName = x.PincodeArea.AreaName,
                            DistrictName = x.PincodeArea.District.Name,
                            StateName = x.PincodeArea.District.State.Name,
                            Pincode = x.PincodeArea.Pincode
                        }).FirstOrDefault();

                    }
                    if (detail.GoldLoanFreshLeadJewelleryDetail != null)
                    {
                        leadDetail.JewelleryDetail = detail.GoldLoanFreshLeadJewelleryDetail.Where(x => x.GlfreshLeadId == detail.Id).Select(x => new GoldLoanFreshLeadJewelleryDetailViewModel
                        {
                            Id = x.Id,
                            PreferredLoanTenure = x.PreferredLoanTenure,
                            JewelleryTypeId = x.JewelleryTypeId,
                            JewelleryTypeName = _db.JewellaryType.FirstOrDefault(y => y.Id == x.JewelleryTypeId).Name,
                            Quantity = x.Quantity,
                            Weight = x.Weight,
                            Karat = x.Karat
                        }).FirstOrDefault();
                    }
                    if (detail.GoldLoanFreshLeadAppointmentDetail != null)
                    {
                        leadDetail.AppointmentDetail = detail.GoldLoanFreshLeadAppointmentDetail.Where(x => x.GlfreshLeadId == detail.Id).Select(x => new GoldLoanFreshLeadAppointmentDetailViewModel
                        {
                            BankId= x.BankId,
                            BranchId=x.BranchId,
                            AppointmentDate = x.AppointmentDate,
                            AppointmentTime = new DateTime(x.AppointmentTime.Value.Ticks).ToString("HH:mm:ss") ,
                            BankName = x.Bank.Name,
                            BranchName = x.Branch.BranchName,
                            IFSC = x.Branch.Ifsc,
                            Pincode = x.Branch.Pincode,
                            Id = x.Id

                        }).FirstOrDefault();
                    }


                    //leadDetail.AppointmentDetail.AppointmentDate = detail.GoldLoanFreshLeadAppointmentDetail.Where(x => x.GlfreshLeadId == detail.Id).FirstOrDefault().AppointmentDate;
                    //leadDetail.AppointmentDetail.AppointmentTime = detail.GoldLoanFreshLeadAppointmentDetail.Where(x => x.GlfreshLeadId == detail.Id).FirstOrDefault().AppointmentTime.ToString();
                    //leadDetail.AppointmentDetail.BankName = detail.GoldLoanFreshLeadAppointmentDetail.Where(x => x.GlfreshLeadId == detail.Id).FirstOrDefault().Bank.Name;
                    //leadDetail.AppointmentDetail.BranchName = detail.GoldLoanFreshLeadAppointmentDetail.Where(x => x.GlfreshLeadId == detail.Id).FirstOrDefault().Bank.BankBranchMaster.Where(y => y.BankId == y.BankId).FirstOrDefault().BranchName;
                    //leadDetail.AppointmentDetail.IFSC = detail.GoldLoanFreshLeadAppointmentDetail.Where(x => x.GlfreshLeadId == detail.Id).FirstOrDefault().Bank.BankBranchMaster.Where(y => y.BankId == y.BankId).FirstOrDefault().Ifsc;
                    //leadDetail.AppointmentDetail.Pincode = detail.GoldLoanFreshLeadAppointmentDetail.Where(x => x.GlfreshLeadId == detail.Id).FirstOrDefault().Bank.BankBranchMaster.Where(y => y.BankId == y.BankId).FirstOrDefault().Pincode;
                    //leadDetail.AppointmentDetail.Id = detail.GoldLoanFreshLeadAppointmentDetail.Where(x => x.GlfreshLeadId == detail.Id).FirstOrDefault().Id;
                    return CreateResponse<GoldLoanFreshLeadViewModel>(leadDetail, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));

                }
                else
                {
                    return CreateResponse<GoldLoanFreshLeadViewModel>(leadDetail, ResponseMessage.NotFound, false, ((int)ApiStatusCode.NotFound));
                }

            }
            catch (Exception ex)
            {
                return CreateResponse<GoldLoanFreshLeadViewModel>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        #endregion


        #region <<Personal Loan , Home Loan , Vehicel Loan Fresh Lead>>
        public async Task<ApiServiceResponseModel<List<FreshLeadHLPLCLModel>>> FreshLeadHLPLCLList(IndexModel model)
        {
            ApiServiceResponseModel<List<FreshLeadHLPLCLModel>> objResponse = new ApiServiceResponseModel<List<FreshLeadHLPLCLModel>>();
            try
            {
                IQueryable<FreshLeadHlplcl> result;
                if (model.UserId == null)
                {
                    result = (from Lead in _db.FreshLeadHlplcl
                              join product in _db.Product on Lead.ProductId equals product.Id
                              join ProductCategory in _db.ProductCategory on product.ProductCategoryId equals ProductCategory.Id
                              join leadScoureUser in _db.UserMaster on Lead.LeadSourceByUserId equals leadScoureUser.Id
                              where !Lead.IsDelete && (string.IsNullOrEmpty(model.Search) || Lead.LeadType == (FreshLeadType.Salaried.GetStringValue().Equals(model.Search) || FreshLeadType.NonSalaried.GetStringValue().Equals(model.Search) ? (FreshLeadType.Salaried.GetStringValue().Equals(model.Search) ? false : true) : Lead.LeadType) || Lead.FullName.Contains(model.Search) || Lead.FatherName.Contains(model.Search) || product.Name.Contains(model.Search) || ProductCategory.Name.Contains(model.Search))
                              select Lead);
                }
                else
                {
                    result = (from Lead in _db.FreshLeadHlplcl
                              where !Lead.IsDelete && (string.IsNullOrEmpty(model.Search) || Lead.FullName.Contains(model.Search) || Lead.FatherName.Contains(model.Search))
                              select Lead);
                }

                switch (model.OrderBy)
                {
                    case "FullName":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.FullName ascending select orderData) : (from orderData in result orderby orderData.FullName descending select orderData);
                        break;
                    case "FatherName":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.FatherName ascending select orderData) : (from orderData in result orderby orderData.FatherName descending select orderData);
                        break;
                    case "MobileNumber":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.MobileNumber ascending select orderData) : (from orderData in result orderby orderData.MobileNumber descending select orderData);
                        break;
                    case "ProductName":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.Product.Name ascending select orderData) : (from orderData in result orderby orderData.MobileNumber descending select orderData);
                        break;
                    case "ProductCategory":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.Product.ProductCategory.Name ascending select orderData) : (from orderData in result orderby orderData.MobileNumber descending select orderData);
                        break;
                    default:
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.CreatedDate ascending select orderData) : (from orderData in result orderby orderData.CreatedDate descending select orderData);
                        break;
                }

                var data = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);

                objResponse.Data = await (from detail in data
                                          where detail.IsDelete == false
                                          select new FreshLeadHLPLCLModel
                                          {
                                              Id = detail.Id,
                                              FullName = detail.FullName ?? null,
                                              FatherName = detail.FatherName,
                                              AnnualIncome = detail.AnnualIncome,
                                              MobileNumber = detail.MobileNumber,
                                              EmployeeType = detail.EmployeeType,
                                              LoanAmount = detail.LoanAmount,
                                              LeadType = detail.LeadType,
                                              ProductCategoryName = detail.Product.ProductCategory.Name,
                                              IsActive = detail.IsActive.Value,
                                              ProductName = detail.Product.Name,
                                              LeadSourceByUserName = detail.LeadSourceByUser.UserName
                                              //Pincode = detail.GoldLoanFreshLeadKycDocument.FirstOrDefault().PincodeArea.Pincode,
                                          }).ToListAsync();
                if (result != null)
                {
                    return CreateResponse(objResponse.Data, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: result.Count());
                }
                else
                {
                    return CreateResponse<List<FreshLeadHLPLCLModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<List<FreshLeadHLPLCLModel>>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        public async Task<ApiServiceResponseModel<string>> SaveFreshLeadHLCLPLAsync(FreshLeadHLPLCLModel model)
        {
            ApiServiceResponseModel<string> objResponse = new ApiServiceResponseModel<string>();
            try
            {
                if (model != null)
                {
                    await _db.Database.BeginTransactionAsync();
                    //  var customerUserId = await SaveCustomerHLPLCL(model);
                    // model.CustomerUserId = customerUserId;
                    var result = await SavePLHLCLFreshLead(model);
                    if (result.status)
                    {
                        _db.Database.CommitTransaction();
                        return CreateResponse<string>("", result.Msg, true, ((int)ApiStatusCode.Ok)); ;
                    }
                    else
                    {
                        _db.Database.RollbackTransaction();
                        return CreateResponse<string>("", result.Msg, true, ((int)ApiStatusCode.Ok)); ;
                    }
                }
                else
                {
                    return CreateResponse<string>("", ResponseMessage.InvalidData, false, ((int)ApiStatusCode.InvaildModel));
                }
            }
            catch (Exception ex)
            {
                _db.Database.RollbackTransaction();
                return CreateResponse<string>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }

        #endregion

        #region  <<Private Method Of Fresh Gold Loan Lead>>
        private async Task<long> SaveCustomerHLPLCL(FreshLeadHLPLCLModel model)
        {
            try
            {
                var isExist = await _db.UserMaster.Where(x => x.Email == model.EmailId || x.Mobile == model.MobileNumber && x.IsDelete == false).FirstOrDefaultAsync();
                if (isExist != null)
                {
                    return isExist.Id;
                }
                Random random = new Random();
                UserMaster user = new UserMaster
                {
                    Email = model.EmailId,
                    Mobile = model.MobileNumber,
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
                    DateOfBirth = DateTime.Now,
                    UserId = customerUser.Entity.Id,
                    Gender = "Male",
                    IsActive = model.IsActive,
                    IsDelete = false,
                    PincodeAreaId = null,
                    Address = "",
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
        private async Task<long> SaveCustomerFreshLead(GoldLoanFreshLeadModel model)
        {
            try
            {
                var isExist = await _db.UserMaster.Where(x => x.Email == model.Email || x.Mobile == model.PrimaryMobileNumber && x.IsDelete == false).FirstOrDefaultAsync();
                if (isExist != null)
                {
                    return isExist.Id;
                }
                Random random = new Random();
                UserMaster user = new UserMaster
                {
                    Email = model.Email,
                    Mobile = model.PrimaryMobileNumber,
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
                    IsActive = model.IsActive,
                    IsDelete = false,
                    PincodeAreaId = model.KycDocument.PincodeAreaId,
                    Address = model.KycDocument.AddressLine1,
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
        private async Task<long> SaveBasicDetailGoldFreshLead(GoldLoanFreshLeadModel model, long customerUserId)
        {
            try
            {
                if (model.Id == default || model.Id == 0)
                {
                    GoldLoanFreshLead lead = new GoldLoanFreshLead
                    {
                        IsActive = model.IsActive,
                        IsDelete = false,
                        CreatedDate = DateTime.Now,
                        FullName = model.FullName,
                        Email = model.Email,
                        CustomerUserId = customerUserId,
                        FatherName = model.FatherName,
                        LeadSourceByUserId = model.LeadSourceByUserId,
                        Purpose = model.Purpose,
                        ProductId = model.ProductId,
                        SecondaryMobileNumber = model.SecondaryMobileNumber,
                        PrimaryMobileNumber = model.PrimaryMobileNumber,
                        Gender = model.Gender,
                        LoanAmountRequired = model.LoanAmountRequired,
                        DateOfBirth = model.DateOfBirth,
                        ModifedDate = null
                    };
                    await _db.GoldLoanFreshLead.AddAsync(lead);
                    await _db.SaveChangesAsync();
                    return lead.Id;
                }
                else
                {
                    var lead = await _db.GoldLoanFreshLead.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                    if (lead != null)
                    {
                        lead.FullName = model.FullName;
                        lead.FatherName = model.FatherName;
                        lead.DateOfBirth = model.DateOfBirth;
                        lead.Gender = model.Gender;
                        lead.Purpose = model.Purpose;
                        lead.ModifedDate = DateTime.Now;
                        lead.SecondaryMobileNumber = model.SecondaryMobileNumber;
                        lead.LeadSourceByUserId = model.LeadSourceByUserId;
                        await _db.SaveChangesAsync();
                        return lead.Id;
                    }
                    return 0;
                }
            }
            catch
            {
                throw;
            }

        }
        private async Task<ResposeData> SavePLHLCLFreshLead(FreshLeadHLPLCLModel model)
        {
            ResposeData returnObject = new ResposeData();
            try
            {
                if (model.Id == default || model.Id == 0)
                {
                    var isExist = await _db.FreshLeadHlplcl.Where(x => x.ProductId == model.ProductId && x.MobileNumber == model.MobileNumber).FirstOrDefaultAsync();
                    if (isExist == null)
                    {
                        FreshLeadHlplcl lead = new FreshLeadHlplcl
                        {
                            IsActive = model.IsActive,
                            IsDelete = false,
                            CreatedDate = DateTime.Now,
                            FullName = model.FullName,
                            FatherName = model.FatherName,
                            LeadSourceByUserId = model.LeadSourceByUserId,
                            //  CustomerUserId = model.CustomerUserId,
                            ProductId = model.ProductId,
                            LeadType = model.LeadType,
                            AnnualIncome = model.AnnualIncome,
                            EmailId = model.EmailId,
                            EmployeeType = model.EmployeeType,
                            NoOfItr = model.NoOfItr,
                            MobileNumber = model.MobileNumber,
                            LoanAmount = model.LoanAmount,
                            ModifiedDate = null
                        };
                        await _db.FreshLeadHlplcl.AddAsync(lead);
                        await _db.SaveChangesAsync();
                        returnObject.status = true;
                        returnObject.Msg = "Saved Record";
                        return returnObject;
                    }
                    else
                    {
                        returnObject.status = true;
                        returnObject.Msg = "Already Exist";
                        return returnObject;
                    }
                }
                else
                {
                    var lead = await _db.FreshLeadHlplcl.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                    if (lead != null)
                    {
                        lead.FullName = model.FullName;
                        lead.FatherName = model.FatherName;
                        lead.LeadSourceByUserId = model.LeadSourceByUserId;
                        lead.LoanAmount = model.LoanAmount;
                        lead.MobileNumber = model.MobileNumber;
                        lead.ModifiedDate = DateTime.Now;
                        lead.LeadType = model.LeadType;
                        lead.NoOfItr = model.NoOfItr;
                        lead.ProductId = model.ProductId;
                        lead.EmployeeType = model.EmployeeType;
                        lead.EmailId = model.EmailId;
                        lead.EmployeeType = model.EmployeeType;
                        lead.IsActive = model.IsActive;
                        await _db.SaveChangesAsync();
                        returnObject.status = true;
                        returnObject.Msg = "Update Record";
                        return returnObject;
                    }
                    returnObject.status = false;
                    returnObject.Msg = "Record Not Exist";
                    return returnObject;
                }
            }
            catch
            {
                throw;
            }

        }
        private async Task<bool> SaveJewelleryDetail(GoldLoanFreshLeadJewelleryDetailModel model, long freshLeadId)
        {
            try
            {
                if (model.Id == default || model.Id == 0)
                {
                    GoldLoanFreshLeadJewelleryDetail jewellery = new GoldLoanFreshLeadJewelleryDetail
                    {
                        IsActive = true,
                        IsDelete = false,
                        CreatedDate = DateTime.Now,
                        GlfreshLeadId = freshLeadId,
                        JewelleryTypeId = model.JewelleryTypeId,
                        Karat = model.Karat,
                        Quantity = model.Quantity,
                        Weight = model.Weight,
                        PreferredLoanTenure = model.PreferredLoanTenure,
                        ModifiedDate = null
                    };
                    await _db.GoldLoanFreshLeadJewelleryDetail.AddAsync(jewellery);
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    var detail = await _db.GoldLoanFreshLeadJewelleryDetail.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                    if (detail == null)
                    {
                        return false;
                    }
                    detail.ModifiedDate = DateTime.Now;
                    detail.Quantity = model.Quantity;
                    detail.Weight = model.Weight;
                    detail.PreferredLoanTenure = model.PreferredLoanTenure;
                    detail.JewelleryTypeId = model.JewelleryTypeId;
                    detail.Karat = model.Karat;
                    await _db.SaveChangesAsync();
                }
                return true;
            }
            catch { throw; }
        }
        private async Task<bool> SaveAppointmentDetail(GoldLoanFreshLeadAppointmentDetailModel model, long freshLeadId)
        {
            try
            {
                if (model.Id == default || model.Id == 0)
                {
                    GoldLoanFreshLeadAppointmentDetail appointment = new GoldLoanFreshLeadAppointmentDetail
                    {
                        AppointmentDate = model.AppointmentDate,
                        AppointmentTime = model.AppointmentTime.ToTimeSpanValue(),
                        BankId = model.BankId,
                        BranchId = model.BranchId,
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        IsDelete = false,
                        GlfreshLeadId = freshLeadId,
                    };
                    await _db.GoldLoanFreshLeadAppointmentDetail.AddAsync(appointment);
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    var detail = await _db.GoldLoanFreshLeadAppointmentDetail.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                    detail.AppointmentDate = model.AppointmentDate;
                    detail.AppointmentTime = model.AppointmentTime.ToTimeSpanValue();
                    detail.BankId = model.BankId;
                    detail.BranchId = model.BranchId;
                    await _db.SaveChangesAsync();
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }
        private async Task<bool> SaveKycDocumentDetail(GoldLoanFreshLeadKycDocumentModel model, long freshLeadId)
        {
            try
            {
                if (model.Id == default || model.Id == 0)
                {
                    GoldLoanFreshLeadKycDocument document = new GoldLoanFreshLeadKycDocument
                    {
                        GlfreshLeadId = freshLeadId,
                        IsActive = true,
                        IsDelete = false,
                        CreatedDate = DateTime.Now,
                        AddressLine1 = model.AddressLine1,
                        AddressLine2 = model.AddressLine2,
                        PanNumber = model.PanNumber,
                        KycDocumentTypeId = model.KycDocumentTypeId,
                        PincodeAreaId = model.PincodeAreaId,
                        DocumentNumber = model.DocumentNumber
                    };
                    await _db.GoldLoanFreshLeadKycDocument.AddAsync(document);
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    var detail = await _db.GoldLoanFreshLeadKycDocument.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                    detail.DocumentNumber = model.DocumentNumber;
                    detail.PanNumber = model.PanNumber;
                    detail.AddressLine1 = model.AddressLine1;
                    detail.PincodeAreaId = model.PincodeAreaId;
                    detail.KycDocumentTypeId = model.KycDocumentTypeId;
                    detail.AddressLine2 = model.AddressLine2;
                    await _db.SaveChangesAsync();
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }
        private class ResposeData
        {
            public bool status { get; set; }
            public string Msg { get; set; }
        }
        #endregion
    }
}
