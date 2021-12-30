using AurigainLoanERP.Data.Database;
using AurigainLoanERP.Shared.Common.Method;
using AurigainLoanERP.Shared.Common.Model;
using AurigainLoanERP.Shared.ContractModel;
using AurigainLoanERP.Shared.ExtensionMethod;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AurigainLoanERP.Shared.Enums.FixedValueEnums;

namespace AurigainLoanERP.Services.Common
{
    public class CommonService : BaseService, ICommonService
    {
        public readonly IMapper _mapper;
        private readonly AurigainContext _db;
        public CommonService(IMapper mapper, AurigainContext db)
        {
            this._mapper = mapper;
            _db = db;
        }

        public async Task<ApiServiceResponseModel<Dictionary<string, object>>> GetDropDown(string[] key)
        {
            Dictionary<string, object> objData = new Dictionary<string, object>();
            try
            {
                foreach (var item in key)
                {

                    switch (item.ToLower())
                    {
                        case DropDownKey.ddlParentUserRole:

                            objData.Add(item, await GetUserRole(true));
                            break;

                        case DropDownKey.ddlUserRole:

                            objData.Add(item, await GetUserRole());
                            break;

                        case DropDownKey.ddlState:

                            objData.Add(item, await GetState());
                            break;
                        case DropDownKey.ddlDistrict:

                            objData.Add(item, await GetDistrict());
                            break;
                        case DropDownKey.ddlQualification:

                            objData.Add(item, await GetQualification());
                            break;
                        case DropDownKey.ddlDocumentType:

                            objData.Add(item, await GetDocumentType());
                            break;

                        case DropDownKey.ddlRelationship:

                            objData.Add(item, GetEnumDropDown<RelationshipEnum>());
                            break;
                        case DropDownKey.ddlGender:

                            objData.Add(item, GetEnumDropDown<GenderEnum>());
                            break;

                        case DropDownKey.ddlPaymentMode:

                            objData.Add(item, await GetPaymentMode());
                            break;

                        case DropDownKey.ddlBank:

                            objData.Add(item, await GetBanks());
                            break;

                        case DropDownKey.ddlLeadApprovalStatus:

                            objData.Add(item, GetEnumDropDownWithInt<LeadApprovalStatusEnum>());
                            break;
                        case DropDownKey.ddlLeadStatus:

                            objData.Add(item, GetEnumDropDownWithInt<LeadStatusEnum>());
                            break;
                        default:
                            break;
                    }



                }

                return CreateResponse(objData, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {

                return CreateResponse<Dictionary<string, object>>(null, ResponseMessage.Success, true, ((int)ApiStatusCode.ServerException), ex.Message.ToString());

            }

        }

        public async Task<ApiServiceResponseModel<Dictionary<string, object>>> GetFilterDropDown(FilterDropDownPostModel[] model)
        {
            Dictionary<string, object> objData = new Dictionary<string, object>();
            try
            {
                foreach (var item in model)
                {

                    switch (item.Key.ToLower())
                    {

                        case DropDownKey.ddlDistrict:
                            if (item.FileterFromKey.ToLower() == DropDownKey.ddlState.ToLower().ToString())
                            {
                                objData.Add(item.Key, await GetDistrict(item.Values));
                            }

                            break;
                        default:
                            break;
                    }

                }

                return CreateResponse(objData, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {

                return CreateResponse<Dictionary<string, object>>(null, ResponseMessage.Success, true, ((int)ApiStatusCode.ServerException), ex.Message.ToString());

            }

        }

        public async Task<ApiServiceResponseModel<CommonModel>> GetCommonDataByUserId(long userId) 
        {
            CommonModel model = new CommonModel();
            try
            {
                model.Qualification =await  getQualificaitonData();
                model.Purpose = await getPurposeData();
                model.ProductCategory = await getProductCategories();
                return CreateResponse(model, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex) 
            {
                return CreateResponse<CommonModel>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.ServerException), ex.Message.ToString());
            }
        }

        #region <<private Method>>
        private async Task<List<DDLQualificationModel>> getQualificaitonData()
        {
            List<DDLQualificationModel> Qualification = new List<DDLQualificationModel>();
            try
            {
                Qualification =  await (from data in _db.QualificationMaster where data.IsActive == true && !data.IsDelete select data)
                     .Select(item => new DDLQualificationModel { Name = item.Name, Id = item.Id })
                     .ToListAsync();
                return Qualification;
            }
            catch
            {

                return Qualification;
            }
        }
        private async Task<List<ddlPurposeModel>> getPurposeData()
        {
            List<ddlPurposeModel> purpose = new List<ddlPurposeModel>();
            try
            {
                purpose = await (from data in _db.Purpose where data.IsActive.Value == true && !data.IsDelete select data)
                     .Select(item => new ddlPurposeModel { Name = item.Name, Id = item.Id })
                     .ToListAsync();
                return purpose;
            }
            catch
            {
                return purpose;
            }
        }
        private async Task<List<DllProductCategoryModel>> getProductCategories()
        {
            List<DllProductCategoryModel> productCategory = new List<DllProductCategoryModel>();
            try
            {
                productCategory = await (from data in _db.ProductCategory where data.IsActive.Value == true && !data.IsDelete select data)
                     .Select(item => new DllProductCategoryModel
                     {
                         Name = item.Name,
                         Id = item.Id }).ToListAsync();
                return productCategory;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get User Role
        /// </summary>
        /// <param name="isParent">for getiing Parrent Role</param>
        /// <returns></returns>
        private async Task<object> GetUserRole(bool? isParent = false)
        {
            try
            {
                return await (from role in _db.UserRole where role.IsActive == true && !role.IsDelete && (isParent == false || role.ParentId == null) select role)
                     .Select(r => new { Text = r.Name, Value = r.Id })
                     .ToListAsync();
            }
            catch
            {

                return null;
            }
        }

        private async Task<object> GetQualification()
        {
            try
            {
                return await (from data in _db.QualificationMaster where data.IsActive == true && !data.IsDelete select data)
                     .Select(item => new { Text = item.Name, Value = item.Id })
                     .ToListAsync();
            }
            catch
            {

                return null;
            }
        }

        private async Task<object> GetState()
        {
            try
            {
                return await (from data in _db.State where data.IsActive == true && !data.IsDelete select data)
                     .Select(item => new { Text = item.Name, Value = item.Id }).ToListAsync();
            }
            catch
            {

                return null;
            }
        }

        private async Task<object> GetDistrict(int[] stateId = null)
        {
            try
            {
                return await (from data in _db.District where data.IsActive == true && !data.IsDelete && (stateId == null || stateId.Contains(data.StateId)) select data)
                     .Select(r => new { Text = r.Name, Value = r.Id })
                     .ToListAsync();
            }
            catch
            {

                return null;
            }
        }

        private async Task<object> GetDocumentType()
        {
            try
            {
                return await (from data in _db.DocumentType where data.IsActive == true && !data.IsDelete select data)
                     .Select(item => new { Text = item.DocumentName, Value = item.Id })
                     .ToListAsync();
            }
            catch
            {

                return null;
            }
        }

        private object GetEnumDropDown<T>() where T : Enum
        {
            try
            {
                return Enum.GetValues(typeof(T)).Cast<T>()
              .Select(v => new { Value = v.ToString(), Text = v.GetStringValue() })
              .ToList();

            }
            catch
            {

                return null;
            }
        }

        private object GetEnumDropDownWithInt<T>() where T : Enum
        {
            try
            {
                return Enum.GetValues(typeof(T)).Cast<T>()
              .Select(v => new { Value = v, Text = v.GetStringValue() })
              .ToList();

            }
            catch
            {

                return null;
            }
        }

        private async Task<object> GetPaymentMode()
        {
            try
            {
                return await (from data in _db.PaymentMode where data.IsActive == true && !data.IsDelete select data)
                     .Select(item => new { Text = item.Mode, Value = item.Id })
                     .ToListAsync();
            }
            catch
            {

                return null;
            }
        }

        private async Task<object> GetBanks()
        {
            try
            {
                return await (from data in _db.BankMaster where data.IsActive == true && !data.IsDelete select data)
                     .Select(item => new { Text = item.Name, Value = item.Id })
                     .ToListAsync();
            }
            catch
            {

                return null;
            }
        }


        #endregion
    }
}
