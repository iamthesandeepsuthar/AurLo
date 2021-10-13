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

        #region <<Agent User>>

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
                long userId = 0;
                if (model.User != null)
                {
                    model.User.UserRoleId = (int)UserRoleEnum.Agent;
                    userId = await SaveUserAsync(model.User);

                    if (userId > 0)
                    {


                        await SaveAgentAsync(model, userId);

                        if (model.BankDetails != null)
                        {
                            await SaveUserBankAsync(model.BankDetails, userId);

                        }

                        if (model.ReportingPerson != null)
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
        public Task<ApiServiceResponseModel<List<AgentViewModel>>> GetAgentAsync(IndexModel model)
        {
            throw new NotImplementedException();
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
                            objAgentModel.UserKYC = _mapper.Map<List<UserKycViewModel>>(objAgent.User.UserKyc.ToList() ?? null);

                        }
                        if (objAgent.User.UserDocument != null)
                        {
                            foreach (var item in objAgent.User.UserDocument.Select((value, i) => new { i, value }))
                            {
                                objAgentModel.Documents.Add(_mapper.Map<UserDocumentViewModel>(item.value));
                                if (item.value.UserDocumentFiles != null)
                                {
                                    foreach (var fileitem in item.value.UserDocumentFiles)
                                    {
                                        objAgentModel.Documents[item.i].UserDocumentFiles.Add(
                                        new UserDocumentFilesViewModel
                                        {
                                            Id = fileitem.Id,
                                            DocumentId = fileitem.DocumentId,
                                            FileName = fileitem.FileName,
                                            FileType = fileitem.FileType,
                                            Path = fileitem.Path.ToAbsoluteUrl()
                                        });

                                    }

                                }
                            }
                        }
                        if (objAgent.User.UserNominee != null)
                        {
                            objAgentModel.UserNominee = _mapper.Map<UserNomineeViewModel>(objAgent.User.UserNominee.FirstOrDefault() ?? null);

                        }
                        objAgentModel.DistrictName = objAgent.District.Name;
                        objAgentModel.StateName = objAgent.District.State.Name;
                        objAgentModel.QualificationName = objAgent.Qualification.Name;
                        objAgentModel.User.UserRoleName = objAgent.User.UserRole.Name;
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

        #region <<Door-step Agent>>
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
                                KycdocumentTypeName =  _db.DocumentType.FirstOrDefault(z=>z.Id==x.KycdocumentTypeId).DocumentName ?? null,
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
                                    UserDocumentFiles = _db.UserDocumentFiles.Where(x=> x.DocumentId==item.Id).Select(fileitem => new UserDocumentFilesViewModel
                                    {
                                        Id = fileitem.Id,
                                        DocumentId = fileitem.DocumentId,
                                        FileName = fileitem.FileName,
                                        FileType = fileitem.FileType,
                                        Path = fileitem.Path.ToAbsoluteUrl()
                                    }).ToList() ?? null
                                });

                                //if (item.value.UserDocumentFiles != null)
                                //{
                                //    foreach (var fileitem in item.value.UserDocumentFiles)
                                //    {
                                //        objDoorStepAgentModel.Documents[item.i].UserDocumentFiles.Add(
                                //        new UserDocumentFilesViewModel
                                //        {
                                //            Id = fileitem.Id,
                                //            DocumentId = fileitem.DocumentId,
                                //            FileName = fileitem.FileName,
                                //            FileType = fileitem.FileType,
                                //            Path = fileitem.Path.ToAbsoluteUrl()
                                //        });

                                //    }

                                //}
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

        #region <<Common Method>>
        public async Task<ApiServiceResponseModel<object>> UpateActiveStatus(long id)
        {
            try
            {
                await _db.Database.BeginTransactionAsync();
                var user = await _db.UserMaster.FirstOrDefaultAsync(X => X.Id == id);
                user.IsActive = !user.IsActive;
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
        public async Task<ApiServiceResponseModel<object>> UpdateDeleteStatus(long id)
        {
            try
            {
                await _db.Database.BeginTransactionAsync();
                var user = await _db.UserMaster.FirstOrDefaultAsync(X => X.Id == id);
                user.IsDelete = !user.IsDelete;
                await _db.SaveChangesAsync();
                _db.Database.CommitTransaction();
                return CreateResponse(true as object, ResponseMessage.Update, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception)
            {
                _db.Database.RollbackTransaction();
                return CreateResponse(true as object, ResponseMessage.Fail, true, ((int)ApiStatusCode.DataBaseTransactionFailed));

            }
        }
        #endregion

        #region << Private Method >>

        /// <summary>
        /// Save User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<long> SaveUserAsync(UserPostModel model)
        {
            try
            {
                var existingUser = await _db.UserMaster.FirstOrDefaultAsync(x => (model.Id == default || x.Id != model.Id) && x.Mobile == model.Mobile || x.Email == model.Email);
                if (existingUser == null)
                {
                    if (model.Id == default)
                    {

                        var objModel = _mapper.Map<UserMaster>(model);
                        objModel.UserName = model.UserName ?? model.Email;

                        objModel.CreatedOn = DateTime.Now;
                        objModel.Mpin = GenerateUniqueId();//_security.EncryptData(GenerateUniqueId());
                        var result = await _db.UserMaster.AddAsync(objModel);
                        await _db.SaveChangesAsync();
                        model.Id = result.Entity.Id;
                    }
                    else
                    {
                        var objModel = await _db.UserMaster.FirstOrDefaultAsync(x => x.Id == model.Id);
                        objModel = _mapper.Map<UserMaster>(model);
                        objModel.ModifiedOn = DateTime.Now;
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
                    objModel.ProfilePictureUrl = !string.IsNullOrEmpty(model.ProfilePictureUrl) ? Path.Combine(FilePathConstant.UserProfile, _fileHelper.Save(model.ProfilePictureUrl, FilePathConstant.UserProfile)) : null;
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
                    objModel.ProfilePictureUrl = !string.IsNullOrEmpty(model.ProfilePictureUrl) ? Path.Combine(FilePathConstant.UserProfile, _fileHelper.Save(model.ProfilePictureUrl, FilePathConstant.UserProfile)) : null;
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
                        if (fileitem.IsEditMode)
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
                            var objFileModel = new UserDocumentFiles();
                            objFileModel.DocumentId = documentId;
                            objFileModel.FileName = _fileHelper.Save(fileitem.File, FilePathConstant.UserAgentFile, fileitem.FileName);
                            objFileModel.Path = Path.Combine(FilePathConstant.UserAgentFile, objFileModel.FileName);

                            objFileModel.FileType = _fileHelper.GetFileExtension(fileitem.File); //fileitem.File.ContentType;
                            await _db.UserDocumentFiles.AddAsync(objFileModel);
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

                if (model.Id == default)
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
                    //  objModel.IsSelfDeclaration = model.IsSelfDeclaration;
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
                    objModel.TransactionStatus = model.TransactionStatus ?? null;
                    objModel.Amount = model.Amount;
                    objModel.CreditDate = model.CreditDate;
                    objModel.ReferanceNumber = !string.IsNullOrEmpty(model.ReferanceNumber) ? model.ReferanceNumber : null;
                    objModel.AccountNumber = !string.IsNullOrEmpty(model.AccountNumber) ? model.AccountNumber : null;
                    objModel.BankName = !string.IsNullOrEmpty(model.BankName) ? model.BankName : null;
                    objModel.IsActive = model.IsActive;
                    var result = await _db.UserSecurityDepositDetails.AddAsync(objModel);
                    var user = await _db.UserDoorStepAgent.FirstOrDefaultAsync(x => x.Id == userId);
                    user.SecurityDepositId = result.Entity.Id;
                    await _db.SaveChangesAsync();

                }
                else
                {
                    var objModel = await _db.UserSecurityDepositDetails.FirstOrDefaultAsync(x => x.Id == model.Id);
                    objModel.ModifiedDate = DateTime.Now;
                    objModel.PaymentModeId = model.PaymentModeId;
                    objModel.TransactionStatus = model.TransactionStatus;
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

        #endregion
    }
}
