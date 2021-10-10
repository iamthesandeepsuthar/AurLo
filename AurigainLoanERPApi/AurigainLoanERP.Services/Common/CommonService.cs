using AurigainLoanERP.Data.Database;
using AurigainLoanERP.Shared.Common.Method;
using AurigainLoanERP.Shared.Common.Model;
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
        private AurigainContext _db;
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

                return CreateResponse(objData, ResponseMessage.Success, true , ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {

                return CreateResponse<Dictionary<string, object>>(null, ResponseMessage.Success, true,((int)ApiStatusCode.ServerException) ,ex.Message.ToString());

            }

        }

        #region <<private Method>>

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
            catch (Exception)
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
            catch (Exception)
            {

                return null;
            }
        }

        private async Task<object> GetState()
        {
            try
            {
                return await (from data in _db.State where data.IsActive == true && !data.IsDelete select data)
                     .Select(item => new { Text = item.Name, Value = item.Id })
                     .ToListAsync();
            }
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception ex)
            {

                return null;
            }
        }


        #endregion
    }
}
