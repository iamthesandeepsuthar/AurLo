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
using AurigainLoanERP.Shared.ExtensionMethod;

namespace AurigainLoanERP.Services.Customer
{
    public class CustomerService : BaseService, ICustomerService
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
        public async Task<ApiServiceResponseModel<string>> TestEmail()
        {
            Dictionary<string, string> replaceValues = new Dictionary<string, string>();
            replaceValues.Add("{{UserName}}", "Aakash");
            replaceValues.Add("{{Password}}", "1234556");
            await _emailHelper.SendHTMLBodyMail("sandeep.suthar08@gmail.com", "Registration Notification", EmailPathConstant.RegisterTemplate, replaceValues);
            return CreateResponse<string>("", ResponseMessage.Save, true, ((int)ApiStatusCode.Ok));
        }
        public async Task<ApiServiceResponseModel<List<CustomerListModel>>> GetListAsync(IndexModel model)
        {
            ApiServiceResponseModel<List<CustomerListModel>> objResponse = new ApiServiceResponseModel<List<CustomerListModel>>();
            try
            {

                var result = (from customer in _db.UserCustomer
                              where !customer.User.IsDelete && (string.IsNullOrEmpty(model.Search) || customer.FullName.Contains(model.Search) || customer.User.Mobile.Contains(model.Search) || customer.User.Email.Contains(model.Search)) || customer.PincodeArea.Pincode.Contains(model.Search) || customer.Gender.Contains(model.Search)
                              select customer);
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
                    case "Pincode":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.User.Email ascending select orderData) : (from orderData in result orderby orderData.User.Email descending select orderData);
                        break;
                    default:
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.User.UserRole.Name ascending select orderData) : (from orderData in result orderby orderData.User.UserRole.Name descending select orderData);
                        break;
                }

                var data = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);

                objResponse.Data = await (from detail in data
                                          where detail.IsDelete == false
                                          select new CustomerListModel
                                          {
                                              Id = detail.Id,
                                              UserId = detail.User != null ? detail.User.Id : default,
                                              FullName = detail.FullName ?? null,
                                              EmailId = detail.User.Email,
                                              Mobile = detail.User.Mobile,
                                              FatherName = detail.FatherName,
                                              IsApproved = detail.User.IsApproved,
                                              Gender = detail.Gender ?? null,
                                              ProfileImageUrl = detail.User.ProfilePath.ToAbsolutePath() ?? null,
                                              IsActive = detail.User.IsActive,
                                              Password = detail.User.Password,
                                              Pincode = detail.PincodeArea.Pincode
                                          }).ToListAsync();
                if (result != null)
                {
                    return CreateResponse(objResponse.Data, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: result.Count());
                }
                else
                {
                    return CreateResponse<List<CustomerListModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
                }

            }
            catch (Exception ex)
            {
                return CreateResponse<List<CustomerListModel>>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message ?? ex.InnerException.ToString());
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
                        case (int)UserRoleEnum.Customer:
                            var customerUser = await _db.UserCustomer.Where(x => x.UserId == id).FirstOrDefaultAsync();
                            customerUser.IsActive = !customerUser.IsActive;
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
                        case (int)UserRoleEnum.Customer:
                            var supervisorUser = _db.UserCustomer.Where(x => x.UserId == id).FirstOrDefault();
                            supervisorUser.IsDelete = !supervisorUser.IsDelete;
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
                return CreateResponse(true as object, ResponseMessage.Fail, true, ((int)ApiStatusCode.DataBaseTransactionFailed));

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
        public async Task<ApiServiceResponseModel<string>> AddUpdateAsync(CustomerRegistrationModel model)
        {
            try
            {
                await _db.Database.BeginTransactionAsync();
                if (model != null)
                {
                    var userId = await SaveCustomerAsync(model);
                    if (userId > 0)
                    {
                        var result = await SaveUserKYCAsync(model.KycDocuments, userId);
                        if (result)
                        {
                            var user = await _db.UserMaster.Where(x => x.Id == userId).FirstOrDefaultAsync();
                            Dictionary<string, string> replaceValues = new Dictionary<string, string>();
                            replaceValues.Add("{{UserName}}", user.Email);
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

        //for profile detail
        public async Task<ApiServiceResponseModel<CustomerRegistrationViewModel>> GetCustomerProfile(long id)
        {
            try
            {
                var detail = await _db.UserCustomer.Where(x => x.UserId == id).Include(x => x.User).ThenInclude(x => x.UserKyc).Include(x => x.PincodeArea).ThenInclude(y => y.District).ThenInclude(y => y.State).FirstOrDefaultAsync();

                if (detail != null)
                {
                    CustomerRegistrationViewModel customer = new CustomerRegistrationViewModel
                    {
                        Id = detail.Id,
                        UserId = detail.UserId,
                        FullName = detail.FullName,
                        FatherName = detail.FatherName,
                        EmailId = detail.User.Email,
                        Mobile = detail.User.Mobile,
                        Gender = detail.Gender,
                        DateOfBirth = detail.DateOfBirth,
                        Address = detail.Address,
                        AreaName = detail.PincodeArea.AreaName,
                        Pincode = detail.PincodeArea.Pincode,
                        District = detail.PincodeArea.District.Name,
                        State = detail.PincodeArea.District.State.Name,
                        IsActive = detail.IsActive,
                        CreatedOn = detail.CreatedOn,


                    };
                    var docs = await _db.UserKyc.Where(x => x.UserId == detail.UserId).Include(x => x.KycdocumentType).ToListAsync();
                    foreach (var doc in docs)
                    {
                        CustomerKycViewModel kycDoc = new CustomerKycViewModel
                        {
                            Id = doc.Id,
                            KycdocumentTypeId = doc.KycdocumentTypeId,
                            Kycnumber = doc.Kycnumber,
                            KycdocumentTypeName = doc.KycdocumentType.DocumentName,
                            UserId = doc.UserId
                        };
                        customer.KycDocuments.Add(kycDoc);
                    }
                    return CreateResponse<CustomerRegistrationViewModel>(customer, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<CustomerRegistrationViewModel>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.NotFound));
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<CustomerRegistrationViewModel>(null, ex.Message + " " + ex.InnerException.Message, false, ((int)ApiStatusCode.InternalServerError));
            }
        }

        //for edit pupose
        public async Task<ApiServiceResponseModel<CustomerRegistrationModel>> GetCustomerDetail(long id)
        {
            try
            {
                var data = await _db.UserCustomer.Where(x => x.UserId == id).Include(x => x.PincodeArea).Include(x => x.User).FirstOrDefaultAsync();
                if (data != null)
                {

                    UserPostModel user = new UserPostModel
                    {
                        Id = data.User.Id,
                        Email = data.User.Email,
                        Mobile = data.User.Mobile,
                        UserRoleId = data.User.UserRoleId,
                        IsApproved = data.User.IsApproved,
                        IsWhatsApp = data.User.IsWhatsApp,
                        UserName = data.User.UserName
                    };
                    List<UserKycPostModel> userDocuments = _db.UserKyc.Where(x => x.UserId == data.UserId).Select(x => new UserKycPostModel
                    {
                        KycdocumentTypeId = x.KycdocumentTypeId,
                        Kycnumber = x.Kycnumber,
                        Id = x.Id
                    }).ToList();

                    CustomerRegistrationModel customer = new CustomerRegistrationModel
                    {
                        Id = data.Id,
                        User = user,
                        FullName = data.FullName,
                        Address = data.Address,
                        FatherName = data.FatherName,
                        Gender = data.Gender,
                        DateOfBirth = data.DateOfBirth,
                        IsActive = data.IsActive,
                        PincodeAreaId = data.PincodeAreaId,
                        PinCode = data.PincodeArea.Pincode,
                        UserId = data.UserId,
                        KycDocuments = userDocuments,
                        CreatedOn = data.CreatedOn
                    };
                    return CreateResponse<CustomerRegistrationModel>(customer, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));

                }
                else
                {
                    return CreateResponse<CustomerRegistrationModel>(null, ResponseMessage.NotFound, false, ((int)ApiStatusCode.NotFound));
                }

            }
            catch (Exception ex)
            {
                return CreateResponse<CustomerRegistrationModel>(null, ex.Message + " " + ex.InnerException.Message, false, ((int)ApiStatusCode.InternalServerError));
            }
        }

        #region <<Private Method Of Customer>>
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
                        return isExist.Id;
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
            { throw; }
        }
        private async Task<bool> SaveUserKYCAsync(List<UserKycPostModel> model, long userId)
        {
            try
            {
                if (model != null)
                {

                    var docType = model.Select(x => x.KycdocumentTypeId).ToArray();
                    var removedDate = await _db.UserKyc.Where(x => x.UserId == userId && !docType.Contains(x.KycdocumentTypeId)).ToListAsync();

                    if (removedDate != null && removedDate.Count > 0)
                    {
                        _db.UserKyc.RemoveRange(removedDate);
                        await _db.SaveChangesAsync();

                    }

                    foreach (var item in model)
                    {
                        var objModel = await _db.UserKyc.FirstOrDefaultAsync(x => x.UserId == userId && x.KycdocumentTypeId == item.KycdocumentTypeId);
                        if (objModel != null)
                        {
                            objModel.ModifiedOn = DateTime.Now;
                            objModel.UserId = userId;
                            objModel.Kycnumber = !string.IsNullOrEmpty(item.Kycnumber) ? item.Kycnumber : null; //_security.EncryptData(item.Kycnumber)
                            objModel.KycdocumentTypeId = item.KycdocumentTypeId;
                        }
                        else
                        {
                            var objNewModel = new UserKyc();
                            objNewModel.CreatedOn = DateTime.Now;
                            objNewModel.UserId = userId;
                            objNewModel.KycdocumentTypeId = item.KycdocumentTypeId;
                            objNewModel.Kycnumber = !string.IsNullOrEmpty(item.Kycnumber) ? item.Kycnumber : null; //_security.EncryptData(item.Kycnumber)
                            var result = await _db.UserKyc.AddAsync(objNewModel);

                        }

                    }
                    await _db.SaveChangesAsync();
                    return true;
                }
                return false;

            }
            catch (Exception)
            {

                throw;
            }

        }

        #endregion
    }
}
