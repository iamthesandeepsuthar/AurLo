﻿using AurigainLoanERP.Data.Database;
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
using System.Text;
using System.Threading.Tasks;
using static AurigainLoanERP.Shared.Enums.FixedValueEnums;

namespace AurigainLoanERP.Services.User
{
    public class UserService : BaseService, IUserService
    {
        public readonly IMapper _mapper;
        private AurigainContext _db;
        private readonly Security _security;
        private readonly FileHelper _fileHelper;
        public UserService(IMapper mapper, AurigainContext db, IConfiguration _configuration, IHostingEnvironment environment)
        {
            this._mapper = mapper;
            _db = db;
            _security = new Security(_configuration);
            _fileHelper = new FileHelper(environment);

        }

        #region << Agent User >>

        /// <summary>
        /// Save AgentUser Detail
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiServiceResponseModel<string>> AddUpdateAgentAsync(AgentPostModel model)
        {
            try
            {

                await _db.Database.BeginTransactionAsync();
                if (model != null)
                {

                    long userId = 0;
                    if (model.User != null)
                    {
                        model.User.UserRoleId = (int)UserRoleEnum.Agent;
                        userId = await SaveUserAsync(model.User);
                    }

                    if (userId > 0)
                    {
                        await SaveAgentAsync(model, userId);
                        if (model.BankDetails != null)
                        {
                            await SaveUserBankAsync(model.BankDetails, userId);
                        }
                        if (model.ReportingPerson != null && model.ReportingPerson.ReportingUserId > 0)
                        {
                            await SaveUserReportingPersonAsync(model.ReportingPerson, userId);
                        }
                        if (model.Documents != null)
                        {
                            await SaveUserDocumentAsync(model.Documents, userId);
                        }
                        if (model.UserKYC != null)
                        {
                            await SaveUserKYCAsync(model.UserKYC, userId);
                        }
                        if (model.UserNominee != null)
                        {
                            await SaveUserNomineeAsync(model.UserNominee, userId);
                        }
                        _db.Database.CommitTransaction();
                        return CreateResponse<string>(userId.ToString(), ResponseMessage.Save, true, ((int)ApiStatusCode.Ok));
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

        /// <summary>
        /// Get Agent
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiServiceResponseModel<List<AgentListViewModel>>> GetAgentAsync(IndexModel model)
        {

            ApiServiceResponseModel<List<AgentListViewModel>> objResponse = new ApiServiceResponseModel<List<AgentListViewModel>>();
            try
            {
                var result = (from agent in _db.UserAgent
                                  //join user in _db.UserMaster on agent.UserId equals user.Id
                                  //join role in _db.UserRole on user.UserRoleId equals role.Id
                              where !agent.User.IsDelete && (string.IsNullOrEmpty(model.Search) || agent.FullName.Contains(model.Search) || agent.User.Email.Contains(model.Search) || agent.User.UserName.Contains(model.Search))
                              select agent);
                switch (model.OrderBy)
                {
                    case "FullName":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.FullName ascending select orderData) : (from orderData in result orderby orderData.FullName descending select orderData);
                        break;
                    case "Mobile":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.User.Mobile ascending select orderData) : (from orderData in result orderby orderData.User.Mobile descending select orderData);
                        break;
                    case "Email":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.User.Email ascending select orderData) : (from orderData in result orderby orderData.User.Email descending select orderData);
                        break;

                    case "PinCode":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.PinCode ascending select orderData) : (from orderData in result orderby orderData.PinCode descending select orderData);
                        break;

                    case "IsActive":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.User.IsActive ascending select orderData) : (from orderData in result orderby orderData.User.IsActive descending select orderData);
                        break;
                    default:
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.User.UserRole.Name ascending select orderData) : (from orderData in result orderby orderData.User.UserRole.Name descending select orderData);
                        break;
                }

                var data = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);

