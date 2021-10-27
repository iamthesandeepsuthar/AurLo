using AurigainLoanERP.Data.Database;
using AurigainLoanERP.Shared.Common.Method;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using AurigainLoanERP.Shared.ExtensionMethod;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AurigainLoanERP.Shared.Enums.FixedValueEnums;

namespace AurigainLoanERP.Services.FreshLead
{
    public class FreshLeadService : BaseService , IFreshLeadService
    {
        public readonly IMapper _mapper;
        private AurigainContext _db;        
        public FreshLeadService(IMapper mapper, AurigainContext db)
        {
            this._mapper = mapper;
            _db = db;           
        }
        #region  <<Gold Loan Fresh Lead>>
        public async Task<ApiServiceResponseModel<string>>SaveGoldLoanFreshLeadAsync(GoldLoanFreshLeadModel model) 
        {
            ApiServiceResponseModel<string> objResponse = new ApiServiceResponseModel<string>();
            try
            {
                if (model != null)
                {
                    await _db.Database.BeginTransactionAsync();

                    long leadId = 0;
                    leadId = await SaveBasicDetailGoldFreshLead(model);
                   
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
                    return CreateResponse<string>(null, ResponseMessage.InvalidData, false, ((int)ApiStatusCode.InvaildModel));

                }
            }
            catch (Exception ex)
            {
                _db.Database.RollbackTransaction();
                return CreateResponse<string>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        #endregion

        #region <<Personal Loan , Home Loan , Vehicel Loan Fresh Lead>>

        #endregion

        #region  <<Private method>>
        private async Task<long> SaveBasicDetailGoldFreshLead( GoldLoanFreshLeadModel model) 
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
                    FatherName = model.FatherName,
                    LeadSourceByUserId = model.LeadSourceByUserId,
                    Purpose = model.Purpose,
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

        private async Task<bool> SaveJewelleryDetail(GoldLoanFreshLeadJewelleryDetailModel model , long freshLeadId) 
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
                        AppointmentTime = model.AppointmentTime,
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
                    detail.AppointmentTime = model.AppointmentTime;
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
        #endregion
    }
}
