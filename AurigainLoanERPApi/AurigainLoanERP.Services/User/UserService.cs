using AurigainLoanERP.Data.Database;
using AurigainLoanERP.Shared.Common;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AurigainLoanERP.Services.User
{
    public class UserService : IUserService
    {
        public readonly IMapper _mapper;
        private AurigainContext _db;
        public UserService(IMapper mapper, AurigainContext db)
        {
            this._mapper = mapper;
            _db = db;
        }

        public async Task<ApiServiceResponseModel<string>> AddUpdateAgentAsync(AgentPostModel model)
        {
            try
            {
                await _db.Database.BeginTransactionAsync();

                await SaveUserBankAsync(model.BankDetails, 1);
                await SaveUserReportingPersonAsync(model.ReportingPerson, 1);
                await SaveUserDocumentAsync(model.Documents, 1);
                await SaveUserKYCAsync(model.UserKYC, 1);
                await SaveUserNomineeAsync(model.UserNominee, 1);
                _db.Database.CommitTransaction();
            }
            catch (Exception)
            {
                _db.Database.RollbackTransaction();

                throw;
            }
            return null;
        }



        public Task<ApiServiceResponseModel<List<AgentViewModel>>> GetAsync(IndexModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ApiServiceResponseModel<AgentViewModel>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiServiceResponseModel<object>> UpateActiveStatus(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiServiceResponseModel<object>> UpdateDeleteStatus(int id)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> SaveUserDocumentAsync(List<UserDocumentPostModel> model, long userId)
        {
            try
            {
                foreach (var item in model)
                {
                    long documentId = 0;
                    if (item.Id == default)
                    {
                        var objModel = _mapper.Map<UserDocument>(item);
                        objModel.CreatedOn = DateTime.Now;
                        objModel.UserId = userId;

                        var result = await _db.UserDocument.AddAsync(objModel);
                        await _db.SaveChangesAsync();
                        documentId = result.Entity.Id;
                    }
                    else
                    {
                        var objDocment = await _db.UserDocument.FirstOrDefaultAsync(x => x.Id == item.Id && x.UserId == userId);
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
                            ////if File Not fount then remove old file
                            if (fileitem.File == null)
                            {
                                FileHelper.Delete(Path.Combine(objFileModel.Path, objFileModel.FileName));
                                _db.UserDocumentFiles.Remove(objFileModel);

                            }
                            ////if File  fount then update the file and entry

                            else
                            {
                                objFileModel.DocumentId = objFileModel.DocumentId;
                                objFileModel.FileName = FileHelper.Save(fileitem.File, FilePathConstant.UserAgentFile);
                                objFileModel.Path = FilePathConstant.UserAgentFile;
                                objFileModel.FileType = FileHelper.GetFileExtension(objFileModel.FileName); //fileitem.File.ContentType;
                            }

                        }
                        ////For File Add 

                        else
                        {
                            var objFileModel = _mapper.Map<UserDocumentFiles>(item);
                            objFileModel.DocumentId = documentId;
                            objFileModel.FileName = FileHelper.Save(fileitem.File, FilePathConstant.UserAgentFile);
                            objFileModel.Path = FilePathConstant.UserAgentFile;
                            objFileModel.FileType = FileHelper.GetFileExtension(objFileModel.FileName); //fileitem.File.ContentType;
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
            return false;
        }
        private async Task<bool> SaveUserReportingPersonAsync(UserReportingPersonPostModel model, long userId)
        {
            try
            {

                if (model.Id == default)
                {
                    var objModel = _mapper.Map<UserReportingPerson>(model);
                    objModel.CreatedOn = DateTime.Now;
                    objModel.UserId = userId;

                    var result = await _db.UserReportingPerson.AddAsync(objModel);
                    await _db.SaveChangesAsync();

                }
                else
                {
                    var objModel = await _db.UserReportingPerson.FirstOrDefaultAsync(x => x.Id == model.Id && x.UserId == userId);
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
            return false;
        }
        private async Task<bool> SaveUserBankAsync(UserBankDetailPostModel model, long userId)
        {
            try
            {

                if (model.Id == default)
                {
                    var objModel = _mapper.Map<UserBank>(model);
                    objModel.CreatedOn = DateTime.Now;
                    objModel.UserId = userId;

                    var result = await _db.UserBank.AddAsync(objModel);
                    await _db.SaveChangesAsync();

                }
                else
                {
                    var objModel = await _db.UserBank.FirstOrDefaultAsync(x => x.Id == model.Id && x.UserId == userId);
                    objModel.ModifiedOn = DateTime.Now;
                    objModel.UserId = userId;
                    objModel.BankName = model.BankName;
                    objModel.AccountNumber = model.AccountNumber;
                    objModel.Address = model.Address;
                    objModel.Ifsccode = model.Ifsccode;

                    await _db.SaveChangesAsync();

                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
            return false;
        }
        private async Task<bool> SaveUserKYCAsync(UserKycPostModel model, long userId)
        {
            try
            {

                if (model.Id == default)
                {
                    var objModel = _mapper.Map<UserKyc>(model);
                    objModel.CreatedOn = DateTime.Now;
                    objModel.UserId = userId;

                    var result = await _db.UserKyc.AddAsync(objModel);
                    await _db.SaveChangesAsync();

                }
                else
                {
                    var objModel = await _db.UserKyc.FirstOrDefaultAsync(x => x.Id == model.Id && x.UserId == userId);
                    objModel.ModifiedOn = DateTime.Now;
                    objModel.UserId = userId;
                    objModel.Kycnumber = model.Kycnumber;
                    objModel.KycdocumentTypeId = model.KycdocumentTypeId;

                    await _db.SaveChangesAsync();

                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
            return false;
        }
        private async Task<bool> SaveUserNomineeAsync(UserNomineePostModel model, long userId)
        {
            try
            {

                if (model.Id == default)
                {
                    var objModel = _mapper.Map<UserNominee>(model);
                    objModel.CreatedOn = DateTime.Now;
                    objModel.UserId = userId;

                    var result = await _db.UserNominee.AddAsync(objModel);
                    await _db.SaveChangesAsync();

                }
                else
                {
                    var objModel = await _db.UserNominee.FirstOrDefaultAsync(x => x.Id == model.Id && x.UserId == userId);
                   
                    objModel.ModifiedOn = DateTime.Now;
                    objModel.RelationshipWithNominee = model.RelationshipWithNominee;
                    objModel.NamineeName = model.NamineeName;

                    await _db.SaveChangesAsync();

                }

                return true;
            }
            catch (Exception)
            {

               
            }
            return false;
        }

    }
}
