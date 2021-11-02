using AurigainLoanERP.Data.Database;
using AurigainLoanERP.Shared.Common.Method;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AurigainLoanERP.Shared.Enums.FixedValueEnums;
using Microsoft.Extensions.Configuration;

namespace AurigainLoanERP.Services.Customer
{
    public class CustomerService : BaseService , ICustomerService
    {
        public readonly IMapper _mapper;
        private AurigainContext _db;
        private readonly EmailHelper _emailHelper;
        public CustomerService(IMapper mapper, AurigainContext db, Microsoft.Extensions.Configuration.IConfiguration _configuration, IHostingEnvironment environment)
        {
            this._mapper = mapper;
            _db = db;
            _emailHelper = new EmailHelper(_configuration, environment);
        }
        public async Task<ApiServiceResponseModel<string>> AddUpdateAsync(CustomerRegistrationModel model)
        {
            try
            {
                await _db.Database.BeginTransactionAsync();
                if (model != null)
                {                   
                    var userId = await SaveCustomerAsync(model);
                    if (userId> 0)
                    {
                        var result  = await SaveUserKYCAsync(model.KycDocuments, userId);
                        if (result)
                        {
                            var user = await _db.UserMaster.Where(x => x.Id == userId).FirstOrDefaultAsync();
                            Dictionary<string, string> replaceValues = new Dictionary<string, string>();
                            replaceValues.Add("{{UserName}}", user.Email  + "/" + user.Mobile);
                            replaceValues.Add("{{Password}}", user.Password);
                            await _emailHelper.SendHTMLBodyMail(user.Email, "Registration Notification", EmailPathConstant.RegisterTemplate, replaceValues);
                            _db.Database.CommitTransaction();                          
                            return CreateResponse<string>("", ResponseMessage.Save, true, ((int)ApiStatusCode.Ok));
                        }
                        else
                        {
                            _db.Database.RollbackTransaction();
                            return CreateResponse<string>(null, "Document not uploaded !,Please try again.", false, ((int)ApiStatusCode.DataBaseTransactionFailed));
                        }
                      
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
        private async Task<long> SaveCustomerAsync(CustomerRegistrationModel model)
        {
            try
            {
                if (model.Id == default)
                {
                    var isExist = await _db.UserMaster.Where(x => x.Mobile == model.User.Email && x.Mobile == model.User.Mobile && x.IsDelete == false).FirstOrDefaultAsync();
                    if (isExist == null)
                    {
                        model.User.UserRoleId = ((int)UserRoleEnum.Customer);
                        Random random = new Random();
                        UserMaster user = new UserMaster
                        {
                            Mpin = random.Next(100000, 199999).ToString(),
                            Email = model.User.Email,
                            UserName = model.FullName,
                            Mobile = model.User.Mobile,
                            CreatedOn = DateTime.Now,
                            IsActive = model.IsActive,
                            UserRoleId = model.User.UserRoleId.Value,
                            IsWhatsApp = model.User.IsWhatsApp,
                            Password = "12345",
                            IsApproved = true,
                            IsDelete = false,
                            ProfilePath = null
                        };
                        var result = await _db.UserMaster.AddAsync(user);
                        await _db.SaveChangesAsync();
                        model.UserId = result.Entity.Id;
                        UserCustomer customer = new UserCustomer
                        {
                            FullName = model.FullName,
                            FatherName = model.FatherName,
                            DateOfBirth = model.DateOfBirth,
                            UserId = model.UserId,
                            Gender = model.Gender,                        
                            IsActive = model.IsActive,
                            IsDelete = model.IsDelete,
                            PincodeAreaId = model.PincodeAreaId,
                            Address = model.Address,                           
                            CreatedBy = (int)UserRoleEnum.Admin                            
                        };
                        var res = await _db.UserCustomer.AddAsync(customer);
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    var customer = await _db.UserCustomer.Where(x => x.Id == model.Id).Include(x => x.User).FirstOrDefaultAsync();
                    if (customer != null)
                    {
                        customer.FullName = model.FullName;
                        customer.FatherName = model.FatherName;
                        customer.Gender = model.Gender;
                        customer.User.Email = model.User.Email;
                        customer.DateOfBirth = model.DateOfBirth;
                        customer.Address = model.Address;
                        customer.PincodeAreaId = model.PincodeAreaId;
                        customer.User.UserRoleId = model.User.UserRoleId.Value;
                    }
                    await _db.SaveChangesAsync();
                }
                return model.UserId;
            }
            catch (Exception)
            {  throw;  }
        }
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
    }
}