                objResponse.Data = await (from detail in data
                                          where detail.IsDelete == false
                                          select new AgentListViewModel
                                          {
                                              Id = detail.Id,
                                              UserId = detail.User != null ? detail.User.Id : default,
                                              FullName = detail.FullName ?? null,
                                              Email = detail.User != null ? detail.User.Email : null,
                                              Mobile = detail.User != null ? detail.User.Mobile : null,
                                              Role = detail.User != null && detail.User.UserRole != null ? detail.User.UserRole.Name : null,
                                              UniqueId = detail.UniqueId ?? null,
                                              Gender = detail.Gender ?? null,
                                              QualificationName = detail.Qualification.Name ?? null,
                                              Address = detail.Address ?? null,
                                              DistrictName = detail.District != null ? detail.District.Name : null,
                                              StateName = detail.District != null && detail.District.State != null ? detail.District.State.Name : null,
                                              PinCode = detail.PinCode ?? null,
                                              DateOfBirth = detail.DateOfBirth ?? null,
                                              ProfilePictureUrl = detail.User.ProfilePath.ToAbsolutePath() ?? null,
                                              IsApproved = detail.User.IsApproved,
                                              IsActive = detail.User.IsActive,
                                              Mpin = detail.User.Mpin,
                                              IsDelete = detail.IsDelete,
                                              CreatedOn = detail.CreatedOn,
                                              CreatedBy = detail.CreatedBy
                                          }).ToListAsync();
                if (result != null)
                {
                    return CreateResponse(objResponse.Data, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: result.Count());
                }
                else
                {
                    return CreateResponse<List<AgentListViewModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
                }

            }
            catch (Exception ex)
            {
                return CreateResponse<List<AgentListViewModel>>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }
        /// <summary>
        /// Get Agent Detail 
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns></returns>
        public async Task<ApiServiceResponseModel<AgentViewModel>> GetAgentDetailAsync(long id)
        {

            try
            {

                UserAgent objAgent = await _db.UserAgent.Where(x => x.UserId == id)
                    .Include(x => x.District).Include(x => x.District.State)
                    .Include(x => x.User).Include(x => x.User.UserRole)
                    .Include(x => x.Qualification)
                    .Include(x => x.User.UserNominee)
                    .Include(x => x.User.UserKyc)
                    .Include(x => x.User.UserBank)
                    .Include(x => x.User.UserDocument)
                    .Include(x => x.User.UserRole).Include(x => x.User.UserReportingPersonUser).FirstOrDefaultAsync();
                if (objAgent != null)
                {
                    AgentViewModel objAgentModel = _mapper.Map<AgentViewModel>(objAgent);

                    if (objAgent.User != null)
                    {
                        if (objAgent.User != null)
                        {
                            objAgentModel.User = _mapper.Map<UserViewModel>(objAgent.User ?? null);
                            objAgentModel.User.MPin = null;
                            objAgentModel.User.ProfilePath = !string.IsNullOrEmpty(objAgent.User.ProfilePath) ? objAgent.User.ProfilePath.ToAbsolutePath() : null;


                        }

                        if (objAgent.User.UserBank != null)
                        {
                            objAgentModel.BankDetails = _mapper.Map<UserBankDetailViewModel>(objAgent.User.UserBank.FirstOrDefault() ?? null);

                        }
                        if (objAgent.User.UserReportingPersonUser != null)
                        {
                            objAgentModel.ReportingPerson = _mapper.Map<UserReportingPersonViewModel>(objAgent.User.UserReportingPersonUser.FirstOrDefault() ?? null);

                        }
                        if (objAgent.User.UserKyc != null)
                        {

                            objAgentModel.UserKYC = objAgent.User.UserKyc.Select(x => new UserKycViewModel
                            {

                                Id = x.Id,
                                Kycnumber = x.Kycnumber ?? null,
                                KycdocumentTypeId = x.KycdocumentTypeId,
                                KycdocumentTypeName = _db.DocumentType.FirstOrDefault(z => z.Id == x.KycdocumentTypeId).DocumentName ?? null,
                                IsActive = x.IsActive ?? null,
                                IsDelete = x.IsDelete,
                                CreatedOn = x.CreatedOn,
                                ModifiedOn = x.ModifiedOn ?? null,
                                CreatedBy = x.CreatedBy ?? null,
                                ModifiedBy = x.ModifiedBy ?? null,

                            }).ToList();

                        }
                        if (objAgent.User.UserDocument != null)
                        {
                            foreach (var item in objAgent.User.UserDocument)
                            {
                                //  objDoorStepAgentModel.Documents.Add(_mapper.Map<UserDocumentViewModel>(item.value));
                                objAgentModel.Documents.Add(new UserDocumentViewModel
                                {
                                    Id = item.Id,
                                    DocumentTypeId = item.DocumentTypeId,
                                    DocumentTypeName = _db.DocumentType.FirstOrDefault(z => z.Id == item.DocumentTypeId).DocumentName ?? null,
                                    UserId = item.UserId,
                                    IsActive = item.IsActive ?? null,
                                    IsDelete = item.IsDelete,
                                    CreatedOn = item.CreatedOn,
                                    ModifiedOn = item.ModifiedOn ?? null,
                                    CreatedBy = item.CreatedBy ?? null,
                                    ModifiedBy = item.ModifiedBy ?? null,
                                    UserDocumentFiles = _db.UserDocumentFiles.Where(x => x.DocumentId == item.Id).Select(fileitem => new UserDocumentFilesViewModel
                                    {
                                        Id = fileitem.Id,
                                        DocumentId = fileitem.DocumentId,
                                        FileName = fileitem.FileName,
                                        FileType = fileitem.FileType,
                                        Path = fileitem.Path.ToAbsolutePath()
                                    }).ToList() ?? null
                                });

                            }

                        }
                        if (objAgent.User.UserNominee != null)
                        {
                            objAgentModel.UserNominee = _mapper.Map<UserNomineeViewModel>(objAgent.User.UserNominee.FirstOrDefault() ?? null);

                        }

                        objAgentModel.User.UserRoleName = objAgent.User.UserRole.Name;
                        objAgentModel.DistrictName = objAgent.District.Name;
                        objAgentModel.StateName = objAgent.District.State.Name;
                        objAgentModel.StateId = objAgent.District.StateId;
                        objAgentModel.QualificationName = objAgent.Qualification.Name;
                    }
                    return CreateResponse(objAgentModel, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<AgentViewModel>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<AgentViewModel>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }

        }

        #endregion

        #region << Door-step Agent >>

        /// <summary>
        /// Get Door Step Agent Async
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiServiceResponseModel<List<DoorStepAgentListModel>>> GetDoorStepAgentAsync(IndexModel model)
        {

            ApiServiceResponseModel<List<DoorStepAgentListModel>> objResponse = new ApiServiceResponseModel<List<DoorStepAgentListModel>>();
            try
            {
                var result = (from agent in _db.UserDoorStepAgent
                                  //join user in _db.UserMaster on agent.UserId equals user.Id
                                  //join role in _db.UserRole on user.UserRoleId equals role.Id
                              where !agent.IsDelete && (string.IsNullOrEmpty(model.Search) || agent.FullName.Contains(model.Search) || agent.User.Email.Contains(model.Search) || agent.User.UserName.Contains(model.Search))
                              select agent);
                switch (model.OrderBy)
                {
                    case "FullName":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.FullName ascending select orderData) : (from orderData in result orderby orderData.FullName descending select orderData);
                        break;
                    case "Mobile":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.User.Mobile ascending select orderData) : (from orderData in result orderby orderData.User.Mobile descending select orderData);
                        break;
                    case "Email":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.User.Email ascending select orderData) : (from orderData in result orderby orderData.User.Email descending select orderData);
                        break;

                    case "PinCode":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.PinCode ascending select orderData) : (from orderData in result orderby orderData.PinCode descending select orderData);
                        break;

                    case "IsActive":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.User.IsActive ascending select orderData) : (from orderData in result orderby orderData.User.IsActive descending select orderData);
                        break;
                    default:
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.User.UserRole.Name ascending select orderData) : (from orderData in result orderby orderData.User.UserRole.Name descending select orderData);
                        break;
                }

                var data = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);
                objResponse.Data = await (from detail in data
                                          select new DoorStepAgentListModel
                                          {
                                              Id = detail.Id,
                                              UserId = detail.User != null ? detail.User.Id : default,
                                              FullName = detail.FullName ?? null,
                                              Email = detail.User != null ? detail.User.Email : null,
                                              Mobile = detail.User != null ? detail.User.Mobile : null,
                                              RoleId = detail.User != null ? detail.User.UserRoleId : default,
                                              FatherName = detail.FatherName ?? null,
                                              Role = detail.User != null && detail.User.UserRole != null ? detail.User.UserRole.Name : null,
                                              UniqueId = detail.UniqueId ?? null,
                                              Gender = detail.Gender ?? null,
                                              QualificationName = detail.Qualification.Name ?? null,
                                              Address = detail.Address ?? null,
                                              DistrictName = detail.District != null ? detail.District.Name : null,
                                              StateName = detail.District != null && detail.District.State != null ? detail.District.State.Name : null,
                                              PinCode = detail.PinCode ?? null,
                                              DateOfBirth = detail.DateOfBirth ?? null,
                                              ProfilePictureUrl = detail.User.ProfilePath.ToAbsolutePath() ?? null,
                                              IsApproved = detail.User.IsApproved,
                                              IsActive = detail.User.IsActive,
                                              IsDelete = detail.IsDelete,
                                              CreatedOn = detail.CreatedOn,
                                              CreatedBy = detail.CreatedBy
                                          }).ToListAsync();


                if (result != null)
                {
                    return CreateResponse(objResponse.Data, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: result.Count());
                }
                else
                {
                    return CreateResponse<List<DoorStepAgentListModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
                }

            }
            catch (Exception ex)
            {
                return CreateResponse<List<DoorStepAgentListModel>>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
            }
        }

        /// <summary>
        /// Add Update Door Step Agent 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiServiceResponseModel<string>> AddUpdateDoorStepAgentAsync(DoorStepAgentPostModel model)
        {
            try
            {
                if (model != null)
                {
                    await _db.Database.BeginTransactionAsync();

                    long userId = 0;
                    if (model.User != null)
                    {
                        model.User.UserRoleId = (int)UserRoleEnum.DoorStepAgent;
                        userId = await SaveUserAsync(model.User);

                    }
                    if (userId > 0)
                    {

                        await SaveDoorStepAgentAsync(model, userId);


                        if (model.BankDetails != null)
                        {
                            await SaveUserBankAsync(model.BankDetails, userId);

                        }

                        if (model.ReportingPerson != null && model.ReportingPerson.ReportingUserId > 0)
                        {
                            await SaveUserReportingPersonAsync(model.ReportingPerson, userId);
                        }
                        if (model.Documents != null)
                        {
                            await SaveUserDocumentAsync(model.Documents, userId);

                        }
                        if (model.UserKYC != null)
                        {
                            await SaveUserKYCAsync(model.UserKYC, userId);

                        }
                        if (model.UserNominee != null)
                        {
                            await SaveUserNomineeAsync(model.UserNominee, userId);

                        }
                        if (model.SecurityDeposit != null && !string.IsNullOrEmpty(model.SecurityDeposit.AccountNumber))
                        {
                            await SaveUserSecurityDepositAsync(model.SecurityDeposit, userId);

                        }


                        _db.Database.CommitTransaction();
                        return CreateResponse<string>(userId.ToString(), ResponseMessage.Save, true, ((int)ApiStatusCode.Ok));
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
        /// <summary>
        /// Get Door Step Agent Detail
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns></returns>
        public async Task<ApiServiceResponseModel<DoorStepAgentViewModel>> GetDoorStepAgentDetailAsync(long id)
        {

            try
            {

                UserDoorStepAgent objDoorStepAgent = await _db.UserDoorStepAgent.Where(x => x.UserId == id)
                    .Include(x => x.District).Include(x => x.District.State)
                    .Include(x => x.User).Include(x => x.User.UserRole)
                    .Include(x => x.Qualification)
                    .Include(x => x.User.UserNominee)
                    .Include(x => x.User.UserKyc)
                    .Include(x => x.User.UserBank)
                    .Include(x => x.User.UserDocument)
                    .Include(x => x.User.UserSecurityDepositDetails)
                    .Include(x => x.User.UserRole).Include(x => x.User.UserReportingPersonUser).FirstOrDefaultAsync();
                if (objDoorStepAgent != null)
                {
                    DoorStepAgentViewModel objDoorStepAgentModel = _mapper.Map<DoorStepAgentViewModel>(objDoorStepAgent);

                    if (objDoorStepAgent.User != null)
                    {

                        if (objDoorStepAgent.User != null)
                        {
                            objDoorStepAgentModel.User = _mapper.Map<UserViewModel>(objDoorStepAgent.User ?? null);
                            objDoorStepAgentModel.User.UserRoleName = objDoorStepAgent.User.UserRole.Name;
                            objDoorStepAgentModel.User.ProfilePath = !string.IsNullOrEmpty(objDoorStepAgent.User.ProfilePath) ? objDoorStepAgent.User.ProfilePath.ToAbsolutePath() : null;

                        }

                        if (objDoorStepAgent.User.UserBank != null)
                        {
                            objDoorStepAgentModel.BankDetails = _mapper.Map<UserBankDetailViewModel>(objDoorStepAgent.User.UserBank.FirstOrDefault() ?? null);

                        }
                        if (objDoorStepAgent.User.UserReportingPersonUser != null)
                        {
                            objDoorStepAgentModel.ReportingPerson = _mapper.Map<UserReportingPersonViewModel>(objDoorStepAgent.User.UserReportingPersonUser.FirstOrDefault() ?? null);

                        }
                        if (objDoorStepAgent.User.UserKyc != null && objDoorStepAgent.User.UserKyc.Count > 0)
                        {


                            objDoorStepAgentModel.UserKYC = objDoorStepAgent.User.UserKyc.Select(x => new UserKycViewModel
                            {

                                Id = x.Id,
                                Kycnumber = x.Kycnumber ?? null,
                                KycdocumentTypeId = x.KycdocumentTypeId,
                                KycdocumentTypeName = _db.DocumentType.FirstOrDefault(z => z.Id == x.KycdocumentTypeId).DocumentName ?? null,
                                IsActive = x.IsActive ?? null,
                                IsDelete = x.IsDelete,
                                CreatedOn = x.CreatedOn,
                                ModifiedOn = x.ModifiedOn ?? null,
                                CreatedBy = x.CreatedBy ?? null,
                                ModifiedBy = x.ModifiedBy ?? null,

                            }).ToList();
                        }
                        if (objDoorStepAgent.User.UserDocument != null)
                        {
                            foreach (var item in objDoorStepAgent.User.UserDocument)
                            {
                                //  objDoorStepAgentModel.Documents.Add(_mapper.Map<UserDocumentViewModel>(item.value));
                                objDoorStepAgentModel.Documents.Add(new UserDocumentViewModel
                                {
                                    Id = item.Id,
                                    DocumentTypeId = item.DocumentTypeId,
                                    DocumentTypeName = _db.DocumentType.FirstOrDefault(z => z.Id == item.DocumentTypeId).DocumentName ?? null,
                                    UserId = item.UserId,
                                    IsActive = item.IsActive ?? null,
                                    IsDelete = item.IsDelete,
                                    CreatedOn = item.CreatedOn,
                                    ModifiedOn = item.ModifiedOn ?? null,
                                    CreatedBy = item.CreatedBy ?? null,
                                    ModifiedBy = item.ModifiedBy ?? null,
                                    UserDocumentFiles = _db.UserDocumentFiles.Where(x => x.DocumentId == item.Id).Select(fileitem => new UserDocumentFilesViewModel
                                    {
                                        Id = fileitem.Id,
                                        DocumentId = fileitem.DocumentId,
                                        FileName = fileitem.FileName,
                                        FileType = fileitem.FileType,
                                        Path = fileitem.Path.ToAbsolutePath()
                                    }).ToList() ?? null
                                });

                            }
                        }
                        if (objDoorStepAgent.User.UserNominee != null)
                        {
                            objDoorStepAgentModel.UserNominee = _mapper.Map<UserNomineeViewModel>(objDoorStepAgent.User.UserNominee.FirstOrDefault() ?? null);

                        }
                        if (objDoorStepAgent.SecurityDeposit != null)
                        {
                            objDoorStepAgentModel.SecurityDeposit = _mapper.Map<UserSecurityDepositViewModel>(objDoorStepAgent.User.UserSecurityDepositDetails.FirstOrDefault() ?? null);

                        }

                        objDoorStepAgentModel.DistrictName = objDoorStepAgent.District.Name;
                        objDoorStepAgentModel.StateName = objDoorStepAgent.District.State.Name;
                        objDoorStepAgentModel.StateId = objDoorStepAgent.District.StateId;
                        objDoorStepAgentModel.QualificationName = objDoorStepAgent.Qualification.Name;
                        objDoorStepAgentModel.User.UserRoleName = objDoorStepAgent.User.UserRole.Name;


                    }
                    return CreateResponse(objDoorStepAgentModel, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<DoorStepAgentViewModel>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<DoorStepAgentViewModel>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }

        }
        #endregion

        #region << Manager User >>
        public async Task<ApiServiceResponseModel<string>> AddUpdateManagerAsync(UserManagerModel model)
        {
            try
            {
                await _db.Database.BeginTransactionAsync();
                if (model != null)
                {
                    var response = await SaveUserManagerAsync(model);
                    if (response)
                    {
                        _db.Database.CommitTransaction();
                        return CreateResponse<string>("", ResponseMessage.Save, true, ((int)ApiStatusCode.Ok));
                    }
                    _db.Database.RollbackTransaction();
                    return CreateResponse<string>(null, ResponseMessage.UserExist, false, ((int)ApiStatusCode.DataBaseTransactionFailed));
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

        public async Task<ApiServiceResponseModel<List<UserManagerModel>>> ManagersList(IndexModel model)
        {
            ApiServiceResponseModel<List<UserManagerModel>> objResponse = new ApiServiceResponseModel<List<UserManagerModel>>();

            return objResponse;
        }
        #endregion

        #region << Common Method >>

        public async Task<ApiServiceResponseModel<UserViewModel>> GetUserProfile(long id)
        {
            try
            {
                var user = await _db.UserMaster.Where(x => x.Id == id && !x.IsDelete)
                   .Include(x => x.UserAgent).Include(x => x.UserDoorStepAgent).Include(x => x.Managers)
                   .Include(x => x.UserRole).FirstOrDefaultAsync();
                if (user != null)
                {
                    UserViewModel objUser = _mapper.Map<UserViewModel>(user ?? null);
                    objUser.UserRoleName = user.UserRole.Name;
                    switch (user.UserRoleId)
                    {
                        case (int)UserRoleEnum.Agent:
                            objUser.FullName = user.UserAgent.FirstOrDefault().FullName;
                            break;
                        case (int)UserRoleEnum.DoorStepAgent:
                            objUser.FullName = user.UserDoorStepAgent.FirstOrDefault().FullName;
                            break;
                        case (int)UserRoleEnum.ZonalManager:
                            objUser.FullName = user.Managers.FirstOrDefault().FullName;
                            break;
                        case (int)UserRoleEnum.Supervisor:
                            objUser.FullName = user.UserName;
                            break;
                        default:
                            objUser.FullName = null;
                            break;
                    }

                    objUser.ProfilePath = !string.IsNullOrEmpty(user.ProfilePath) ? objUser.ProfilePath.ToAbsolutePath() : null;
                    return CreateResponse(objUser, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));

                }
                else
                {
                    return CreateResponse<UserViewModel>(null, ResponseMessage.Update, true, ((int)ApiStatusCode.RecordNotFound));

                }

            }
            catch (Exception)
            {

                return CreateResponse<UserViewModel>(null, ResponseMessage.Fail, true, ((int)ApiStatusCode.InternalServerError));

            }
        }

        public async Task<ApiServiceResponseModel<object>> UpateActiveStatus(long id)
        {
            try
            {
                await _db.Database.BeginTransactionAsync();
                var user = await _db.UserMaster.FirstOrDefaultAsync(X => X.Id == id);
                if (user != null)
                {
                    user.IsActive = !user.IsActive;

                    switch (user.UserRoleId)
                    {
                        case (int)UserRoleEnum.DoorStepAgent:
                            var doorStepUser = user.UserDoorStepAgent.Where(x => x.UserId == id).FirstOrDefault();

                            doorStepUser.IsActive = !doorStepUser.IsActive;
                            break;

                        case (int)UserRoleEnum.Agent:
                            var agentUser = user.UserAgent.Where(x => x.UserId == id).FirstOrDefault();

                            agentUser.IsActive = !agentUser.IsActive;
                            break;



                    }

                    await _db.SaveChangesAsync();

                }

                _db.Database.CommitTransaction();
                return CreateResponse(true as object, ResponseMessage.Update, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {
                _db.Database.RollbackTransaction();
                return CreateResponse(true as object, ResponseMessage.Fail, true, ((int)ApiStatusCode.ServerException), ex.Message);

            }
        }


        public async Task<ApiServiceResponseModel<object>> UpdateDeleteStatus(long id)
        {
            try
            {
                await _db.Database.BeginTransactionAsync();
                var user = await _db.UserMaster.FirstOrDefaultAsync(X => X.Id == id);

                if (user != null)
                {
                    user.IsDelete = !user.IsDelete;

                    switch (user.UserRoleId)
                    {
                        case (int)UserRoleEnum.DoorStepAgent:
                            var doorStepUser = user.UserDoorStepAgent.Where(x => x.UserId == id).FirstOrDefault();

                            doorStepUser.IsDelete = !doorStepUser.IsDelete;
                            break;

                        case (int)UserRoleEnum.Agent:
                            var agentUser = user.UserAgent.Where(x => x.UserId == id).FirstOrDefault();

                            agentUser.IsDelete = !agentUser.IsDelete;
                            break;



                    }



                    await _db.SaveChangesAsync();

                }

                _db.Database.CommitTransaction();
                return CreateResponse(true as object, ResponseMessage.Update, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception)
            {
                _db.Database.RollbackTransaction();
                return CreateResponse(true as object, ResponseMessage.Fail, true, ((int)ApiStatusCode.DataBaseTransactionFailed));

            }
        }
        public async Task<ApiServiceResponseModel<string>> UpdateProfile(UserSettingPostModel model)
        {
            try
            {
                if (model != null && !string.IsNullOrEmpty(model.ProfileBase64) && model.UserId > 0)
                {


                    await _db.Database.BeginTransactionAsync();
                    var user = await _db.UserMaster.FirstOrDefaultAsync(X => X.Id == model.UserId);
                    string savedFilePath = !string.IsNullOrEmpty(model.ProfileBase64) ? Path.Combine(FilePathConstant.UserProfile, _fileHelper.Save(model.ProfileBase64, FilePathConstant.UserProfile, model.FileName)) : null;
                    user.ProfilePath = savedFilePath;
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
        public async Task<ApiServiceResponseModel<string>> SetUserAvailibilty(UserAvailibilityPostModel model)
        {
            try
            {
                if (model != null && model.UserId > 0)
                {
                    await _db.Database.BeginTransactionAsync();
                    var dataUserAvailability = await _db.UserAvailability.FirstOrDefaultAsync(X => X.Id == model.Id && X.UserId == model.UserId && X.IsActive == true);

                    if (dataUserAvailability != null)
                    {


                        dataUserAvailability.MondaySt = !string.IsNullOrEmpty(model.MondayST) ? TimeSpan.Parse(model.MondayST) : default;
                        dataUserAvailability.MondayEt = !string.IsNullOrEmpty(model.MondayET) ? TimeSpan.Parse(model.MondayET) : default;
                        dataUserAvailability.TuesdaySt = !string.IsNullOrEmpty(model.TuesdayST) ? TimeSpan.Parse(model.TuesdayST) : default;
                        dataUserAvailability.TuesdayEt = !string.IsNullOrEmpty(model.TuesdayET) ? TimeSpan.Parse(model.TuesdayET) : default;
                        dataUserAvailability.WednesdaySt = !string.IsNullOrEmpty(model.WednesdayST) ? TimeSpan.Parse(model.WednesdayST) : default;
                        dataUserAvailability.WednesdayEt = !string.IsNullOrEmpty(model.WednesdayET) ? TimeSpan.Parse(model.WednesdayET) : default;
                        dataUserAvailability.ThursdaySt = !string.IsNullOrEmpty(model.ThursdayST) ? TimeSpan.Parse(model.ThursdayST) : default;
                        dataUserAvailability.ThursdayEt = !string.IsNullOrEmpty(model.ThursdayET) ? TimeSpan.Parse(model.ThursdayET) : default;
                        dataUserAvailability.FridaySt = !string.IsNullOrEmpty(model.FridayST) ? TimeSpan.Parse(model.FridayST) : default;
                        dataUserAvailability.FridayEt = !string.IsNullOrEmpty(model.FridayET) ? TimeSpan.Parse(model.FridayET) : default;
                        dataUserAvailability.SaturdaySt = !string.IsNullOrEmpty(model.SaturdayST) ? TimeSpan.Parse(model.SaturdayST) : default;
                        dataUserAvailability.SaturdayEt = !string.IsNullOrEmpty(model.SaturdayET) ? TimeSpan.Parse(model.SaturdayET) : default;
                        dataUserAvailability.SundaySt = !string.IsNullOrEmpty(model.SundayST) ? TimeSpan.Parse(model.SundayST) : default;
                        dataUserAvailability.SundayEt = !string.IsNullOrEmpty(model.SundayET) ? TimeSpan.Parse(model.SundayET) : default;
                        dataUserAvailability.Capacity = model.Capacity ?? null;
                        dataUserAvailability.PincodeAreaId = model.PincodeAreaId ?? null;

                    }
                    else
                    {
                        UserAvailability objUserAvailability = new UserAvailability();
                        objUserAvailability.UserId = model.UserId;
                        objUserAvailability.MondaySt = !string.IsNullOrEmpty(model.MondayST) ? TimeSpan.Parse(model.MondayST) : default;
                        objUserAvailability.MondayEt = !string.IsNullOrEmpty(model.MondayET) ? TimeSpan.Parse(model.MondayET) : default;
                        objUserAvailability.TuesdaySt = !string.IsNullOrEmpty(model.TuesdayST) ? TimeSpan.Parse(model.TuesdayST) : default;
                        objUserAvailability.TuesdayEt = !string.IsNullOrEmpty(model.TuesdayET) ? TimeSpan.Parse(model.TuesdayET) : default;
                        objUserAvailability.WednesdaySt = !string.IsNullOrEmpty(model.WednesdayST) ? TimeSpan.Parse(model.WednesdayST) : default;
                        objUserAvailability.WednesdayEt = !string.IsNullOrEmpty(model.WednesdayET) ? TimeSpan.Parse(model.WednesdayET) : default;
                        objUserAvailability.ThursdaySt = !string.IsNullOrEmpty(model.ThursdayST) ? TimeSpan.Parse(model.ThursdayST) : default;
                        objUserAvailability.ThursdayEt = !string.IsNullOrEmpty(model.ThursdayET) ? TimeSpan.Parse(model.ThursdayET) : default;
                        objUserAvailability.FridaySt = !string.IsNullOrEmpty(model.FridayST) ? TimeSpan.Parse(model.FridayST) : default;
                        objUserAvailability.FridayEt = !string.IsNullOrEmpty(model.FridayET) ? TimeSpan.Parse(model.FridayET) : default;
                        objUserAvailability.SaturdaySt = !string.IsNullOrEmpty(model.SaturdayST) ? TimeSpan.Parse(model.SaturdayST) : default;
                        objUserAvailability.SaturdayEt = !string.IsNullOrEmpty(model.SaturdayET) ? TimeSpan.Parse(model.SaturdayET) : default;
                        objUserAvailability.SundaySt = !string.IsNullOrEmpty(model.SundayST) ? TimeSpan.Parse(model.SundayST) : default;
                        objUserAvailability.SundayEt = !string.IsNullOrEmpty(model.SundayET) ? TimeSpan.Parse(model.SundayET) : default;
                        objUserAvailability.Capacity = model.Capacity ?? null;
                        objUserAvailability.PincodeAreaId = model.PincodeAreaId ?? null;

                        _db.UserAvailability.Add(objUserAvailability);
                    }

                    await _db.SaveChangesAsync();

                    _db.Database.CommitTransaction();
                    return CreateResponse<string>(null, ResponseMessage.Update, true, ((int)ApiStatusCode.Ok));
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
        public async Task<ApiServiceResponseModel<object>> UpdateApproveStatus(long id)
        {
            try
            {
                await _db.Database.BeginTransactionAsync();
                var user = await _db.UserMaster.FirstOrDefaultAsync(X => X.Id == id);
                user.IsApproved = !user.IsApproved;
                user.ModifiedOn = DateTime.Now;
                await _db.SaveChangesAsync();
                _db.Database.CommitTransaction();
                return CreateResponse(true as object, ResponseMessage.Update, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {
                _db.Database.RollbackTransaction();
                return CreateResponse(true as object, ResponseMessage.Fail, true, ((int)ApiStatusCode.ServerException), ex.Message);

            }
        }
        public async Task<ApiServiceResponseModel<object>> DeleteDocumentFile(long id, long documentId)
        {
            try
            {
                await _db.Database.BeginTransactionAsync();

                var objFileModel = await _db.UserDocumentFiles.FirstOrDefaultAsync(x => x.Id == id && x.DocumentId == documentId);

                if (objFileModel != null)
                {
                    _fileHelper.Delete(Path.Combine(objFileModel.Path, objFileModel.FileName));
                    _db.UserDocumentFiles.Remove(objFileModel);
                    await _db.SaveChangesAsync();
                }


                _db.Database.CommitTransaction();
                return CreateResponse(true as object, ResponseMessage.Update, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {
                _db.Database.RollbackTransaction();
                return CreateResponse(true as object, ResponseMessage.Fail, true, ((int)ApiStatusCode.DataBaseTransactionFailed), ex.Message ?? ex.InnerException.ToString());

            }
        }

        /// <summary>
        /// Get User Availibilty by User Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiServiceResponseModel<List<UserAvailabilityViewModel>>> GetUserAvailibilty(long id)
        {
            try
            {
                var records = await _db.UserAvailability.Where(x => x.UserId == id && !x.IsDelete).ToListAsync();

                if (records != null)
                {
                    List<UserAvailabilityViewModel> objArea = _mapper.Map<List<UserAvailabilityViewModel>>(records ?? null);
                    return CreateResponse(objArea, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<List<UserAvailabilityViewModel>>(null, ResponseMessage.Update, true, ((int)ApiStatusCode.RecordNotFound));
                }

            }
            catch (Exception)
            {

                return CreateResponse<List<UserAvailabilityViewModel>>(null, ResponseMessage.Fail, true, ((int)ApiStatusCode.InternalServerError));

            }
        }


        #endregion

        #region << Private Method>>

        /// <summary>
        /// Save User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<long> SaveUserAsync(UserPostModel model)
        {
            try
            {
                // check new user detail match with existing users
                var existingUser = await _db.UserMaster.FirstOrDefaultAsync(x => (model.Id == null || model.Id == 0 || x.Id != model.Id) && ((!string.IsNullOrEmpty(model.Mobile) && x.Mobile == model.Mobile) || (!string.IsNullOrEmpty(model.Email) && x.Email == model.Email) || (string.IsNullOrEmpty(model.UserName) || (!string.IsNullOrEmpty(model.UserName) && x.UserName == model.UserName))));

                if (existingUser == null)
                {
                    if (model.Id == null || model.Id < 1)
                    {
                        var objModel = _mapper.Map<UserMaster>(model);
                        objModel.UserName = model.UserName ?? model.Email;
                        objModel.CreatedOn = DateTime.Now;
                        objModel.Mpin = GenerateUniqueId();//_security.EncryptData(GenerateUniqueId());
                        objModel.IsWhatsApp = model.IsWhatsApp;
                        var result = await _db.UserMaster.AddAsync(objModel);
                        await _db.SaveChangesAsync();
                        model.Id = result.Entity.Id;
                    }
                    else
                    {
                        var objModel = await _db.UserMaster.FirstOrDefaultAsync(x => x.Id == model.Id);
                        objModel.UserName = model.UserName;
                        objModel.Email = model.Email;
                        objModel.Mobile = model.Mobile;
                        objModel.IsApproved = model.IsApproved ?? objModel.IsApproved;
                        objModel.IsWhatsApp = model.IsWhatsApp;
                        objModel.ModifiedOn = DateTime.Now;
                        _db.Entry(objModel).State = EntityState.Modified;
                        await _db.SaveChangesAsync();

                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {

                throw;
            };
            return model.Id.Value;
        }
        /// <summary>
        /// Add/update Agent
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<bool> SaveAgentAsync(AgentPostModel model, long userId)
        {
            try
            {

                if (model.Id == default)
                {
                    var objModel = new UserAgent();
                    objModel.CreatedOn = DateTime.Now;
                    objModel.UserId = userId;
                    objModel.FullName = !string.IsNullOrEmpty(model.FullName) ? model.FullName : null;
                    objModel.FatherName = !string.IsNullOrEmpty(model.FatherName) ? model.FatherName : null;
                    objModel.UniqueId = GenerateUniqueId();
                    objModel.Gender = !string.IsNullOrEmpty(model.Gender) ? model.Gender : null;
                    objModel.Address = !string.IsNullOrEmpty(model.Address) ? model.Address : null;
                    objModel.DateOfBirth = model.DateOfBirth ?? null;
                    objModel.DistrictId = model.DistrictId;
                    objModel.PinCode = model.PinCode;
                    objModel.IsActive = model.IsActive;
                    objModel.QualificationId = model.QualificationId;
                    var result = await _db.UserAgent.AddAsync(objModel);
                    await _db.SaveChangesAsync();
                    model.Id = result.Entity.Id;
                }
                else
                {
                    var objModel = await _db.UserAgent.FirstOrDefaultAsync(x => x.Id == model.Id);
                    objModel.FullName = !string.IsNullOrEmpty(model.FullName) ? model.FullName : null;
                    objModel.FatherName = !string.IsNullOrEmpty(model.FatherName) ? model.FatherName : null;
                    objModel.Gender = !string.IsNullOrEmpty(model.Gender) ? model.Gender : null;
                    objModel.Address = !string.IsNullOrEmpty(model.Address) ? model.Address : null;
                    objModel.DateOfBirth = model.DateOfBirth ?? null;
                    objModel.DistrictId = model.DistrictId;
                    objModel.PinCode = model.PinCode;
                    objModel.QualificationId = model.QualificationId;


                    objModel.ModifiedOn = DateTime.Now;
                    await _db.SaveChangesAsync();

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            };


        }
        /// <summary>
        /// Save Door Step Agent
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<bool> SaveDoorStepAgentAsync(DoorStepAgentPostModel model, long userId)
        {
            try
            {
                if (model.Id == default)
                {
                    var objModel = new UserDoorStepAgent();
                    objModel.CreatedOn = DateTime.Now;
                    objModel.UserId = userId;
                    objModel.FullName = !string.IsNullOrEmpty(model.FullName) ? model.FullName : null;
                    objModel.FatherName = !string.IsNullOrEmpty(model.FatherName) ? model.FatherName : null;
                    objModel.UniqueId = GenerateUniqueId();
                    objModel.Gender = !string.IsNullOrEmpty(model.Gender) ? model.Gender : null;
                    objModel.Address = !string.IsNullOrEmpty(model.Address) ? model.Address : null;
                    objModel.DateOfBirth = model.DateOfBirth ?? null;
                    objModel.DistrictId = model.DistrictId;
                    objModel.PinCode = model.PinCode;
                    objModel.SelfFunded = model.SelfFunded;
                    objModel.IsActive = true;

                    objModel.QualificationId = model.QualificationId;
                    var result = await _db.UserDoorStepAgent.AddAsync(objModel);
                    await _db.SaveChangesAsync();
                    model.Id = result.Entity.Id;
                }
                else
                {
                    var objModel = await _db.UserDoorStepAgent.FirstOrDefaultAsync(x => x.Id == model.Id);
                    objModel.FullName = !string.IsNullOrEmpty(model.FullName) ? model.FullName : null;
                    objModel.FatherName = !string.IsNullOrEmpty(model.FatherName) ? model.FatherName : null;
                    objModel.Gender = !string.IsNullOrEmpty(model.Gender) ? model.Gender : null;
                    objModel.Address = !string.IsNullOrEmpty(model.Address) ? model.Address : null;
                    objModel.DateOfBirth = model.DateOfBirth ?? null;
                    objModel.DistrictId = model.DistrictId;
                    objModel.PinCode = model.PinCode;
                    objModel.QualificationId = model.QualificationId;
                    objModel.SelfFunded = model.SelfFunded;
                    objModel.ModifiedOn = DateTime.Now;
                    await _db.SaveChangesAsync();

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            };


        }

        /// <summary>
        /// Save User Document
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<bool> SaveUserDocumentAsync(List<UserDocumentPostModel> model, long userId)
        {
            try
            {
                foreach (var item in model)
                {
                    long documentId = default;
                    if (item.Id == default)
                    {
                        var objModel = new UserDocument();
                        objModel.CreatedOn = DateTime.Now;
                        objModel.UserId = userId;
                        objModel.DocumentTypeId = item.DocumentTypeId;

                        var result = await _db.UserDocument.AddAsync(objModel);
                        await _db.SaveChangesAsync();
                        documentId = result.Entity.Id;
                    }
                    else
                    {
                        var objDocment = await _db.UserDocument.FirstOrDefaultAsync(x => x.Id == item.Id);
                        objDocment.DocumentTypeId = item.DocumentTypeId;
                        await _db.SaveChangesAsync();

                        documentId = objDocment.Id;
                    }


                    foreach (var fileitem in item.Files)
                    {
                        ////If File is in Edit Mode
                        if (fileitem.IsEditMode && fileitem.Id > 0)
                        {
                            var objFileModel = await _db.UserDocumentFiles.FirstOrDefaultAsync(x => x.Id == fileitem.Id);

                            if (objFileModel != null)
                            {
                                _fileHelper.Delete(Path.Combine(objFileModel.Path, objFileModel.FileName));

                            }

                            ////if File Not fount then remove old file
                            if (string.IsNullOrEmpty(fileitem.File))
                            {
                                _db.UserDocumentFiles.Remove(objFileModel);

                            }

                            ////if File  fount then update the file and entry

                            else
                            {
                                objFileModel.DocumentId = documentId;
                                objFileModel.FileName = _fileHelper.Save(fileitem.File, FilePathConstant.UserAgentFile, fileitem.FileName);
                                objFileModel.Path = Path.Combine(FilePathConstant.UserAgentFile, objFileModel.FileName);
                                objFileModel.FileType = _fileHelper.GetFileExtension(objFileModel.FileName); //fileitem.File.ContentType;
                            }

                        }
                        ////For File Add 

                        else
                        {
                            if (fileitem.File.IsBase64())
                            {
                                var objFileModel = new UserDocumentFiles();
                                objFileModel.DocumentId = documentId;
                                objFileModel.FileName = _fileHelper.Save(fileitem.File, FilePathConstant.UserAgentFile, fileitem.FileName);
                                objFileModel.Path = Path.Combine(FilePathConstant.UserAgentFile, objFileModel.FileName);

                                objFileModel.FileType = _fileHelper.GetFileExtension(fileitem.File); //fileitem.File.ContentType;
                                await _db.UserDocumentFiles.AddAsync(objFileModel);
                            }
                        }

                    }

                }
                await _db.SaveChangesAsync();
                return true;


            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Save User Reporting Person detail
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<bool> SaveUserReportingPersonAsync(UserReportingPersonPostModel model, long userId)
        {
            try
            {
                if (model.Id == default || model.Id == 0)
                {
                    var objModel = _mapper.Map<UserReportingPerson>(model);
                    objModel.CreatedOn = DateTime.Now;
                    objModel.UserId = userId;
                    objModel.ReportingUserId = model.ReportingUserId;
                    var result = await _db.UserReportingPerson.AddAsync(objModel);
                    await _db.SaveChangesAsync();

                }
                else
                {
                    var objModel = await _db.UserReportingPerson.FirstOrDefaultAsync(x => x.Id == model.Id);
                    objModel.ModifiedOn = DateTime.Now;
                    objModel.UserId = userId;
                    objModel.ReportingUserId = model.ReportingUserId;

                    await _db.SaveChangesAsync();

                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Save User Bank detail
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<bool> SaveUserBankAsync(UserBankDetailPostModel model, long userId)
        {
            try
            {

                if (model.Id == default)
                {
                    var objModel = new UserBank();
                    objModel.CreatedOn = DateTime.Now;
                    objModel.UserId = userId;
                    objModel.BankName = !string.IsNullOrEmpty(model.BankName) ? model.BankName : null;
                    objModel.AccountNumber = !string.IsNullOrEmpty(model.AccountNumber) ? model.AccountNumber : null;
                    objModel.Address = !string.IsNullOrEmpty(model.Address) ? model.Address : null;
                    objModel.Ifsccode = !string.IsNullOrEmpty(model.Ifsccode) ? model.Ifsccode : null;
                    var result = await _db.UserBank.AddAsync(objModel);
                    await _db.SaveChangesAsync();

                }
                else
                {
                    var objModel = await _db.UserBank.FirstOrDefaultAsync(x => x.Id == model.Id && x.UserId == userId);
                    objModel.ModifiedOn = DateTime.Now;
                    objModel.UserId = userId;
                    objModel.BankName = !string.IsNullOrEmpty(model.BankName) ? model.BankName : null;
                    objModel.AccountNumber = !string.IsNullOrEmpty(model.AccountNumber) ? model.AccountNumber : null;
                    objModel.Address = !string.IsNullOrEmpty(model.Address) ? model.Address : null;
                    objModel.Ifsccode = !string.IsNullOrEmpty(model.Ifsccode) ? model.Ifsccode : null;

                    await _db.SaveChangesAsync();

                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Save User KYC detail
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<bool> SaveUserKYCAsync(List<UserKycPostModel> model, long userId)
        {
            try
            {
                if (model != null)
                {

                    foreach (var item in model)
                    {
                        if (item.Id == default)
                        {
                            var objModel = new UserKyc();
                            objModel.CreatedOn = DateTime.Now;
                            objModel.UserId = userId;
                            objModel.KycdocumentTypeId = item.KycdocumentTypeId;
                            objModel.Kycnumber = !string.IsNullOrEmpty(item.Kycnumber) ? item.Kycnumber : null; //_security.EncryptData(item.Kycnumber)
                            var result = await _db.UserKyc.AddAsync(objModel);

                        }
                        else
                        {
                            var objModel = await _db.UserKyc.FirstOrDefaultAsync(x => x.Id == item.Id && x.UserId == userId);
                            objModel.ModifiedOn = DateTime.Now;
                            objModel.UserId = userId;
                            objModel.Kycnumber = !string.IsNullOrEmpty(item.Kycnumber) ? item.Kycnumber : null; //_security.EncryptData(item.Kycnumber)
                            objModel.KycdocumentTypeId = item.KycdocumentTypeId;



                        }
                    }
                    await _db.SaveChangesAsync();


                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Save User Nominee
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<bool> SaveUserNomineeAsync(UserNomineePostModel model, long userId)
        {
            try
            {

                if (model.Id == default)
                {
                    var objModel = new UserNominee();
                    objModel.CreatedOn = DateTime.Now;
                    objModel.UserId = userId;

                    objModel.RelationshipWithNominee = !string.IsNullOrEmpty(model.RelationshipWithNominee) ? model.RelationshipWithNominee : null;
                    objModel.NamineeName = !string.IsNullOrEmpty(model.NamineeName) ? model.NamineeName : null;
                    objModel.IsSelfDeclaration = model.IsSelfDeclaration;
                    var result = await _db.UserNominee.AddAsync(objModel);
                    await _db.SaveChangesAsync();

                }
                else
                {
                    var objModel = await _db.UserNominee.FirstOrDefaultAsync(x => x.Id == model.Id);

                    objModel.ModifiedOn = DateTime.Now;
                    objModel.RelationshipWithNominee = !string.IsNullOrEmpty(model.RelationshipWithNominee) ? model.RelationshipWithNominee : objModel.RelationshipWithNominee;
                    objModel.NamineeName = !string.IsNullOrEmpty(model.NamineeName) ? model.NamineeName : objModel.NamineeName;
                    //  objModel.IsSelfDeclaration = model.IsSelfDeclaration;

                    await _db.SaveChangesAsync();

                }

                return true;
            }
            catch (Exception)
            {
                throw;

            }

        }
        /// <summary>
        /// Save User Security Deposit
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<bool> SaveUserSecurityDepositAsync(UserSecurityDepositPostModel model, long userId)
        {
            try
            {

                if (model.Id == default)
                {
                    var objModel = new UserSecurityDepositDetails();
                    objModel.CreatedOn = DateTime.Now;
                    objModel.UserId = userId;
                    objModel.PaymentModeId = model.PaymentModeId;
                    objModel.TransactionStatus = (int)TransactionStatusEnum.None;
                    objModel.Amount = model.Amount;
                    objModel.CreditDate = model.CreditDate;
                    objModel.ReferanceNumber = !string.IsNullOrEmpty(model.ReferanceNumber) ? model.ReferanceNumber : null;
                    objModel.AccountNumber = !string.IsNullOrEmpty(model.AccountNumber) ? model.AccountNumber : null;
                    objModel.BankName = !string.IsNullOrEmpty(model.BankName) ? model.BankName : null;
                    objModel.IsActive = model.IsActive;
                    var result = await _db.UserSecurityDepositDetails.AddAsync(objModel);
                    await _db.SaveChangesAsync();

                    var user = await _db.UserDoorStepAgent.FirstOrDefaultAsync(x => x.UserId == userId);
                    if (true)
                    {
                        user.SecurityDepositId = result.Entity.Id;
                        await _db.SaveChangesAsync();
                    }


                }
                else
                {
                    var objModel = await _db.UserSecurityDepositDetails.FirstOrDefaultAsync(x => x.Id == model.Id);
                    objModel.ModifiedDate = DateTime.Now;
                    objModel.PaymentModeId = model.PaymentModeId;
                    objModel.TransactionStatus = objModel.TransactionStatus.HasValue ? objModel.TransactionStatus : (int)TransactionStatusEnum.None;
                    objModel.Amount = model.Amount;
                    objModel.CreditDate = model.CreditDate.ToLocalTime();
                    objModel.ReferanceNumber = !string.IsNullOrEmpty(model.ReferanceNumber) ? model.ReferanceNumber : null;
                    objModel.AccountNumber = !string.IsNullOrEmpty(model.AccountNumber) ? model.AccountNumber : null;
                    objModel.BankName = !string.IsNullOrEmpty(model.BankName) ? model.BankName : null;
                    await _db.SaveChangesAsync();

                }

                return true;
            }
            catch (Exception)
            {
                throw;

            }

        }
        /// <summary>
        /// Save user manager
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<bool> SaveUserManagerAsync(UserManagerModel model)
        {
            try
            {
                if (model.Id == default)
                {
                    var isExist = await _db.UserMaster.Where(x => x.Mobile == model.Mobile && x.IsDelete == false).FirstOrDefaultAsync();
                    if (isExist == null)
                    {

                        model.RoleId = ((int)UserRoleEnum.Supervisor);
                        Random random = new Random();
                        UserMaster user = new UserMaster
                        {
                            Mpin = random.Next(100000, 199999).ToString(),
                            Email = model.EmailId,
                            UserName = model.FullName,
                            Mobile = model.Mobile,
                            CreatedOn = DateTime.Now,
                            IsActive = model.IsActive,
                            UserRoleId = model.RoleId,
                            IsWhatsApp = model.IsWhatsApp,
                            Password = "12345",
                            IsApproved = true,
                            IsDelete = false,
                            ProfilePath = null
                        };
                        var result = await _db.UserMaster.AddAsync(user);
                        await _db.SaveChangesAsync();
                        model.UserId = result.Entity.Id;
                        Managers manager = new Managers
                        {
                            FullName = model.FullName,
                            FatherName = model.FatherName,
                            DateOfBirth = model.DateOfBirth,
                            UserId = model.UserId,
                            Gender = model.Gender,
                            DistrictId = model.DistrictId,
                            IsActive = model.IsActive,
                            IsDelete = model.IsDelete,
                            Pincode = model.Pincode,
                            Address = model.Address,
                            Setting = model.Setting,
                            CreatedBy = (int)UserRoleEnum.Admin,
                            ModifiedDate = DateTime.Now
                        };
                        var res = await _db.Managers.AddAsync(manager);
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    var manager = await _db.UserAgent.FirstOrDefaultAsync(x => x.Id == model.Id);
                    if (manager != null)
                    {
                        manager.FullName = model.FullName;
                        manager.FatherName = model.FatherName;
                        manager.Gender = model.Gender;
                        manager.User.Email = model.EmailId;
                        manager.ModifiedOn = DateTime.Now;
                        manager.DateOfBirth = model.DateOfBirth;
                        manager.Address = model.Address;
                        manager.PinCode = model.Pincode;
                    }
                    await _db.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
